using AutoMapper;
using Synergy.App.Business.Interface;
using Synergy.App.Data;
using Synergy.App.Data.ViewModels;

namespace Synergy.App.Business.Implementation
{
    public class ColumnMetadataBusiness(
        IContextBase<ColumnMetadataViewModel, Data.Models.ColumnMetadataModel> repo,
        IMapper autoMapper,
        IQueryBase<ColumnMetadataViewModel> repoQuery,
        IServiceProvider serviceProvider,
        ICmsQueryBusiness cmsQueryBusiness)
        : BusinessBase<ColumnMetadataViewModel, Data.Models.ColumnMetadataModel>(repo, serviceProvider),
            IColumnMetadataBusiness
    {
        private readonly IContextBase<ColumnMetadataViewModel, Data.Models.ColumnMetadataModel> _repo = repo;


        public override async Task<CommandResult<ColumnMetadataViewModel>> Create(ColumnMetadataViewModel viewModel,
            bool autoCommit = true)
        {
            if (viewModel.IsForeignKey)
            {
                var fkTable = await _repo.GetSingle<TableMetadataViewModel, Data.Models.TableMetadataModel>(x =>
                    x.Name == viewModel.ForeignKeyTableName && x.Schema == viewModel.ForeignKeyTableSchemaName);
                if (fkTable != null)
                {
                    viewModel.ForeignKeyTableId = fkTable.Id;
                    if (viewModel.ForeignKeyColumnName.IsNotNullAndNotEmpty())
                    {
                        var fkColumn = await _repo.GetSingle<ColumnMetadataViewModel, Data.Models.ColumnMetadataModel>(x =>
                            x.TableMetadataId == fkTable.Id &&
                            x.Name == viewModel.ForeignKeyColumnName);
                        if (fkColumn != null)
                        {
                            viewModel.ForeignKeyColumnId = fkColumn.Id;
                        }
                    }

                    if (viewModel.ForeignKeyDisplayColumnName.IsNotNullAndNotEmpty())
                    {
                        var fkColumnDisplay = await _repo.GetSingle<ColumnMetadataViewModel, Data.Models.ColumnMetadataModel>(x =>
                            x.TableMetadataId == fkTable.Id &&
                            x.Name == viewModel.ForeignKeyDisplayColumnName);
                        if (fkColumnDisplay != null)
                        {
                            viewModel.ForeignKeyDisplayColumnId = fkColumnDisplay.Id;
                        }
                    }

                    if (viewModel.ForeignKeyConstraintName.IsNullOrEmpty())
                    {
                        var table = await _repo.GetSingle<TableMetadataViewModel, Data.Models.TableMetadataModel>(x =>
                            x.Id == viewModel.TableMetadataId);
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
                await _repo.GetSingle(x => x.TableMetadataId == viewModel.TableMetadataId && x.Name == viewModel.Name);
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
            return CommandResult<ColumnMetadataViewModel>.Instance(viewModel, result.IsSuccess, result.Messages);
        }

        public async override Task<CommandResult<ColumnMetadataViewModel>> Edit(ColumnMetadataViewModel viewModel,
            bool autoCommit = true)
        {
            var EditableByS = viewModel.EditableBy;
            var EditableContextS = viewModel.EditableContext;
            var ViewableByS = viewModel.ViewableBy;
            var ViewableContextS = viewModel.ViewableContext;
            if (viewModel.IsForeignKey)
            {
                var fkTable = await _repo.GetSingle<TableMetadataViewModel, Data.Models.TableMetadataModel>(x =>
                    x.Name == viewModel.ForeignKeyTableName && x.Schema == viewModel.ForeignKeyTableSchemaName);
                if (fkTable != null)
                {
                    viewModel.ForeignKeyTableId = fkTable.Id;
                    if (viewModel.ForeignKeyColumnName.IsNotNullAndNotEmpty())
                    {
                        var fkColumn = await _repo.GetSingle<ColumnMetadataViewModel, Data.Models.ColumnMetadataModel>(x =>
                            x.TableMetadataId == fkTable.Id &&
                            x.Name == viewModel.ForeignKeyColumnName);
                        if (fkColumn != null)
                        {
                            viewModel.ForeignKeyColumnId = fkColumn.Id;
                        }
                    }

                    if (viewModel.ForeignKeyDisplayColumnName.IsNotNullAndNotEmpty())
                    {
                        var fkColumnDisplay = await _repo.GetSingle<ColumnMetadataViewModel, Data.Models.ColumnMetadataModel>(x =>
                            x.TableMetadataId == fkTable.Id &&
                            x.Name == viewModel.ForeignKeyDisplayColumnName);
                        if (fkColumnDisplay != null)
                        {
                            viewModel.ForeignKeyDisplayColumnId = fkColumnDisplay.Id;
                        }
                    }

                    if (viewModel.ForeignKeyConstraintName.IsNullOrEmpty())
                    {
                        var table = await _repo.GetSingle<TableMetadataViewModel, Data.Models.TableMetadataModel>(x =>
                            x.Id == viewModel.TableMetadataId);
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
                var exist = await _repo.GetSingle<ColumnMetadataViewModel, Data.Models.ColumnMetadataModel>(x => x.Id == viewModel.Id);

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
            viewModel.EditableBy = EditableByS;
            viewModel.EditableContext = EditableContextS;
            viewModel.ViewableBy = ViewableByS;
            viewModel.ViewableContext = ViewableContextS;
            // await ManageForeignKeyReferenceColumn(model);

            return CommandResult<ColumnMetadataViewModel>.Instance(viewModel);
        }


        public async Task<List<ColumnMetadataViewModel>> GetViewableColumnMetadataList(string schemaName,
            string tableName, bool includeForeignKeyTableColumns = true)
        {
            var tableMetadata = await cmsQueryBusiness.GetViewableColumnMetadataListData(schemaName, tableName);
            //   var templateType = TemplateTypeEnum.FormIndexPage;
            if (tableMetadata != null && tableMetadata.Id != null)
            {
                //templateType = tableMetadata.TemplateType;

                return await GetViewableColumnMetadataList(tableMetadata.Id, includeForeignKeyTableColumns);
            }

            return await Task.FromResult(default(List<ColumnMetadataViewModel>));
        }

        public async Task<List<ColumnMetadataViewModel>> GetViewableColumnMetadataList(Guid tableMetadataId,
            bool includeForeignKeyTableColumns = true)
        {
            var list = new List<ColumnMetadataViewModel>();

            list = await _repo.GetList(x =>
                x.TableMetadataId == tableMetadataId && (x.IsHiddenColumn == false || x.IsPrimaryKey));
            if (includeForeignKeyTableColumns)
            {
                var fkColumns = await GetViewableForeignKeyColumnListForForm(tableMetadataId, list);
                if (fkColumns != null && fkColumns.Count > 0)
                {
                    list.AddRange(fkColumns);
                }
            }


            list = await _repo.GetList(x =>
                x.TableMetadataId == tableMetadataId && (x.IsHiddenColumn == false || x.IsPrimaryKey));
            if (includeForeignKeyTableColumns)
            {
                var fkColumns = await GetViewableForeignKeyColumnListForForm(tableMetadataId, list);
                if (fkColumns != null && fkColumns.Count > 0)
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

        private async Task<List<ColumnMetadataViewModel>> GetViewableForeignKeyColumnListForForm(Guid tableMetadataId,
            List<ColumnMetadataViewModel> columnList)
        {
            var list = new List<ColumnMetadataViewModel>();
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
}