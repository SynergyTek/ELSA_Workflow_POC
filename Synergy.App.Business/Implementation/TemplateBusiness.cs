using AutoMapper;
using Newtonsoft.Json;
using Synergy.App.Business.Interface;
using Synergy.App.Common;
using Synergy.App.Data;
using Synergy.App.Data.Model;
using Synergy.App.Data.ViewModel;
using static System.String;

namespace Synergy.App.Business.Implementation;

public class TemplateBusiness(
    IContextBase<TemplateViewModel, TemplateModel> repo,
    IContextBase<TableViewModel, TableModel> tableRepo,
    IContextBase<ColumnViewModel, ColumnModel> columnRepo,
    IUserContext userContext,
    IQueryBase<TableViewModel> tableQueryRepo,
    IServiceProvider sp)
    : BusinessBase<TemplateViewModel, TemplateModel>(repo, sp), ITemplateBusiness
{
    public override async Task<CommandResult<TemplateViewModel>> Create(TemplateViewModel model, bool autoCommit = true)
    {
        await ManageTemplateTable(model);
        return CommandResult<TemplateViewModel>.Instance(model);
    }

    public override async Task<CommandResult<TemplateViewModel>> Edit(TemplateViewModel model, bool autoCommit = true)
    {
        var result = await base.Edit(model, autoCommit);
        if (!result.IsSuccess)
            return CommandResult<TemplateViewModel>.Instance(model, result.IsSuccess, result.Messages);
        var tableResult = await ManageTemplateTable(model, false);
        return !tableResult.IsSuccess
            ? CommandResult<TemplateViewModel>.Instance(model, tableResult.IsSuccess, tableResult.Messages)
            : CommandResult<TemplateViewModel>.Instance(model, result.IsSuccess, result.Messages);
    }

    private async Task<CommandResult<TableModel>> ManageTemplateTable(
        TemplateViewModel model, bool autoCommit = true)
    {
        model.CreatedBy = userContext.User;
        model.UpdatedBy = userContext.User;
        var tableModel = new TableViewModel
        {
            Schema = ApplicationConstant.Database.Schema.Form,
            Template = model
        };

        var tableResult = await tableRepo.Create(tableModel, autoCommit);
        var tableResultModel = tableResult;
        if (tableResultModel == null)
        {
            return CommandResult<TableModel>.Instance(null, false, "Failed to create table");
        }

        var json = JsonConvert.DeserializeObject<dynamic>(model.Json);
        var columnsJson = json?.components;
        var columns = new List<ColumnViewModel>();
        if (columnsJson != null)
        {
            foreach (var column in columnsJson)
            {
                var columnModel = new ColumnViewModel
                {
                    Table = tableResultModel,
                    Name = column.label.ToString(),
                    DataType = GetDataType(column.type.ToString()),
                    Alias = column.key.ToString(),
                };
                columns.Add(columnModel);
            }
        }

        var columnQueryResult = await ManageTemplateColumn(columns);
        if (!columnQueryResult.IsSuccess)
        {
            return CommandResult<TableModel>.Instance(tableResultModel, false, columnQueryResult.Messages);
        }

        var columnQuery = columnQueryResult.Item;
        if (IsNullOrEmpty(columnQuery))
        {
            return CommandResult<TableModel>.Instance(tableResultModel, true, "No columns to create");
        }

        var query = $"""
                     CREATE TABLE IF NOT EXISTS {tableModel.Schema}."{model.Reference}" (
                     {columnQuery}
                     )
                     """;

        await tableQueryRepo.ExecuteCommand(query, null);

        return CommandResult<TableModel>.Instance(tableResultModel, true, "Table created successfully");
    }

    private async Task<CommandResult<string>> ManageTemplateColumn(List<ColumnViewModel> columns)
    {
        if (columns.Count == 0)
        {
            return CommandResult<string>.Instance("", false, "No columns provided");
        }

        var columnTasks = new List<Task>();

        var columnQueries = new List<string>
        {
            $"\"{nameof(BaseModel.Id)}\" UUID PRIMARY KEY",
            $"\"{nameof(BaseModel.CreatedAt)}\" TIMESTAMP DEFAULT CURRENT_TIMESTAMP",
            $"\"{nameof(BaseModel.CreatedBy)}Id\" UUID  REFERENCES public.\"User\"(\"Id\")",
            $"\"{nameof(BaseModel.UpdatedAt)}\" TIMESTAMP DEFAULT CURRENT_TIMESTAMP",
            $"\"{nameof(BaseModel.UpdatedBy)}Id\" UUID  REFERENCES public.\"User\"(\"Id\")",
            $"\"{nameof(BaseModel.IsDeleted)}\" BOOLEAN DEFAULT FALSE"
        };

        foreach (var column in columns)
        {
            var task = columnRepo.Create(column);
            columnTasks.Add(task);
            columnQueries.Add($"\"{column.Alias}\" {column.DataType}");
        }

        await Task.WhenAll(columnTasks);
        return CommandResult<string>.Instance(Join(", ", columnQueries), true, "Column created successfully");
    }

    private DataColumnTypeEnum GetDataType(string dataType)
    {
        return dataType switch
        {
            "number" => DataColumnTypeEnum.Integer,
            "datetime" => DataColumnTypeEnum.Timestamp,
            "checkbox" => DataColumnTypeEnum.Bool,
            _ => DataColumnTypeEnum.Text
        };
    }
}