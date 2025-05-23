using Synergy.App.Business.Interface;
using Synergy.App.Data;
using Synergy.App.Data.ViewModels;
using Synergy.App.Data.Models;

namespace Synergy.App.Business.Implementation;

public class ColumnMetadataBusiness(
    IContextBase<ColumnViewModel, ColumnModel> repo,
    IServiceProvider serviceProvider,
    ICmsQueryBusiness cmsQueryBusiness)
    : BusinessBase<ColumnViewModel, ColumnModel>(repo, serviceProvider),
        IColumnMetadataBusiness
{
    private readonly IContextBase<ColumnViewModel, ColumnModel> _repo = repo;


    public override async Task<CommandResult<ColumnViewModel>> Create(ColumnViewModel viewModel,
        bool autoCommit = true)
    {
        if (viewModel.IsForeignKey)
        {
            var fkTable = await _repo.GetSingle<TableViewModel, TableModel>(x =>
                x.Name == viewModel.ForeignKeyTableName && x.Schema == viewModel.ForeignKeyTableSchemaName);
            if (fkTable != null)
            {
                viewModel.ForeignKeyTableId = fkTable.Id;
                if (viewModel.ForeignKeyColumnName.IsNotNullAndNotEmpty())
                {
                    var fkColumn = await _repo.GetSingle<ColumnViewModel, ColumnModel>(x =>
                        x.TableId == fkTable.Id &&
                        x.Name == viewModel.ForeignKeyColumnName);
                    if (fkColumn != null)
                    {
                        viewModel.ForeignKeyColumnId = fkColumn.Id;
                    }
                }

                if (viewModel.ForeignKeyDisplayColumnName.IsNotNullAndNotEmpty())
                {
                    var fkColumnDisplay = await _repo.GetSingle<ColumnViewModel, ColumnModel>(x =>
                        x.TableId == fkTable.Id &&
                        x.Name == viewModel.ForeignKeyDisplayColumnName);
                    if (fkColumnDisplay != null)
                    {
                        viewModel.ForeignKeyDisplayColumnId = fkColumnDisplay.Id;
                    }
                }

                if (viewModel.ForeignKeyConstraintName.IsNullOrEmpty())
                {
                    var table = await _repo.GetSingle<TableViewModel, TableModel>(x =>
                        x.Id == viewModel.TableId);
                    if (table != null)
                    {
                        viewModel.ForeignKeyConstraintName =
                            $"FK_{table.Name}_{fkTable.Name}_{viewModel.Name}_{viewModel.ForeignKeyColumnName}";
                    }
                }
            }
        }

        if (viewModel.ForeignKeyConstraintName != null)
        {
            viewModel.ForeignKeyConstraintName = TruncateForeignKeyContraint(viewModel.ForeignKeyConstraintName);
        }

        var colExists =
            await _repo.GetSingle(x => x.TableId == viewModel.TableId && x.Name == viewModel.Name);
        if (colExists != null)
        {
            viewModel.Id = colExists.Id;
            viewModel.DataAction = DataActionEnum.Edit;
            return await Edit(viewModel);
        }

        var result = await base.Create(viewModel, autoCommit);
        if (result.IsSuccess)
        {
            viewModel.Id = result.Item.Id;
        }

        // await ManageForeignKeyReferenceColumn(model);
        return CommandResult<ColumnViewModel>.Instance(viewModel, result.IsSuccess, result.Messages);
    }

    public override async Task<CommandResult<ColumnViewModel>> Edit(ColumnViewModel viewModel,
        bool autoCommit = true)
    {
        var editableByS = viewModel.EditableBy;
        var editableContextS = viewModel.EditableContext;
        var viewableByS = viewModel.ViewableBy;
        var viewableContextS = viewModel.ViewableContext;
        if (viewModel.IsForeignKey)
        {
            var fkTable = await _repo.GetSingle<TableViewModel, TableModel>(x =>
                x.Name == viewModel.ForeignKeyTableName && x.Schema == viewModel.ForeignKeyTableSchemaName);
            if (fkTable != null)
            {
                viewModel.ForeignKeyTableId = fkTable.Id;
                if (viewModel.ForeignKeyColumnName.IsNotNullAndNotEmpty())
                {
                    var fkColumn = await _repo.GetSingle<ColumnViewModel, ColumnModel>(x =>
                        x.TableId == fkTable.Id &&
                        x.Name == viewModel.ForeignKeyColumnName);
                    if (fkColumn != null)
                    {
                        viewModel.ForeignKeyColumnId = fkColumn.Id;
                    }
                }

                if (viewModel.ForeignKeyDisplayColumnName.IsNotNullAndNotEmpty())
                {
                    var fkColumnDisplay = await _repo.GetSingle<ColumnViewModel, ColumnModel>(x =>
                        x.TableId == fkTable.Id &&
                        x.Name == viewModel.ForeignKeyDisplayColumnName);
                    if (fkColumnDisplay != null)
                    {
                        viewModel.ForeignKeyDisplayColumnId = fkColumnDisplay.Id;
                    }
                }

                if (viewModel.ForeignKeyConstraintName.IsNullOrEmpty())
                {
                    var table = await _repo.GetSingle<TableViewModel, TableModel>(x =>
                        x.Id == viewModel.TableId);
                    if (table != null)
                    {
                        viewModel.ForeignKeyConstraintName =
                            $"FK_{table.Name}_{fkTable.Name}_{viewModel.Name}_{viewModel.ForeignKeyColumnName}";
                    }
                }
            }
        }

        if (viewModel.IgnorePermission)
        {
            var exist = await _repo.GetSingle<ColumnViewModel, ColumnModel>(x => x.Id == viewModel.Id);

            if (exist != null)
            {
                viewModel.EditableBy = exist.EditableBy;
                viewModel.EditableContext = exist.EditableContext;
                viewModel.ViewableBy = exist.ViewableBy;
                viewModel.ViewableContext = exist.ViewableContext;
            }
        }

        if (viewModel.ForeignKeyConstraintName != null)
        {
            viewModel.ForeignKeyConstraintName = TruncateForeignKeyContraint(viewModel.ForeignKeyConstraintName);
        }

        var result = await base.Edit(viewModel, autoCommit);
        viewModel.EditableBy = editableByS;
        viewModel.EditableContext = editableContextS;
        viewModel.ViewableBy = viewableByS;
        viewModel.ViewableContext = viewableContextS;
        // await ManageForeignKeyReferenceColumn(model);

        return CommandResult<ColumnViewModel>.Instance(viewModel);
    }


    public async Task<List<ColumnViewModel>> GetViewableColumnMetadataList(string schemaName,
        string tableName, bool includeForeignKeyTableColumns = true)
    {
        var tableMetadata = await cmsQueryBusiness.GetViewableColumnMetadataListData(schemaName, tableName);
        //   var templateType = TemplateTypeEnum.FormIndexPage;
        if (tableMetadata?.Id != null)
        {
            //templateType = tableMetadata.TemplateType;

            return await GetViewableColumnMetadataList(tableMetadata.Id, includeForeignKeyTableColumns);
        }

        return await Task.FromResult(default(List<ColumnViewModel>));
    }

    public async Task<List<ColumnViewModel>> GetViewableColumnMetadataList(Guid tableMetadataId,
        bool includeForeignKeyTableColumns = true)
    {
        var list = await _repo.GetList(x =>
            x.TableId == tableMetadataId && (x.IsHiddenColumn == false || x.IsPrimaryKey));
        if (includeForeignKeyTableColumns)
        {
            var fkColumns = await GetViewableForeignKeyColumnListForForm(tableMetadataId, list);
            if (fkColumns.Count != 0)
            {
                list.AddRange(fkColumns);
            }
        }


        list = await _repo.GetList(x =>
            x.TableId == tableMetadataId && (x.IsHiddenColumn == false || x.IsPrimaryKey));
        if (!includeForeignKeyTableColumns) return list.OrderBy(x => x.Name).ToList();
        {
            var fkColumns = await GetViewableForeignKeyColumnListForForm(tableMetadataId, list);
            if (fkColumns.Count != 0)
            {
                list.AddRange(fkColumns);
            }
        }


        return list.OrderBy(x => x.Name).ToList();
    }

    private string TruncateForeignKeyContraint(string name)
    {
        if (name.IsNullOrEmpty())
        {
            return name;
        }

        if (name.Length > 60)
        {
            return $"{name.Substring(0, 25)}_{Guid.NewGuid().ToString()}";
        }

        return name;
    }

    private async Task<List<ColumnViewModel>> GetViewableForeignKeyColumnListForForm(Guid tableMetadataId,
        List<ColumnViewModel> columnList)
    {
        var list = new List<ColumnViewModel>();
        var fks = columnList.Where(x => x.IsForeignKey && x.ForeignKeyTableId != Guid.Empty);
        if (fks != null && fks.Any())
        {
            var result = await cmsQueryBusiness.GetViewableForeignKeyColumnListForFormData(tableMetadataId);
            if (result != null && result.Any())
            {
                foreach (var item in result)
                {
                    item.Name = $"{item.ForeignKeyBaseColumnName}_{item.Name}";
                }


                list.AddRange(result);
            }
        }

        return list;
    }

      
}