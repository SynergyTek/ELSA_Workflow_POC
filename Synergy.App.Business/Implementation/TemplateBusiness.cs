using Newtonsoft.Json;
using Synergy.App.Business.Interface;
using Synergy.App.Common;
using Synergy.App.Data;
using Synergy.App.Data.Models;
using Synergy.App.Data.ViewModels;
using static System.String;

namespace Synergy.App.Business.Implementation;

public class TemplateBusiness(
    IContextBase<TemplateViewModel, TemplateModel> repo,
    IContextBase<TableViewModel, TableModel> tableRepo,
    IContextBase<ColumnViewModel, ColumnModel> columnRepo,
    IQueryBase<TableViewModel> tableQueryRepo,
    IServiceProvider sp)
    : BusinessBase<TemplateViewModel, TemplateModel>(repo, sp), ITemplateBusiness
{
    private readonly IContextBase<TemplateViewModel, TemplateModel> _repo = repo;

    public override async Task<CommandResult<TemplateViewModel>> Create(TemplateViewModel model, bool autoCommit = true)
    {
        var templateResult = await base.Create(model, autoCommit);
        await ManageTemplateTable(templateResult.Item, false);
        return CommandResult<TemplateViewModel>.Instance(templateResult.Item, templateResult.IsSuccess,
            templateResult.Messages);
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
        var tableModel = new TableViewModel
        {
            Schema = ApplicationConstant.Database.Schema.Form,
            Template = model
        };

        var tableResult = await tableRepo.Create(tableModel, autoCommit);
        if (tableResult == null)
        {
            return CommandResult<TableModel>.Instance(null, false, "Failed to create table");
        }
        var json = JsonConvert.DeserializeObject<dynamic>(model.Json);
        var columnsJson = json.components;
        var columns = new List<ColumnViewModel>();
        if (columnsJson != null)
        {
            foreach (var column in columnsJson)
            {
                var columnModel = new ColumnViewModel
                {
                    TableId = tableModel.Id,
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
            return CommandResult<TableModel>.Instance(tableResult, false, columnQueryResult.Messages);
        }

        var columnQuery = columnQueryResult.Item;
        if (IsNullOrEmpty(columnQuery))
        {
            return CommandResult<TableModel>.Instance(tableResult, true, "No columns to create");
        }

        var query = $"""
                     CREATE TABLE IF NOT EXISTS {tableModel.Schema}."{model.Name}" (
                     {columnQuery}
                     )
                     """;

        await tableQueryRepo.ExecuteCommand(query, null);

        return CommandResult<TableModel>.Instance(tableResult, true, "Table created successfully");
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
            "\"Id\" UUID PRIMARY KEY",
            "\"CreatedDate\" TIMESTAMP DEFAULT CURRENT_TIMESTAMP",
            "\"LastUpdatedDate\" TIMESTAMP DEFAULT CURRENT_TIMESTAMP",
            "\"CreatedBy\" UUID Foreign Key REFERENCES public.\"User\"(\"Id\")",
            "\"LastUpdatedBy\" UUID Foreign Key REFERENCES public.\"User\"(\"Id\")",
        };
        foreach (var column in columns)
        {
            columnTasks.Add(columnRepo.Create(column, false));
            columnQueries.Add($"\"{column.Alias}\" {GetDataType(column.DataTypeString)}");
        }

        await Task.WhenAll(columnTasks.ToArray());
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