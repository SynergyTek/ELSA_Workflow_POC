using AutoMapper;
using Synergy.App.Business.Interface;
using Synergy.App.Common;
using Synergy.App.Data;
using Synergy.App.Data.Models;
using Synergy.App.Data.ViewModels;

namespace Synergy.App.Business.Implementation
{
    public class ColumnMetadataBusiness(
        IContextBase<ColumnMetadataViewModel, ColumnMetadata> repo,
        IMapper autoMapper,
        IRepositoryQueryBase<ColumnMetadataViewModel> repoQuery,
        IServiceProvider serviceProvider,
        ICmsQueryBusiness cmsQueryBusiness)
        : BaseBusiness<ColumnMetadataViewModel, ColumnMetadata>(repo, serviceProvider),
            IColumnMetadataBusiness
    {
        private readonly IContextBase<ColumnMetadataViewModel, ColumnMetadata> _repo = repo;


        public override async Task<CommandResult<ColumnMetadataViewModel>> Create(ColumnMetadataViewModel model,
            bool autoCommit = true)
        {
            if (model.IsForeignKey)
            {
                var fkTable = await _repo.GetSingle<TableMetadataViewModel, TableMetadata>(x =>
                    x.Name == model.ForeignKeyTableName && x.Schema == model.ForeignKeyTableSchemaName);
                if (fkTable != null)
                {
                    model.ForeignKeyTableId = fkTable.Id;
                    if (model.ForeignKeyColumnName.IsNotNullAndNotEmpty())
                    {
                        var fkColumn = await _repo.GetSingle<ColumnMetadataViewModel, ColumnMetadata>(x =>
                            x.TableMetadataId == fkTable.Id &&
                            x.Name == model.ForeignKeyColumnName);
                        if (fkColumn != null)
                        {
                            model.ForeignKeyColumnId = fkColumn.Id;
                        }
                    }

                    if (model.ForeignKeyDisplayColumnName.IsNotNullAndNotEmpty())
                    {
                        var fkColumnDisplay = await _repo.GetSingle<ColumnMetadataViewModel, ColumnMetadata>(x =>
                            x.TableMetadataId == fkTable.Id &&
                            x.Name == model.ForeignKeyDisplayColumnName);
                        if (fkColumnDisplay != null)
                        {
                            model.ForeignKeyDisplayColumnId = fkColumnDisplay.Id;
                        }
                    }

                    if (model.ForeignKeyConstraintName.IsNullOrEmpty())
                    {
                        var table = await _repo.GetSingle<TableMetadataViewModel, TableMetadata>(x =>
                            x.Id == model.TableMetadataId);
                        if (table != null)
                        {
                            model.ForeignKeyConstraintName =
                                $"FK_{table.Name}_{fkTable.Name}_{model.Name}_{model.ForeignKeyColumnName}";
                        }
                    }
                }
            }

            if (model.ForeignKeyConstraintName != null)
            {
                model.ForeignKeyConstraintName = TruncateForeignKeyContraint(model.ForeignKeyConstraintName);
            }

            var colExists =
                await _repo.GetSingle(x => x.TableMetadataId == model.TableMetadataId && x.Name == model.Name);
            if (colExists != null)
            {
                model.Id = colExists.Id;
                model.DataAction = DataActionEnum.Edit;
                return await Edit(model);
            }

            var result = await base.Create(model, autoCommit);
            if (result.IsSuccess)
            {
                model.Id = result.Item.Id;
            }

            // await ManageForeignKeyReferenceColumn(model);
            return CommandResult<ColumnMetadataViewModel>.Instance(model, result.IsSuccess, result.Messages);
        }

        public async override Task<CommandResult<ColumnMetadataViewModel>> Edit(ColumnMetadataViewModel model,
            bool autoCommit = true)
        {
            var EditableByS = model.EditableBy;
            var EditableContextS = model.EditableContext;
            var ViewableByS = model.ViewableBy;
            var ViewableContextS = model.ViewableContext;
            if (model.IsForeignKey)
            {
                var fkTable = await _repo.GetSingle<TableMetadataViewModel, TableMetadata>(x =>
                    x.Name == model.ForeignKeyTableName && x.Schema == model.ForeignKeyTableSchemaName);
                if (fkTable != null)
                {
                    model.ForeignKeyTableId = fkTable.Id;
                    if (model.ForeignKeyColumnName.IsNotNullAndNotEmpty())
                    {
                        var fkColumn = await _repo.GetSingle<ColumnMetadataViewModel, ColumnMetadata>(x =>
                            x.TableMetadataId == fkTable.Id &&
                            x.Name == model.ForeignKeyColumnName);
                        if (fkColumn != null)
                        {
                            model.ForeignKeyColumnId = fkColumn.Id;
                        }
                    }

                    if (model.ForeignKeyDisplayColumnName.IsNotNullAndNotEmpty())
                    {
                        var fkColumnDisplay = await _repo.GetSingle<ColumnMetadataViewModel, ColumnMetadata>(x =>
                            x.TableMetadataId == fkTable.Id &&
                            x.Name == model.ForeignKeyDisplayColumnName);
                        if (fkColumnDisplay != null)
                        {
                            model.ForeignKeyDisplayColumnId = fkColumnDisplay.Id;
                        }
                    }

                    if (model.ForeignKeyConstraintName.IsNullOrEmpty())
                    {
                        var table = await _repo.GetSingle<TableMetadataViewModel, TableMetadata>(x =>
                            x.Id == model.TableMetadataId);
                        if (table != null)
                        {
                            model.ForeignKeyConstraintName =
                                $"FK_{table.Name}_{fkTable.Name}_{model.Name}_{model.ForeignKeyColumnName}";
                        }
                    }
                }
            }

            if (model.IgnorePermission)
            {
                var exist = await _repo.GetSingle<ColumnMetadataViewModel, ColumnMetadata>(x => x.Id == model.Id);

                if (exist != null)
                {
                    model.EditableBy = exist.EditableBy;
                    model.EditableContext = exist.EditableContext;
                    model.ViewableBy = exist.ViewableBy;
                    model.ViewableContext = exist.ViewableContext;
                }
            }

            if (model.ForeignKeyConstraintName != null)
            {
                model.ForeignKeyConstraintName = TruncateForeignKeyContraint(model.ForeignKeyConstraintName);
            }

            var result = await base.Edit(model, autoCommit);
            model.EditableBy = EditableByS;
            model.EditableContext = EditableContextS;
            model.ViewableBy = ViewableByS;
            model.ViewableContext = ViewableContextS;
            // await ManageForeignKeyReferenceColumn(model);

            return CommandResult<ColumnMetadataViewModel>.Instance(model);
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