using System.Data;
using System.Text;
using Synergy.App.Business.Interface;
using Synergy.App.Common;
using Synergy.App.Data;
using Synergy.App.Data.ViewModels;
using Synergy.App.Data.Models;


namespace Synergy.App.Business.Implementation;

public class CmsBusiness(
    IContextBase<TemplateViewModel, TemplateModel> repo,
    IServiceProvider serviceProvider)
    : BusinessBase<TemplateViewModel, TemplateModel>(repo, serviceProvider), ICmsBusiness
{
    private readonly IContextBase<TemplateViewModel, TemplateModel> _repo = repo;




    private string ConvertToPostgreType(ColumnModel column)
    {
        if (column.IsSystemColumn)
        {
            return column.DataType switch
            {
                DataColumnTypeEnum.Text => "text",
                DataColumnTypeEnum.Bool => "boolean",
                DataColumnTypeEnum.DateTime => "timestamp without time zone",
                DataColumnTypeEnum.Integer => "integer",
                DataColumnTypeEnum.Double => "double precision",
                DataColumnTypeEnum.Long => "bigint",
                DataColumnTypeEnum.TextArray => "text[]",
                _ => "text",
            };
        }
        else
        {
            return "text";
        }
    }


    private void ManageForeignKey(List<string> alterColumnScriptList, ColumnModel column, DataTable constraints)
    {
        if (column.IsForeignKey && column.IsVirtualForeignKey == false &&
            column.ForeignKeyConstraintName.IsNotNullAndNotEmpty())
        {
            var existingFk = constraints.AsEnumerable().FirstOrDefault
                (r => r.Field<string>("conname") == column.ForeignKeyConstraintName);
            if (existingFk != null)
            {
                alterColumnScriptList.Add($@"DROP CONSTRAINT ""{column.ForeignKeyConstraintName}""");
            }

            alterColumnScriptList.Add(
                $@"ADD CONSTRAINT ""{column.ForeignKeyConstraintName}"" FOREIGN KEY (""{column.Name}"") REFERENCES {column.ForeignKeyTableSchemaName}.""{column.ForeignKeyTableName}"" (""{column.ForeignKeyColumnName}"") MATCH SIMPLE ON UPDATE NO ACTION  ON DELETE SET NULL");
        }
    }


    // public async Task<DataTable> GetData(string schemaName, string tableName, string columns = null,
    //     string filter = null, string orderbyColumns = null, OrderByEnum orderby = OrderByEnum.Ascending,
    //     string where = null, bool ignoreJoins = false, string returnColumns = null, int? limit = null,
    //     int? skip = null, bool enableLocalization = false, string lang = null)
    // {
    //     var tableMetaData =
    //         await _repo.GetSingle<TableMetadataViewModel, TableMetadata>(x =>
    //             x.Schema == schemaName && x.Name == tableName);
    //
    //
    //     var selectQuery = await GetFormSelectQuery(tableMetaData, where, columns, filter, ignoreJoins,
    //         returnColumns, limit, skip, enableLocalization, lang);
    //
    //
    //     //return await _queryRepo.ExecuteQueryDataTable(selectQuery, null);
    //     var dt = await cmsQueryBusiness.GetQueryDataTable(selectQuery);
    //     return dt;
    // }
    //
    // private async Task<string> GetCustomSelectQuery(TableMetadataViewModel tableMetaData, string filtercolumns,
    //     string filter)
    // {
    //     var columns = new List<string>();
    //     var tables = new List<string>();
    //     var condition = new List<string>();
    //     var pkColumns = await _repo.GetList<ColumnMetadataViewModel, ColumnMetadata>(x =>
    //         x.TableMetadataId == tableMetaData.Id
    //         && x.IsVirtualColumn == false);
    //     foreach (var item in pkColumns)
    //     {
    //         columns.Add(@$"{Environment.NewLine}""{tableMetaData.Name}"".""{item.Name}"" as ""{item.Name}""");
    //     }
    //
    //     tables.Add(@$"{tableMetaData.Schema}.""{tableMetaData.Name}"" as ""{tableMetaData.Name}""");
    //     condition.Add(@$"""{tableMetaData.Name}"".""IsDeleted""=false");
    //     if (filtercolumns.IsNotNullAndNotEmpty() && filter.IsNotNullAndNotEmpty())
    //     {
    //         var filters = filtercolumns.Split(',');
    //         var filterValues = filter.Split(',');
    //         var i = 0;
    //         foreach (var item in filters)
    //         {
    //             var value = filterValues[i];
    //             condition.Add(@$"""{tableMetaData.Name}"".""{item}""='{value}'");
    //             i++;
    //         }
    //     }
    //     //var fks = pkColumns.Where(x => x.IsForeignKey && x.ForeignKeyTableId.IsNotNullAndNotEmpty() && x.HideForeignKeyTableColumns == false).ToList();
    //     //if (fks != null && fks.Count() > 0)
    //     //{
    //     //    foreach (var item in fks)
    //     //    {
    //     //        if (item.IsUdfColumn)
    //     //        {
    //     //            item.ForeignKeyTableAliasName = item.ForeignKeyTableName + "_" + item.Name;
    //     //        }
    //     //        tables.Add(@$"left join {item.ForeignKeyTableSchemaName}.""{item.ForeignKeyTableName}"" as ""{item.ForeignKeyTableAliasName}""
    //     //        on ""{tableMetaData.Name}"".""{item.Name}""=""{item.ForeignKeyTableAliasName}"".""{item.ForeignKeyColumnName}""
    //     //        and ""{item.ForeignKeyTableAliasName}"".""IsDeleted""=false");
    //     //    }
    //     //    var query = $@"SELECT  fc.*,true as ""IsForeignKeyTableColumn"",c.""ForeignKeyTableName"" as ""TableName""
    //     //    ,c.""ForeignKeyTableSchemaName"" as ""TableSchemaName"",c.""ForeignKeyTableAliasName"" as  ""TableAliasName""
    //     //    ,c.""Name"" as ""ForeignKeyColumnName"",c.""IsUdfColumn"" as ""IsUdfColumn""
    //     //    FROM public.""ColumnMetadata"" c
    //     //    join public.""TableMetadata"" ft on c.""ForeignKeyTableId""=ft.""Id"" and ft.""IsDeleted""=false
    //     //    join public.""ColumnMetadata"" fc on ft.""Id""=fc.""TableMetadataId""  and fc.""IsDeleted""=false
    //     //    where c.""TableMetadataId""='{tableMetaData.Id}'
    //     //    and c.""IsForeignKey""=true and c.""IsDeleted""=false";
    //     //    var result = await _queryRepo.ExecuteQueryList<ColumnMetadataViewModel>(query, null);
    //     //    if (result != null && result.Count() > 0)
    //     //    {
    //     //        foreach (var item in result)
    //     //        {
    //     //            var tableName = @$"""{item.TableAliasName}""";
    //     //            if (item.IsUdfColumn)
    //     //            {
    //     //                tableName = @$"""{item.TableAliasName}_{item.ForeignKeyColumnName}""";
    //     //            }
    //
    //     //            columns.Add(@$"{Environment.NewLine}{tableName}.""{item.Name}"" as ""{item.ForeignKeyColumnName}_{item.Name}""");
    //     //        }
    //
    //     //    }
    //     //}
    //
    //     var selectQuery =
    //         @$"select {string.Join(",", columns)}  {Environment.NewLine}from {string.Join(Environment.NewLine, tables)}{Environment.NewLine}where {string.Join(Environment.NewLine + "and ", condition)}";
    //     return selectQuery;
    // }
    //
    // private async Task<string> GetFormSelectQuery(TableMetadataViewModel tableMetaData, string where = null,
    //     string filtercolumns = null, string filter = null, bool ignoreJoins = false, string returnColumns = null,
    //     int? limit = null, int? skip = null, bool enableLocalization = false, string lang = null)
    // {
    //     var columns = new List<string>();
    //     var tables = new List<string>();
    //     var condition = new List<string>();
    //     var pkColumns = await _repo.GetList<ColumnMetadataViewModel, ColumnMetadata>(x =>
    //         x.TableMetadataId == tableMetaData.Id && x.IsVirtualColumn == false);
    //     TemplateViewModel formTemplate = null;
    //     var displayColumnId = Guid.Empty;
    //     string displayColumn = null;
    //     if (enableLocalization || lang != "en-US")
    //     {
    //         //TODO Change to single qury
    //         var template =
    //             await _repo.GetSingle<TemplateViewModel, Template>(x => x.TableMetadataId == tableMetaData.Id);
    //         if (template != null)
    //         {
    //             formTemplate =
    //                 await _repo.GetSingle<TemplateViewModel, Template>(x => x.Id == template.Id);
    //         }
    //
    //         displayColumnId = formTemplate.Id;
    //     }
    //
    //     string localizedColumn = null;
    //     foreach (var item in pkColumns)
    //     {
    //         if (displayColumnId != Guid.Empty && displayColumnId == item.Id)
    //         {
    //             displayColumn = item.Name;
    //
    //             var col = @$"""{tableMetaData.Name}"".""{item.Name}"" as ""{item.Name}""";
    //
    //
    //             columns.Add(@$"{Environment.NewLine}{col}");
    //         }
    //         else
    //         {
    //             columns.Add(@$"{Environment.NewLine}""{tableMetaData.Name}"".""{item.Name}"" as ""{item.Name}""");
    //         }
    //     }
    //
    //     tables.Add(@$"{tableMetaData.Schema}.""{tableMetaData.Name}"" as ""{tableMetaData.Name}""");
    //     condition.Add(@$"""{tableMetaData.Name}"".""IsDeleted""=false");
    //
    //
    //     if (filtercolumns.IsNotNullAndNotEmpty() && filter.IsNotNullAndNotEmpty())
    //     {
    //         var filters = filtercolumns.Split(',');
    //         var filterValues = filter.Split(',');
    //         var i = 0;
    //         foreach (var item in filters)
    //         {
    //             var value = filterValues[i];
    //             condition.Add(@$"""{tableMetaData.Name}"".""{item}""='{value}'");
    //             i++;
    //         }
    //     }
    //
    //     if (displayColumnId != Guid.Empty)
    //     {
    //         tables.Add(@$"left join public.""FormResourceLanguage"" as lang
    //             on ""{tableMetaData.Name}"".""Id""=lang.""FormTableId""
    //             and lang.""IsDeleted""=false");
    //     }
    //
    //     var fks = pkColumns.Where(x =>
    //             x.IsForeignKey && x.ForeignKeyTableId != Guid.Empty &&
    //             x.HideForeignKeyTableColumns == false)
    //         .ToList();
    //     if (fks.Any())
    //     {
    //         foreach (var item in fks)
    //         {
    //             if (item.IsUdfColumn)
    //             {
    //                 item.ForeignKeyTableAliasName = item.ForeignKeyTableName + "_" + item.Name;
    //             }
    //
    //             tables.Add(
    //                 @$"left join {item.ForeignKeyTableSchemaName}.""{item.ForeignKeyTableName}"" as ""{item.ForeignKeyTableAliasName}""
    //             on ""{tableMetaData.Name}"".""{item.Name}""=""{item.ForeignKeyTableAliasName}"".""{item.ForeignKeyColumnName}""
    //             and ""{item.ForeignKeyTableAliasName}"".""IsDeleted""=false");
    //         }
    //
    //         var result = await cmsQueryBusiness.GetForeignKeyColumnByTableMetadata(tableMetaData);
    //
    //         if (result != null && result.Count() > 0)
    //         {
    //             foreach (var item in result)
    //             {
    //                 var tableName = @$"""{item.TableAliasName}""";
    //                 if (item.IsUdfColumn)
    //                 {
    //                     tableName = @$"""{item.TableAliasName}_{item.ForeignKeyColumnName}""";
    //                 }
    //
    //                 columns.Add(
    //                     @$"{Environment.NewLine}{tableName}.""{item.Name}"" as ""{item.ForeignKeyColumnName}_{item.Name}""");
    //             }
    //         }
    //     }
    //     //var cols = "*";
    //     //if (columns.Any())
    //     //{
    //     //    cols = string.Join(",", columns);
    //     //}
    //
    //     var selectQuery =
    //         @$"select {string.Join(",", columns)}  {Environment.NewLine}from {string.Join(Environment.NewLine, tables)}{Environment.NewLine}where {string.Join(Environment.NewLine + "and ", condition)}";
    //     if (where.IsNotNullAndNotEmpty())
    //     {
    //         where = where.Replace(",", "','");
    //         selectQuery = $"{selectQuery} {where}";
    //     }
    //
    //     if (ignoreJoins)
    //     {
    //         var langQuery = "";
    //         if (returnColumns.IsNullOrEmpty())
    //         {
    //             returnColumns = @$"""{tableMetaData.Name}"".*";
    //         }
    //         else
    //         {
    //             returnColumns = @$"""{returnColumns.Replace(",", "\",\"")}""";
    //         }
    //
    //         if (localizedColumn.IsNotNullAndNotEmpty())
    //         {
    //             returnColumns = @$"{returnColumns},{localizedColumn}";
    //             langQuery =
    //                 @$"left join public.""FormResourceLanguage"" as lang  on ""{tableMetaData.Name}"".""Id""=lang.""FormTableId"" and lang.""IsDeleted""=false";
    //         }
    //
    //         selectQuery = @$"select {returnColumns} from {tableMetaData.Schema}.""{tableMetaData.Name}""
    //         {langQuery}
    //         where ""{tableMetaData.Name}"".""IsDeleted""=false ";
    //         if (where.IsNotNullAndNotEmpty())
    //         {
    //             selectQuery = $"{selectQuery} {where}";
    //         }
    //     }
    //
    //     return selectQuery;
    // }
    //
    // private async Task<string> GetSelectQuery(TableMetadataViewModel tableMetaData, string? where = null,
    //     string filtercolumns = null, string filter = null, bool ignoreJoins = false, string returnColumns = null,
    //     int? limit = null, int? skip = null, bool enableLocalization = false)
    // {
    //     var columns = new List<string>();
    //     var tables = new List<string>();
    //     var condition = new List<string>();
    //     var pkColumns = await _repo.GetList<ColumnMetadataViewModel, ColumnMetadata>(x =>
    //         x.TableMetadataId == tableMetaData.Id
    //         && x.IsVirtualColumn == false);
    //     foreach (var item in pkColumns)
    //     {
    //         columns.Add(@$"{Environment.NewLine}""{tableMetaData.Name}"".""{item.Name}"" as ""{item.Name}""");
    //     }
    //
    //     tables.Add(@$"{tableMetaData.Schema}.""{tableMetaData.Name}"" as ""{tableMetaData.Name}""");
    //     condition.Add(@$"""{tableMetaData.Name}"".""IsDeleted""=false");
    //     if (filtercolumns.IsNotNullAndNotEmpty() && filter.IsNotNullAndNotEmpty())
    //     {
    //         var filters = filtercolumns.Split(',');
    //         var filterValues = filter.Split(',');
    //         var i = 0;
    //         foreach (var item in filters)
    //         {
    //             var value = filterValues[i];
    //             condition.Add(@$"""{tableMetaData.Name}"".""{item}""='{value}'");
    //             i++;
    //         }
    //     }
    //
    //     var fks = pkColumns.Where(x =>
    //             x.IsForeignKey && x.ForeignKeyTableId.IsNotNullAndNotEmpty() &&
    //             x.HideForeignKeyTableColumns == false)
    //         .ToList();
    //     if (fks != null && fks.Count() > 0)
    //     {
    //         foreach (var item in fks)
    //         {
    //             if (item.IsUdfColumn)
    //             {
    //                 item.ForeignKeyTableAliasName = item.ForeignKeyTableName + "_" + item.Name;
    //             }
    //
    //             tables.Add(
    //                 @$"left join {item.ForeignKeyTableSchemaName}.""{item.ForeignKeyTableName}"" as ""{item.ForeignKeyTableAliasName}""
    //             on ""{tableMetaData.Name}"".""{item.Name}""=""{item.ForeignKeyTableAliasName}"".""{item.ForeignKeyColumnName}""
    //             and ""{item.ForeignKeyTableAliasName}"".""IsDeleted""=false");
    //         }
    //         //var query = $@"SELECT  fc.*,true as ""IsForeignKeyTableColumn"",c.""ForeignKeyTableName"" as ""TableName""
    //         //,c.""ForeignKeyTableSchemaName"" as ""TableSchemaName"",c.""ForeignKeyTableAliasName"" as  ""TableAliasName""
    //         //,c.""Name"" as ""ForeignKeyColumnName"",c.""IsUdfColumn"" as ""IsUdfColumn""
    //         //FROM public.""ColumnMetadata"" c
    //         //join public.""TableMetadata"" ft on c.""ForeignKeyTableId""=ft.""Id"" and ft.""IsDeleted""=false
    //         //join public.""ColumnMetadata"" fc on ft.""Id""=fc.""TableMetadataId""  and fc.""IsDeleted""=false
    //         //where c.""TableMetadataId""='{tableMetaData.Id}'
    //         //and c.""IsForeignKey""=true and c.""IsDeleted""=false";
    //
    //         //var result = await _queryRepo.ExecuteQueryList<ColumnMetadataViewModel>(query, null);
    //         var result = await cmsQueryBusiness.GetForeignKeyColumnByTableMetadata(tableMetaData);
    //
    //         if (result != null && result.Count() > 0)
    //         {
    //             foreach (var item in result)
    //             {
    //                 var tableName = @$"""{item.TableAliasName}""";
    //                 if (item.IsUdfColumn)
    //                 {
    //                     tableName = @$"""{item.TableAliasName}_{item.ForeignKeyColumnName}""";
    //                 }
    //
    //                 columns.Add(
    //                     @$"{Environment.NewLine}{tableName}.""{item.Name}"" as ""{item.ForeignKeyColumnName}_{item.Name}""");
    //             }
    //         }
    //     }
    //
    //     var cols = "*";
    //     if (columns.Any())
    //     {
    //         cols = string.Join(",", columns);
    //     }
    //
    //     var selectQuery =
    //         @$"select {cols}  {Environment.NewLine}from {string.Join(Environment.NewLine, tables)}{Environment.NewLine}where {string.Join(Environment.NewLine + "and ", condition)}";
    //     if (where.IsNotNullAndNotEmpty())
    //     {
    //         where = where.Replace(",", "','");
    //         selectQuery = $"{selectQuery} {where}";
    //     }
    //
    //     if (ignoreJoins)
    //     {
    //         if (returnColumns.IsNullOrEmpty())
    //         {
    //             returnColumns = "*";
    //         }
    //         else
    //         {
    //             returnColumns = @$"""{returnColumns.Replace(",", "\",\"")}""";
    //         }
    //
    //         selectQuery =
    //             @$"select {returnColumns} from {tableMetaData.Schema}.""{tableMetaData.Name}"" where ""IsDeleted""=false ";
    //         if (where.IsNotNullAndNotEmpty())
    //         {
    //             selectQuery = $"{selectQuery} {where}";
    //         }
    //     }
    //
    //     return selectQuery;
    // }
    //
    // private async Task<string> GetDataByColumn(ColumnMetadataViewModel column, object columnValue,
    //     TableMetadataViewModel tableMetaData, Guid excludeId)
    // {
    //     var dt = await cmsQueryBusiness.GetDataByColumn(column, columnValue, tableMetaData, excludeId);
    //     if (dt.Rows.Count > 0)
    //     {
    //         var dr = dt.Rows[0];
    //         var dict = dr.Table.Columns
    //             .Cast<DataColumn>()
    //             .ToDictionary(c => c.ColumnName, c => dr[c]);
    //         var j = JsonConvert.SerializeObject(dict);
    //         return j;
    //     }
    //
    //     return string.Empty;
    // }
    //
    //
    // public async Task<CommandResult<TemplateViewModel>> ManageForm(TemplateViewModel model)
    // {
    //     if (model.DataAction == DataActionEnum.Create)
    //     {
    //         // var presubmit = await ManagePresubmit(model);
    //         // if (!presubmit.IsSuccess)
    //         // {
    //         //     return presubmit;
    //         // }
    //
    //         var result = await CreateForm(model.Json, model.PageId, model.TemplateCode);
    //         if (result.Item1)
    //         {
    //             model.RecordId = result.Item2;
    //             var table = await _repo.GetSingle<TemplateViewModel, Template>(x => x.Code == model.TemplateCode);
    //             var tableMetaData =
    //                 await _repo.GetSingleById<TableMetadataViewModel, TableMetadata>(table.TableMetadataId);
    //             var selectQuery = await GetSelectQuery(tableMetaData,
    //                 @$" and ""{tableMetaData.Name}"".""Id""='{model.RecordId}' limit 1");
    //             // dynamic dobject = await _queryRepo.ExecuteQuerySingleDynamicObject(selectQuery, null);
    //             // try
    //             // {
    //             //     await ManageBusinessRule(BusinessLogicExecutionTypeEnum.PostSubmit, model, dobject);
    //             // }
    //             // catch (Exception ex)
    //             // {
    //             //     return CommandResult<TemplateViewModel>.Instance(model, false,
    //             //         $"Error in post submit business rule execution: {ex.Message}");
    //             // }
    //
    //             return await Task.FromResult(CommandResult<TemplateViewModel>.Instance(model));
    //         }
    //
    //         return await Task.FromResult(
    //             CommandResult<TemplateViewModel>.Instance(model, result.Item1, result.Item2));
    //     }
    //
    //     if (model.DataAction == DataActionEnum.Delete)
    //     {
    //         await DeleteFrom(model);
    //         return await Task.FromResult(CommandResult<TemplateViewModel>.Instance(model));
    //     }
    //
    //     if (model.DataAction == DataActionEnum.Edit)
    //     {
    //         // var presubmit = await ManagePresubmit(model);
    //         // if (!presubmit.IsSuccess)
    //         // {
    //         //     return presubmit;
    //         // }
    //
    //         var result = await EditForm(model.RecordId, model.Json, model.PageId, model.TemplateCode);
    //         if (result.Item1)
    //         {
    //             model.RecordId = result.Item2;
    //             var table = await _repo.GetSingle<TemplateViewModel, Template>(x => x.Code == model.TemplateCode);
    //             var tableMetaData =
    //                 await _repo.GetSingleById<TableMetadataViewModel, TableMetadata>(table.TableMetadataId);
    //             var selectQuery = await GetSelectQuery(tableMetaData,
    //                 @$" and ""{tableMetaData.Name}"".""Id""='{model.RecordId}' limit 1");
    //             dynamic dobject = await _queryRepo.ExecuteQuerySingleDynamicObject(selectQuery, null);
    //             try
    //             {
    //                 await ManageBusinessRule(BusinessLogicExecutionTypeEnum.PostSubmit, model, dobject);
    //             }
    //             catch (Exception ex)
    //             {
    //                 return CommandResult<TemplateViewModel>.Instance(model, false,
    //                     $"Error in post submit business rule execution: {ex.Message}");
    //             }
    //
    //             return await Task.FromResult(CommandResult<TemplateViewModel>.Instance(model));
    //         }
    //
    //         return await Task.FromResult(
    //             CommandResult<TemplateViewModel>.Instance(model, result.Item1, result.Item2));
    //     }
    //
    //     return await Task.FromResult(CommandResult<TemplateViewModel>.Instance(model, false,
    //         "An error occured while processing your request"));
    // }
    //
    //
    // private async Task<string> DeleteFrom(TemplateViewModel model)
    // {
    //     var page = await _pageBusiness.GetPageForExecution(model.PageId);
    //     if (page != null && page.Template != null)
    //     {
    //         var id = Guid.NewGuid().ToString();
    //         var tableMetaData =
    //             await _repo.GetSingle<TableMetadataViewModel, TableMetadata>(x =>
    //                 x.Id == page.Template.TableMetadataId);
    //         if (tableMetaData != null)
    //         {
    //             var tableColumns =
    //                 await _repo.GetList<ColumnMetadataViewModel, ColumnMetadata>(x =>
    //                     x.TableMetadataId == tableMetaData.Id);
    //             if (tableColumns != null && tableColumns.Count > 0)
    //             {
    //                 //var selectQuery = new StringBuilder(@$"update {ApplicationConstant.Database.Schema.Cms}.""{tableMetaData.Name}"" set ");
    //                 //selectQuery.Append(Environment.NewLine);
    //                 //selectQuery.Append(@$"""IsDeleted""=true,{Environment.NewLine}");
    //                 //selectQuery.Append(@$"""LastUpdatedDate""='{DateTime.Now.ToDatabaseDateFormat()}',{Environment.NewLine}");
    //                 //selectQuery.Append(@$"""LastUpdatedBy""='{repo.UserContext.UserId}'{Environment.NewLine}");
    //                 //selectQuery.Append(@$"where ""Id""='{model.RecordId}'");
    //                 //var queryText = selectQuery.ToString();
    //                 //await _queryRepo.ExecuteCommand(queryText, null);
    //
    //                 await cmsQueryBusiness.DeleteFrom(model, tableMetaData);
    //             }
    //         }
    //
    //         return await Task.FromResult(id);
    //     }
    //
    //     return await Task.FromResult(string.Empty);
    // }
    //
    // public async Task<Tuple<bool, string>> CreateForm(string data, string pageId, string templateCode = null)
    // {
    //     Template template = null;
    //     if (templateCode.IsNotNullAndNotEmpty())
    //     {
    //         template = await _repo.GetSingle(x => x.Code == templateCode);
    //     }
    //
    //     // var page = await _pageBusiness.GetPageForExecution(pageId);
    //     if (template != null)
    //     {
    //         var rowData = JsonConvert.DeserializeObject<Dictionary<string, object>>(data);
    //         var id = "";
    //         if (rowData.ContainsKey("Id"))
    //         {
    //             id = Convert.ToString(rowData["Id"]);
    //         }
    //         else
    //         {
    //             id = Guid.NewGuid().ToString();
    //         }
    //
    //         if (id.IsNullOrEmpty())
    //         {
    //             id = Guid.NewGuid().ToString();
    //         }
    //
    //         var tableMetaData =
    //             await _repo.GetSingle<TableMetadataViewModel, TableMetadata>(x => x.Id == template.TableMetadataId);
    //         if (tableMetaData != null)
    //         {
    //             var tableColumns =
    //                 await _repo.GetList<ColumnMetadataViewModel, ColumnMetadata>(x =>
    //                     x.TableMetadataId == tableMetaData.Id);
    //             if (tableColumns != null && tableColumns.Count > 0)
    //             {
    //                 var validate = await ValidateForm(data, pageId, rowData, tableMetaData, tableColumns);
    //                 if (validate.IsNotNullAndNotEmpty())
    //                 {
    //                     return await Task.FromResult(new Tuple<bool, string>(false, validate));
    //                 }
    //
    //                 var selectQuery =
    //                     new StringBuilder(
    //                         @$"insert into {ApplicationConstant.Database.Schema.Cms}.""{tableMetaData.Name}""");
    //                 var columnKeys = new List<string>();
    //                 columnKeys.Add(@"""Id""");
    //                 var columnValues = new List<object>();
    //                 columnValues.Add(@$"'{id}'");
    //                 foreach (var col in tableColumns.Where(x =>
    //                              x.IsSystemColumn == false && x.UdfUIType != UdfUITypeEnum.editgrid &&
    //                              x.UdfUIType != UdfUITypeEnum.datagrid))
    //                 {
    //                     if (col.Name != "Id")
    //                     {
    //                         columnKeys.Add(@$"""{col.Name}""");
    //                         columnValues.Add(BusinessHelper.ConvertToDbValue(rowData.GetValueOrDefault(col.Name),
    //                             col.IsSystemColumn, col.DataType));
    //                     }
    //                 }
    //
    //                 foreach (var col in tableColumns.Where(x =>
    //                              x.UdfUIType == UdfUITypeEnum.editgrid || x.UdfUIType == UdfUITypeEnum.datagrid))
    //                 {
    //                     if (col.Name != "Id")
    //                     {
    //                         var gridvalue = rowData.GetValueOrDefault(col.Name);
    //                         if (gridvalue != null)
    //                         {
    //                             columnKeys.Add(@$"""{col.Name}""");
    //                             JArray gridData = JArray.Parse(gridvalue.ToString());
    //                             foreach (JObject jcomp in gridData)
    //                             {
    //                                 foreach (var item in jcomp)
    //                                 {
    //                                     var val = item.Value.ToString();
    //                                     var key = item.Key;
    //                                     var convertedValue =
    //                                         BusinessHelper.ConvertToDbValue(val, false, col.DataType);
    //                                     if (convertedValue != "null")
    //                                     {
    //                                         var value = convertedValue.ToString();
    //                                         value = value.Replace("'", "");
    //                                         jcomp[key] = value;
    //                                     }
    //                                 }
    //                             }
    //
    //                             var colValue = JsonConvert.SerializeObject(gridData);
    //                             columnValues.Add(@$"'{colValue}'");
    //                         }
    //                     }
    //                 }
    //
    //                 columnKeys.Add(@$"""SequenceOrder""");
    //                 var sq = rowData.GetValueOrDefault("SequenceOrder");
    //                 if (sq == null)
    //                 {
    //                     sq = 1;
    //                 }
    //
    //                 columnValues.Add(sq);
    //                 columnKeys.Add(@"""CreatedDate""");
    //                 columnKeys.Add(@"""CreatedBy""");
    //                 columnKeys.Add(@"""LastUpdatedDate""");
    //                 columnKeys.Add(@"""LastUpdatedBy""");
    //                 columnKeys.Add(@"""IsDeleted""");
    //                 columnKeys.Add(@"""CompanyId""");
    //                 columnKeys.Add(@"""LegalEntityId""");
    //                 //columnKeys.Add(@"""SequenceOrder""");
    //                 columnKeys.Add(@"""Status""");
    //                 columnKeys.Add(@"""VersionNo""");
    //
    //                 columnValues.Add(@$"'{DateTime.Now.ToDatabaseDateFormat()}'");
    //                 columnValues.Add(@$"'{_repo.UserContext.UserId}'");
    //                 columnValues.Add(@$"'{DateTime.Now.ToDatabaseDateFormat()}'");
    //                 columnValues.Add(@$"'{_repo.UserContext.UserId}'");
    //                 columnValues.Add(@$"false");
    //                 if (rowData.ContainsKey("CompanyId"))
    //                 {
    //                     columnValues.Add(@$"'{Convert.ToString(rowData["CompanyId"])}'");
    //                 }
    //                 else
    //                 {
    //                     columnValues.Add(@$"'{_repo.UserContext.CompanyId}'");
    //                 }
    //
    //                 if (rowData.ContainsKey("LegalEntityId"))
    //                 {
    //                     columnValues.Add(@$"'{Convert.ToString(rowData["LegalEntityId"])}'");
    //                 }
    //                 else
    //                 {
    //                     columnValues.Add(@$"'{_repo.UserContext.LegalEntityId}'");
    //                 }
    //
    //                 //columnValues.Add(@$"1");
    //                 columnValues.Add(@$"{(int)StatusEnum.Active}");
    //                 columnValues.Add(@$"1");
    //                 selectQuery.Append(Environment.NewLine);
    //                 selectQuery.Append($"({string.Join(", ", columnKeys)})");
    //                 selectQuery.Append(Environment.NewLine);
    //                 selectQuery.Append($"values ({string.Join(", ", columnValues)})");
    //                 var queryText = selectQuery.ToString();
    //
    //                 //await _queryRepo.ExecuteCommand(queryText, null);
    //                 await cmsQueryBusiness.TableQueryExecute(queryText);
    //             }
    //
    //             var childTable = tableColumns.Where(x =>
    //                 x.UdfUIType == UdfUITypeEnum.editgrid || x.UdfUIType == UdfUITypeEnum.datagrid).ToList();
    //             foreach (var child in childTable)
    //             {
    //                 var gridData = rowData.GetValueOrDefault(child.Name);
    //                 if (gridData != null)
    //                 {
    //                     //data = gridData.ToString();
    //                     //data = data.Replace("[", "").Replace("]", "");
    //                     //JArray result = JArray.FromObject(gridData);
    //                     JArray result = JArray.Parse(gridData.ToString());
    //                     foreach (JObject jcomp in result)
    //                     {
    //                         if (jcomp.Count > 0)
    //                         {
    //                             jcomp.Remove("ParentId");
    //                             var newProperty = new JProperty("ParentId", id);
    //                             jcomp.Add(newProperty);
    //                             //jcomp.SelectToken("ParentId").Replace(JToken.FromObject(id));
    //                             var record = await CreateForm(jcomp.ToString(), null, child.Name);
    //                             //jcomp.SelectToken("Id").Replace(JToken.FromObject(record.Item2));
    //                         }
    //                     }
    //                     //var query = @$"update {ApplicationConstant.Database.Schema.Cms}.""{tableMetaData.Name}"" set ""{child.Name}""='{result}'
    //                     //                    where ""Id""='{id}' ";
    //
    //                     //await _cmsQueryBusiness.TableQueryExecute(query);
    //                 }
    //             }
    //         }
    //
    //         return await Task.FromResult(new Tuple<bool, string>(true, id));
    //     }
    //
    //     return await Task.FromResult(new Tuple<bool, string>(false, "Page does not exist."));
    // }
    //
    // //private async Task ManageLocalization(Dictionary<string, object> rowData, string id)
    // //{
    // //    var obj = Convert.ToString(rowData.GetValueOrDefault("Localization"));
    // //    var localizedData = JsonConvert.DeserializeObject<Dictionary<string, object>>(obj);
    // //    if (localizedData != null && localizedData.Any())
    // //    {
    // //        string hindi = null;
    // //        if (localizedData.ContainsKey("Hindi"))
    // //        {
    // //            hindi = Convert.ToString(localizedData["Hindi"]);
    // //        }
    // //        string arabic = null;
    // //        if (localizedData.ContainsKey("Arabic"))
    // //        {
    // //            arabic = Convert.ToString(localizedData["Arabic"]);
    // //        }
    // //        var exist = await repo.GetSingle<FormResourceLanguageViewModel, FormResourceLanguage>(x => x.FormTableId == id);
    // //        if (exist == null)
    // //        {
    // //            await repo.Create<FormResourceLanguageViewModel, FormResourceLanguage>(new FormResourceLanguageViewModel
    // //            {
    // //                Hindi = hindi,
    // //                Arabic = arabic,
    // //                FormTableId = id
    // //            });
    // //        }
    // //        else
    // //        {
    // //            if (hindi != null)
    // //            {
    // //                exist.Hindi = hindi;
    // //            }
    // //            if (arabic != null)
    // //            {
    // //                exist.Arabic = arabic;
    // //            }
    // //            await repo.Edit<FormResourceLanguage, FormResourceLanguage>(exist);
    // //        }
    // //    }
    // //}
    //
    // private async Task<string> ValidateForm(string data, string pageId, Dictionary<string, object> rowData,
    //     TableMetadataViewModel tableMetaData, List<ColumnMetadataViewModel> tableColumns, Guid excludeId = null)
    // {
    //     var uniqueColumns = tableColumns.Where(x => x.IsUniqueColumn).ToList();
    //     foreach (var item in uniqueColumns)
    //     {
    //         var value = rowData.GetValueOrDefault(item.Name);
    //         var exist = await GetDataByColumn(item, value, tableMetaData, excludeId);
    //         if (exist.IsNotNullAndNotEmpty())
    //         {
    //             return await Task.FromResult(
    //                 @$"An item already exists with '{item.Alias}' as '{value}'. Please enter another value");
    //         }
    //     }
    //
    //     return string.Empty;
    // }
    //
    //
    // public async Task<Tuple<bool, string>> EditForm(Guid recordId, string data, string pageId,
    //     string templateCode = null)
    // {
    //     Template template = await _repo.GetSingle(x => x.Code == templateCode);
    //
    //     var rowData = JsonConvert.DeserializeObject<Dictionary<string, object>>(data);
    //     var tableMetaData =
    //         await _repo.GetSingle<TableMetadataViewModel, TableMetadata>(x => x.Id == template.TableMetadataId);
    //     if (tableMetaData != null)
    //     {
    //         var tableColumns =
    //             await _repo.GetList<ColumnMetadataViewModel, ColumnMetadata>(x =>
    //                 x.TableMetadataId == tableMetaData.Id);
    //         if (tableColumns != null && tableColumns.Count > 0)
    //         {
    //             var validate = await ValidateForm(data, pageId, rowData, tableMetaData, tableColumns, recordId);
    //             if (validate.IsNotNullAndNotEmpty())
    //             {
    //                 return await Task.FromResult(new Tuple<bool, string>(false, validate));
    //             }
    //
    //             var selectQuery =
    //                 new StringBuilder(
    //                     @$"update {ApplicationConstant.Database.Schema.Cms}.""{tableMetaData.Name}""");
    //             selectQuery.Append(Environment.NewLine);
    //             selectQuery.Append("set");
    //             selectQuery.Append(Environment.NewLine);
    //             var columnKeys = new List<string>();
    //             foreach (var col in tableColumns.Where(x =>
    //                          x.IsUdfColumn && x.UdfUIType != UdfUITypeEnum.editgrid &&
    //                          x.UdfUIType != UdfUITypeEnum.datagrid))
    //             {
    //                 columnKeys.Add(
    //                     @$"""{col.Name}"" = {BusinessHelper.ConvertToDbValue(rowData.GetValueOrDefault(col.Name), col.IsSystemColumn, col.DataType)}");
    //             }
    //
    //             foreach (var col in tableColumns.Where(x =>
    //                          x.UdfUIType == UdfUITypeEnum.editgrid || x.UdfUIType == UdfUITypeEnum.datagrid))
    //             {
    //                 if (col.Name != "Id")
    //                 {
    //                     var gridvalue = rowData.GetValueOrDefault(col.Name);
    //                     if (gridvalue != null)
    //                     {
    //                         JArray gridData = JArray.Parse(gridvalue.ToString());
    //                         foreach (JObject jcomp in gridData)
    //                         {
    //                             foreach (var item in jcomp)
    //                             {
    //                                 var val = item.Value.ToString();
    //                                 var key = item.Key;
    //                                 var convertedValue =
    //                                     BusinessHelper.ConvertToDbValue(val, false, col.DataType);
    //                                 if (convertedValue != "null")
    //                                 {
    //                                     var value = convertedValue.ToString();
    //                                     value = value.Replace("'", "");
    //                                     jcomp[key] = value;
    //                                 }
    //                             }
    //                         }
    //
    //                         var colValue = JsonConvert.SerializeObject(gridData);
    //                         columnKeys.Add(@$"""{col.Name}"" = '{colValue}'");
    //                     }
    //                 }
    //             }
    //
    //             columnKeys.Add(@$"""LastUpdatedBy"" = '{_userContext.UserId}'");
    //             columnKeys.Add(@$"""LastUpdatedDate"" = '{DateTime.Now.ToDatabaseDateFormat()}'");
    //             selectQuery.Append($"{string.Join(",", columnKeys)}");
    //             selectQuery.Append(Environment.NewLine);
    //             selectQuery.Append(@$"where  ""Id"" = '{recordId}'");
    //             var queryText = selectQuery.ToString();
    //
    //             //await _queryRepo.ExecuteCommand(queryText, null);
    //             await cmsQueryBusiness.TableQueryExecute(queryText);
    //
    //             var childTable = tableColumns.Where(x =>
    //                 x.UdfUIType == UdfUITypeEnum.editgrid || x.UdfUIType == UdfUITypeEnum.datagrid).ToList();
    //             foreach (var child in childTable)
    //             {
    //                 var gridData = rowData.GetValueOrDefault(child.Name);
    //                 if (gridData != null)
    //                 {
    //                     //JArray result = JArray.FromObject(gridData);
    //                     JArray result = JArray.Parse(gridData.ToString());
    //                     var list = JsonConvert.DeserializeObject<IList<IdNameViewModel>>(gridData.ToString());
    //                     var ids = list.Select(x => x.Id).ToList();
    //                     var tableName =
    //                         await _repo.GetSingle<TemplateViewModel, Template>(x => x.Code == child.Name);
    //                     var delRec = await GetData("cms", tableName.Name, "ParentId", recordId);
    //                     var delList = (from DataRow dr in delRec.Rows
    //                         where !ids.Contains(dr["Id"].ToString())
    //                         select new IdNameViewModel()
    //                         {
    //                             Id = dr["Id"].ToString(),
    //                         }).ToList();
    //                     foreach (var del in delList)
    //                     {
    //                         await cmsQueryBusiness.DeleteFrom(new TemplateViewModel { RecordId = del.Id },
    //                             new TableMetadataViewModel { Name = tableName.Name });
    //                     }
    //
    //                     foreach (JObject jcomp in result)
    //                     {
    //                         if (jcomp.Count > 0)
    //                         {
    //                             var dataId = jcomp.GetValue("Id");
    //                             if (dataId.IsNotNull() &&
    //                                 dataId.ToString().IsNotNullAndNotEmptyAndNotValue("null"))
    //                             {
    //                                 await EditForm(dataId.ToString(), jcomp.ToString(), null, child.Name);
    //                             }
    //                             else
    //                             {
    //                                 jcomp.Remove("ParentId");
    //                                 var newProperty = new JProperty("ParentId", recordId);
    //                                 jcomp.Add(newProperty);
    //                                 //jcomp.SelectToken("ParentId").Replace(JToken.FromObject(recordId));
    //                                 await CreateForm(jcomp.ToString(), null, child.Name);
    //                             }
    //                         }
    //                     }
    //                     //var query = @$"update {ApplicationConstant.Database.Schema.Cms}.""{tableMetaData.Name}"" set ""{child.Name}""='{result}'
    //                     //                where ""Id""='{recordId}' ";
    //
    //                     //await _cmsQueryBusiness.TableQueryExecute(query);
    //                 }
    //             }
    //
    //             return await Task.FromResult(new Tuple<bool, string>(true, recordId));
    //         }
    //     }
    //
    //     return await Task.FromResult(new Tuple<bool, string>(false, "Page does not exist."));
    // }
    //
    // //public async Task<IList<EmailTaskViewModel>> ReadEmailTaskData(string refId, ReferenceTypeEnum refType)
    // //{
    // //    var taskdetails = new TaskViewModel();
    // //    if (refType == ReferenceTypeEnum.NTS_Task)
    // //    {
    // //        taskdetails = await _taskBusiness.GetSingle(x => x.Id == refId);
    // //    }
    //
    // //    //var queryData1 = await  _queryRepoEmailTask.ExecuteQueryList(query1, null);
    // //    var queryData1 = await _cmsQueryBusiness.ReadEmailTaskData(refId, refType, taskdetails);
    // //    return queryData1;
    // //}
    //
    //
    // public async Task<TemplateViewModel> GetFormDetails(TemplateViewModel viewModel)
    // {
    //     var template = await _repo.GetSingle<TemplateViewModel, Template>(x => x.Id == viewModel.Id);
    //     if (template != null)
    //     {
    //         viewModel.TableMetadataId = template.TableMetadataId;
    //     }
    //
    //     viewModel.ColumnList = await _repo.GetList<ColumnMetadataViewModel, ColumnMetadata>(x =>
    //         x.TableMetadataId == viewModel.TableMetadataId
    //         && x.IsVirtualColumn == false && x.IsHiddenColumn == false && x.IsLogColumn == false &&
    //         x.IsPrimaryKey == false);
    //
    //
    //     return viewModel;
    // }
    //
    //
    // // private void ChildComp(JArray comps, List<ColumnMetadataViewModel> Columns, TemplateViewModel model)
    // // {
    // //     foreach (JObject jcomp in comps)
    // //     {
    // //         var typeObj = jcomp.SelectToken("type");
    // //         var keyObj = jcomp.SelectToken("key");
    // //         if (typeObj.IsNotNull())
    // //         {
    // //             var type = typeObj.ToString();
    // //             var key = keyObj.ToString();
    // //             if (type == "textfield" || type == "textarea" || type == "number" || type == "password"
    // //                 || type == "selectboxes" || type == "checkbox" || type == "select" || type == "radio"
    // //                 || type == "email" || type == "url" || type == "phoneNumber" || type == "tags"
    // //                 || type == "datetime" || type == "day" || type == "time" || type == "currency" ||
    // //                 type == "button"
    // //                 || type == "signature" || type == "file" || type == "hidden" || type == "datagrid" ||
    // //                 type == "editgrid" || (type == "htmlelement" && key == "chartgrid") ||
    // //                 (type == "htmlelement" && key == "chartJs"))
    // //             {
    // //                 var reserve = jcomp.SelectToken("reservedKey");
    // //                 if (reserve == null)
    // //                 {
    // //                     var tempmodel = JsonConvert.DeserializeObject<FormFieldViewModel>(jcomp.ToString());
    // //                     var columnId = jcomp.SelectToken("columnMetadataId");
    // //                     if (columnId != null)
    // //                     {
    // //                         var columnMeta = Columns.FirstOrDefault(x => x.Name == tempmodel.key);
    // //                         if (columnMeta != null)
    // //                         {
    // //                             ///columnMeta.ActiveUserType = model.ActiveUserType;
    // //                             //columnMeta.NtsStatusCode = model.NoteStatusCode;
    // //                             var isReadonly = false;
    // //                             if (model.ReadoOnlyUdfs != null && model.ReadoOnlyUdfs.ContainsKey(columnMeta.Name))
    // //                             {
    // //                                 isReadonly = model.ReadoOnlyUdfs.GetValueOrDefault(columnMeta.Name);
    // //                             }
    // //
    // //                             var udfValue = new JProperty("udfValue", columnMeta.Value);
    // //                             jcomp.Add(udfValue);
    // //
    // //                             //Set default value
    // //                             if (model.Udfs != null && model.Udfs.GetValue(columnMeta.Name) != null)
    // //                             {
    // //                                 var newProperty = new JProperty("defaultValue",
    // //                                     model.Udfs.GetValue(columnMeta.Name));
    // //                                 jcomp.Add(newProperty);
    // //                             }
    // //                             //if (!columnMeta.IsVisible)
    // //                             //{
    // //                             //    var hiddenproperty = jcomp.SelectToken("hidden");
    // //                             //    if (hiddenproperty == null)
    // //                             //    {
    // //                             //        var newProperty = new JProperty("hidden", true);
    // //                             //        jcomp.Add(newProperty);
    // //                             //    }
    // //
    // //                             //}
    // //                             //if (!columnMeta.IsEditable || isReadonly)
    // //                             //{
    // //                             //    var newhiddenproperty = jcomp.SelectToken("disabled");
    // //                             //    if (newhiddenproperty == null)
    // //                             //    {
    // //                             //        var newProperty = new JProperty("disabled", true);
    // //                             //        jcomp.Add(newProperty);
    // //                             //    }
    // //                             //}
    // //                         }
    // //                     }
    // //                 }
    // //             }
    // //             else if (type == "columns")
    // //             {
    // //                 JArray cols = (JArray)jcomp.SelectToken("columns");
    // //                 foreach (var col in cols)
    // //                 {
    // //                     JArray rows = (JArray)col.SelectToken("components");
    // //                     if (rows != null)
    // //                         ChildComp(rows, Columns, model);
    // //                 }
    // //             }
    // //             else if (type == "table")
    // //             {
    // //                 JArray cols = (JArray)jcomp.SelectToken("rows");
    // //                 foreach (var col in cols)
    // //                 {
    // //                     JArray rows = (JArray)col.SelectToken("components");
    // //                     if (rows != null)
    // //                         ChildComp(rows, Columns, model);
    // //                 }
    // //             }
    // //             else
    // //             {
    // //                 JArray rows = (JArray)jcomp.SelectToken("components");
    // //                 if (rows != null)
    // //                     ChildComp(rows, Columns, model);
    // //             }
    // //         }
    // //     }
    // // }
}