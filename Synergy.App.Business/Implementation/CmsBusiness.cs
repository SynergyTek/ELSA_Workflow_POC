//using AutoMapper;
//using Synergy.App.Common;
//using Synergy.App.DataModel;
//using Synergy.App.Repository;
//using Synergy.App.ViewModel;
//////using Kendo.Mvc.UI;
//using Microsoft.EntityFrameworkCore;
//using Newtonsoft.Json;
//using Newtonsoft.Json.Linq;
//using Npgsql;
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Synergy.App.Business.Interface;
//using Synergy.App.Data.ViewModels;
//using Synergy.App.Data.Models;
//using Synergy.App.Business.Implementation;
//using Synergy.App.Data;

//namespace Synergy.App.Business
//{
//    public class CmsBusiness : BaseBusiness<TemplateViewModel, Template>, ICmsBusiness
//    {
//        private readonly IRepositoryQueryBase<TemplateViewModel> _queryRepo;
  
  
//        private readonly IUserContext _userContext;
//      //  private readonly ICmsQueryBusiness _cmsQueryBusiness;
//        private readonly IServiceProvider _serviceProvider;
//        public CmsBusiness(IRepositoryBase<TemplateViewModel, Template> repo
//            , IMapper autoMapper
//            , IRepositoryQueryBase<TemplateViewModel> queryRepo
      
//            , IUserContext userContext
//            , IServiceProvider serviceProvider
//           /* , ICmsQueryBusiness cmsQueryBusiness*/) : base(repo, autoMapper)
//        {
//            _queryRepo = queryRepo;
           
//            _userContext = userContext;
//         //   _cmsQueryBusiness = cmsQueryBusiness;
//            _serviceProvider = serviceProvider;
//        }

//        public async Task ManageTable(TableMetadataViewModel tableMetadata)
//        {
//            var existingTableMetadata = await _repo.GetSingleById<TableMetadataViewModel, TableMetadata>(tableMetadata.Id);
//            if (existingTableMetadata != null)
//            {
//                existingTableMetadata.OldName = tableMetadata.OldName;
//                existingTableMetadata.OldSchema = tableMetadata.OldSchema;
//                //var tableExists = await _queryRepo.ExecuteScalar<bool>(@$"select exists 
//                //(
//                // select 1
//                // from information_schema.{(existingTableMetadata.TableType == TableTypeEnum.Table ? "tables" : "views")} 
//                // where table_schema = '{existingTableMetadata.Schema}'
//                // and table_name = '{existingTableMetadata.Name}'
//                //) ", null);

//                var tableExists = await _cmsQueryBusiness.ManageTableExists(existingTableMetadata);
//                if (tableExists)
//                {

//                    //var recordExists = await _queryRepo.ExecuteScalar<bool?>(@$"select true from  
//                    //{existingTableMetadata.Schema}.""{existingTableMetadata.Name}"" limit 1 ", null);

//                    var recordExists = await _cmsQueryBusiness.ManageTableRecordExists(existingTableMetadata);
//                    if (recordExists.IsTrue())
//                    {
//                        await EditTable(existingTableMetadata);
//                    }
//                    else
//                    {
//                        await CreateTable(existingTableMetadata, true);
//                    }

//                }
//                else
//                {
//                    await CreateTable(existingTableMetadata, false);
//                }

//            }

//        }


//        private async Task EditTable(TableMetadataViewModel tableMetadata)
//        {
            
              
//                        await EditFormTable(tableMetadata);
                     
                 
                
            
      
//        }

//        private async Task CreateTable(TableMetadataViewModel tableMetadata, bool dropTable)
//        {

    
//                        await CreateFormTable(tableMetadata, dropTable);

 
//        }

//        private async Task CreateFormTable(TableMetadataViewModel tableMetadata, bool dropTable)
//        {
//            var tableVarWithSchema = "<<table-schema>>";
//            var tableVar = "<<table>>";

//            var columns = await _repo.GetList<ColumnMetadata, ColumnMetadata>(x => x.TableMetadataId == tableMetadata.Id && x.IsVirtualColumn == false);
//            var query = new StringBuilder();
//            if (dropTable)
//            {
//                query.Append($"DROP TABLE {tableVarWithSchema};");
//            }
//            query.Append($"CREATE TABLE {tableVarWithSchema}(");
//            var pk = "";
//            var fk = "";
//            var columnStr = new List<string>();
//            foreach (var column in columns)
//            {
//                var columnText = "";
//                var type = ConvertToPostgreType(column);

//                if (column.IsPrimaryKey)
//                {
//                    pk = @$", CONSTRAINT ""PK_{tableVar}"" PRIMARY KEY (""{column.Name}"")";
//                }
//                if (column.IsForeignKey && column.IsVirtualForeignKey == false && column.IsMultiValueColumn == false)
//                {
//                    fk += $@"CONSTRAINT ""{column.ForeignKeyConstraintName}"" FOREIGN KEY (""{column.Name}"") REFERENCES {column.ForeignKeyTableSchemaName}.""{column.ForeignKeyTableName}"" (""{column.ForeignKeyColumnName}"") MATCH SIMPLE ON UPDATE NO ACTION  ON DELETE SET NULL,";
//                }
//                columnText = $"\"{column.Name}\" {type}";
//                if (column.DataType == Data.DataColumnTypeEnum.Text)
//                {
//                    columnText = string.Concat(columnText, $" COLLATE {ApplicationConstant.Database.Schema._Public}.{ApplicationConstant.Database.DefaultCollation}");
//                }
//                if (!column.IsNullable)
//                {
//                    columnText = string.Concat(columnText, " NOT NULL");
//                }
//                columnStr.Add(columnText);
//            }
//            query.Append(string.Join(string.Concat(",", Environment.NewLine), columnStr));
//            query.Append(Environment.NewLine);
//            query.Append(pk);
//            if (fk.Trim(',').IsNotNullAndNotEmpty())
//            {
//                query.Append(",");
//            }
//            query.Append(fk.Trim(','));
//            query.Append(Environment.NewLine);
//            query.Append(')');
//            query.Append(Environment.NewLine);
//            query.Append($"TABLESPACE {ApplicationConstant.Database.TableSpace};");
//            query.Append(Environment.NewLine);
//            query.Append($"ALTER TABLE {tableVarWithSchema} OWNER to {ApplicationConstant.Database.Owner.Postgres};");

//            var tableWithSchema = $"{ApplicationConstant.Database.Schema.Cms}.\"{tableMetadata.Name}\"";
//            var table = $"{tableMetadata.Name}";
//            var tableQuery = query.ToString().Replace("<<table-schema>>", tableWithSchema).Replace("<<table>>", table);
//            //await _queryRepo.ExecuteCommand(tableQuery, null);

//            await _cmsQueryBusiness.TableQueryExecute(tableQuery);

//        }
//        private string ConvertToPostgreType(ColumnMetadata column)
//        {
//            if (column.IsSystemColumn)
//            {
//                return column.DataType switch
//                {
//                    Data.DataColumnTypeEnum.Text => "text",
//                    Data.DataColumnTypeEnum.Bool => "boolean",
//                    Data.DataColumnTypeEnum.DateTime => "timestamp without time zone",
//                    Data.DataColumnTypeEnum.Integer => "integer",
//                    Data.DataColumnTypeEnum.Double => "double precision",
//                    Data.DataColumnTypeEnum.Long => "bigint",
//                    Data.DataColumnTypeEnum.TextArray => "text[]",
//                    _ => "text",
//                };
//            }
//            else
//            {
//                return "text";
//            }
//        }

//        public async Task<DataTable> GetDataListByTemplate(string templateCode, string templateId, string where = null)
//        {
//            var template = await _repo.GetSingle<TemplateViewModel, Template>(x => x.Code == templateCode || x.Id == templateId);
//            if (template != null)
//            {
//                var tableMetadata = await _repo.GetSingleById<TableMetadataViewModel, TableMetadata>(template.UdfTableMetadataId ?? template.TableMetadataId);
//                if (tableMetadata != null)
//                {
//                    var selectQuery = await GetSelectQuery(tableMetadata, where);
//                    //var dt = await _queryRepo.ExecuteQueryDataTable(selectQuery, null);
//                    var dt = await _cmsQueryBusiness.GetQueryDataTable(selectQuery);
//                    return dt;
//                }
//            }

//            return null;
//        }
 
//        private async Task EditFormTable(TableMetadataViewModel tableMetadata)
//        {
//            //   var existColumn = await _queryRepo.ExecuteQueryDataTable(@$"select column_name,data_type,is_nullable	   
//            //   from information_schema.columns where table_schema = '{tableMetadata.Schema}'
//            //and table_name = '{tableMetadata.Name}'", null);

//            var existColumn = await _cmsQueryBusiness.GetEditFormTableExistColumn(tableMetadata);

//            //var constraints = await _queryRepo.ExecuteQueryDataTable(@$"SELECT con.conname as conname
//            //FROM pg_catalog.pg_constraint con
//            //INNER JOIN pg_catalog.pg_class rel ON rel.oid = con.conrelid
//            //INNER JOIN pg_catalog.pg_namespace nsp ON nsp.oid = connamespace
//            //WHERE nsp.nspname = '{tableMetadata.Schema}' AND rel.relname = '{tableMetadata.Name}';", null);

//            var constraints = await _cmsQueryBusiness.GetEditServiceTableConstraints(tableMetadata);

//            var tableColumnList = new List<ColumnMetadataViewModel>();
//            foreach (DataRow row in existColumn.Rows)
//            {
//                var data = new ColumnMetadataViewModel();
//                data.Name = row["column_name"].ToString();
//                data.DataTypestr = row["data_type"].ToString();
//                data.IsNullable = Convert.ToString(row["is_nullable"]) == "YES" ? true : false;
//                tableColumnList.Add(data);
//            }
//            var existingMetadaColumnList = await _repo.GetList<ColumnMetadata, ColumnMetadata>
//                (x => x.TableMetadataId == tableMetadata.Id && x.IsVirtualColumn == false);
//            var query = new StringBuilder($"ALTER TABLE {ApplicationConstant.Database.Schema.Cms}.\"{tableMetadata.Name}\"");
//            var alterColumnScriptList = new List<string>();
//            foreach (var existingMetadaColumn in existingMetadaColumnList)
//            {
//                var postgreType = ConvertToPostgreType(existingMetadaColumn);
//                var isInTable = tableColumnList.FirstOrDefault(x => x.Name == existingMetadaColumn.Name);
//                if (isInTable != null)
//                {
//                    if (postgreType != isInTable.DataTypestr)
//                    {
//                        alterColumnScriptList.Add($"{Environment.NewLine}ALTER COLUMN \"{existingMetadaColumn.Name}\" TYPE {postgreType}");
//                        ManageForeignKey(alterColumnScriptList, existingMetadaColumn, constraints);

//                    }
//                    else if (existingMetadaColumn.IsNullable != isInTable.IsNullable)
//                    {
//                        if (existingMetadaColumn.IsNullable)
//                        {
//                            alterColumnScriptList.Add($"{Environment.NewLine}ALTER COLUMN \"{existingMetadaColumn.Name}\"  DROP NOT NULL");
//                        }
//                        else
//                        {
//                            alterColumnScriptList.Add($"{Environment.NewLine}ALTER COLUMN \"{existingMetadaColumn.Name}\"  SET NOT NULL");

//                        }
//                    }

//                }
//                else
//                {
//                    var nullSet = "";
//                    if (!existingMetadaColumn.IsNullable)
//                    {
//                        nullSet = " NOT NULL";
//                    }


//                    if (existingMetadaColumn.DataType == Data.DataColumnTypeEnum.Text)
//                    {
//                        alterColumnScriptList.Add($"{Environment.NewLine}ADD COLUMN \"{existingMetadaColumn.Name}\" {postgreType} {nullSet} COLLATE {ApplicationConstant.Database.Schema._Public}.{ApplicationConstant.Database.DefaultCollation}");
//                    }
//                    else
//                    {
//                        alterColumnScriptList.Add($"{Environment.NewLine}ADD COLUMN \"{existingMetadaColumn.Name}\" {postgreType} {nullSet}");
//                    }
//                    ManageForeignKey(alterColumnScriptList, existingMetadaColumn, constraints);
//                }
//            }
//            if (alterColumnScriptList.Count > 0)
//            {
//                query.Append(string.Join(",", alterColumnScriptList));
//                query.Append(";");
//                var queryText = query.ToString();

//                //await _queryRepo.ExecuteCommand(queryText, null);
//                await _cmsQueryBusiness.TableQueryExecute(queryText);
//            }

//        }

//        private void ManageForeignKey(List<string> alterColumnScriptList, ColumnMetadata column, DataTable constraints)
//        {
//            if (column.IsForeignKey && column.IsVirtualForeignKey == false && column.ForeignKeyConstraintName.IsNotNullAndNotEmpty())
//            {
//                var existingFk = constraints.AsEnumerable().FirstOrDefault
//                    (r => r.Field<string>("conname") == column.ForeignKeyConstraintName);
//                if (existingFk != null)
//                {
//                    alterColumnScriptList.Add($@"DROP CONSTRAINT ""{column.ForeignKeyConstraintName}""");
//                }
//                alterColumnScriptList.Add($@"ADD CONSTRAINT ""{column.ForeignKeyConstraintName}"" FOREIGN KEY (""{column.Name}"") REFERENCES {column.ForeignKeyTableSchemaName}.""{column.ForeignKeyTableName}"" (""{column.ForeignKeyColumnName}"") MATCH SIMPLE ON UPDATE NO ACTION  ON DELETE SET NULL");
//            }
//        }



//        //public async Task<DataRow> GetDataById(string recordId, string udfTableMetadataId, string templateId, TemplateTypeEnum pageType)
//        //{
//        //    var baseQuery = "";
//        //    var selectQuery = "";
//        //    var dt = new DataTable();
//        //    TableMetadataViewModel tableMetaData = null;
//        //    switch (pageType)
//        //    {
//        //        case TemplateTypeEnum.Task:
//        //            if (udfTableMetadataId.IsNullOrEmpty())
//        //            {
//        //                var stepTaskComponent = await _repo.GetSingle<StepTaskComponentViewModel, StepTaskComponent>(x => x.TemplateId == templateId);
//        //                udfTableMetadataId = stepTaskComponent?.UdfTableMetadataId;
//        //            }
//        //            tableMetaData = await _repo.GetSingle<TableMetadataViewModel, TableMetadata>(x => x.Id == udfTableMetadataId);
//        //            dt = await _taskBusiness.GetTaskDataTableById(recordId, tableMetaData);
//        //            break;
//        //        case TemplateTypeEnum.Service:
//        //            tableMetaData = await _repo.GetSingle<TableMetadataViewModel, TableMetadata>(x => x.Id == udfTableMetadataId);
//        //            dt = await _serviceBusiness.GetServiceDataTableById(recordId, tableMetaData);
//        //            break;
//        //        case TemplateTypeEnum.Note:
//        //            tableMetaData = await _repo.GetSingle<TableMetadataViewModel, TableMetadata>(x => x.Id == udfTableMetadataId);
//        //            dt = await _noteBusiness.GetNoteDataTableById(recordId, tableMetaData);
//        //            break;
//        //        case TemplateTypeEnum.Form:
//        //            tableMetaData = await _repo.GetSingle<TableMetadataViewModel, TableMetadata>(x => x.Id == udfTableMetadataId);
//        //            baseQuery = await GetFormSelectQuery(tableMetaData);
//        //            selectQuery = @$"{baseQuery} and ""{tableMetaData.Name}"".""Id""='{recordId}' limit 1";
//        //            dt = await _cmsQueryBusiness.GetQueryDataTable(selectQuery);
//        //            break;
//        //        default:
//        //            tableMetaData = await _repo.GetSingle<TableMetadataViewModel, TableMetadata>(x => x.Id == udfTableMetadataId);
//        //            baseQuery = await GetSelectQuery(tableMetaData);
//        //            selectQuery = @$"{baseQuery} and ""{tableMetaData.Name}"".""Id""='{recordId}' limit 1";

//        //            //dt = await _queryRepo.ExecuteQueryDataTable(selectQuery, null);
//        //            dt = await _cmsQueryBusiness.GetQueryDataTable(selectQuery);

//        //            break;
//        //    }
//        //    if (dt != null && dt.Rows.Count > 0)
//        //    {
//        //        var dr = dt.Rows[0];
//        //        var dict = dr.Table.Columns.Cast<DataColumn>().ToDictionary(c => c.ColumnName, c => dr[c]);
//        //        var j = JsonConvert.SerializeObject(dict);
//        //        return dr;
//        //    }
//        //    return null;

//        //}

//        //public async Task<DataRow> GetDataById(TemplateTypeEnum viewName, PageViewModel page, string recordId, bool isLog = false, string logId = null)
//        //{
//        //    var baseQuery = "";
//        //    var selectQuery = "";
//        //    var dt = new DataTable();
//        //    TableMetadataViewModel tableMetaData = null;
//        //    var tablemetaid = "";
//        //    switch (page.PageType)
//        //    {
//        //        case TemplateTypeEnum.Task:
//        //            var udfTableMetadataId = page.Template.UdfTableMetadataId;
//        //            if (udfTableMetadataId.IsNullOrEmpty())
//        //            {
//        //                var stepTaskComponent = await _repo.GetSingle<StepTaskComponentViewModel, StepTaskComponent>(x => x.TemplateId == page.Template.Id);
//        //                udfTableMetadataId = stepTaskComponent?.UdfTableMetadataId;
//        //            }
//        //            tableMetaData = await _repo.GetSingle<TableMetadataViewModel, TableMetadata>(x => x.Id == udfTableMetadataId);
//        //            dt = await _taskBusiness.GetTaskDataTableById(recordId, tableMetaData, isLog, logId);
//        //            tablemetaid = udfTableMetadataId;
//        //            break;
//        //        case TemplateTypeEnum.Service:
//        //            tableMetaData = await _repo.GetSingle<TableMetadataViewModel, TableMetadata>(x => x.Id == page.Template.UdfTableMetadataId);
//        //            dt = await _serviceBusiness.GetServiceDataTableById(recordId, tableMetaData, isLog, logId);
//        //            tablemetaid = page.Template.UdfTableMetadataId;
//        //            break;
//        //        case TemplateTypeEnum.Note:
//        //            tableMetaData = await _repo.GetSingle<TableMetadataViewModel, TableMetadata>(x => x.Id == page.Template.TableMetadataId);
//        //            dt = await _noteBusiness.GetNoteDataTableById(recordId, tableMetaData, isLog, logId);
//        //            tablemetaid = page.Template.TableMetadataId;

//        //            break;
//        //        case TemplateTypeEnum.Form:
//        //            tableMetaData = await _repo.GetSingle<TableMetadataViewModel, TableMetadata>(x => x.Id == page.Template.TableMetadataId);
//        //            baseQuery = await GetFormSelectQuery(tableMetaData);
//        //            selectQuery = @$"{baseQuery} and ""{tableMetaData.Name}"".""Id""='{recordId}' limit 1";
//        //            dt = await _cmsQueryBusiness.GetQueryDataTable(selectQuery);
//        //            tablemetaid = page.Template.TableMetadataId;

//        //            break;
//        //        default:
//        //            tableMetaData = await _repo.GetSingle<TableMetadataViewModel, TableMetadata>(x => x.Id == page.Template.TableMetadataId);
//        //            baseQuery = await GetSelectQuery(tableMetaData);
//        //            selectQuery = @$"{baseQuery} and ""{tableMetaData.Name}"".""Id""='{recordId}' limit 1";

//        //            //dt = await _queryRepo.ExecuteQueryDataTable(selectQuery, null);
//        //            dt = await _cmsQueryBusiness.GetQueryDataTable(selectQuery);
//        //            tablemetaid = page.Template.TableMetadataId;

//        //            break;
//        //    }
//        //    if (dt != null && dt.Rows.Count > 0)
//        //    {
//        //        var dr = dt.Rows[0];
//        //        var dict = dr.Table.Columns.Cast<DataColumn>().ToDictionary(c => c.ColumnName, c => dr[c]);
//        //        var j = JsonConvert.SerializeObject(dict);
//        //        var tc = await _repo.GetList<ColumnMetadataViewModel, ColumnMetadata>(x => x.TableMetadataId == tablemetaid);
//        //        var childTable = tc.Where(x => x.UdfUIType == UdfUITypeEnum.editgrid || x.UdfUIType == UdfUITypeEnum.datagrid).ToList();
//        //        foreach (var child in childTable)
//        //        {
//        //            var tableName = await _repo.GetSingle<TemplateViewModel, Template>(x => x.Code == child.Name);
//        //            //var editRec = await GetDataById(TemplateTypeEnum.Form, new PageViewModel { PageType = TemplateTypeEnum.Form, Template = new Template { TableMetadataId = tableName.TableMetadataId } }, dataId.ToString());
//        //            var recId = "";
//        //            if (page.PageType == TemplateTypeEnum.Form)
//        //            {
//        //                recId = dr["Id"].ToString();
//        //            }
//        //            else
//        //            {
//        //                recId = dr["UdfNoteTableId"].ToString();
//        //            }
//        //            var editRec = await GetData(ApplicationConstant.Database.Schema.Cms, tableName.Name, "ParentId", recId);

//        //            if (editRec != null)
//        //            {
//        //                //if (editRec.Rows.Count > 0)
//        //                //{
//        //                var json = JsonConvert.SerializeObject(editRec);
//        //                dr[child.Name] = json;
//        //                //}
//        //            }
//        //        }
//        //        return dr;
//        //    }
//        //    return null;

//        //}
//        public async Task<DataTable> GetData(string schemaName, string tableName, string columns = null, string filter = null, string orderbyColumns = null, OrderByEnum orderby = OrderByEnum.Ascending, string where = null, bool ignoreJoins = false, string returnColumns = null, int? limit = null, int? skip = null, bool enableLocalization = false, string lang = null)
//        {
//            var tableMetaData = await _repo.GetSingle<TableMetadataViewModel, TableMetadata>(x => x.Schema == schemaName && x.Name == tableName);
            
         
//                   var selectQuery = await GetFormSelectQuery(tableMetaData, where, columns, filter, ignoreJoins, returnColumns, limit, skip, enableLocalization, lang);
                 
            
//            //return await _queryRepo.ExecuteQueryDataTable(selectQuery, null);
//            var dt = await _cmsQueryBusiness.GetQueryDataTable(selectQuery);
//            return dt;
//        }

//        private async Task<string> GetCustomSelectQuery(TableMetadataViewModel tableMetaData, string filtercolumns, string filter)
//        {
//            var columns = new List<string>();
//            var tables = new List<string>();
//            var condition = new List<string>();
//            var pkColumns = await _repo.GetList<ColumnMetadataViewModel, ColumnMetadata>(x => x.TableMetadataId == tableMetaData.Id
//               && x.IsVirtualColumn == false);
//            foreach (var item in pkColumns)
//            {
//                columns.Add(@$"{Environment.NewLine}""{tableMetaData.Name}"".""{item.Name}"" as ""{item.Name}""");
//            }
//            tables.Add(@$"{tableMetaData.Schema}.""{tableMetaData.Name}"" as ""{tableMetaData.Name}""");
//            condition.Add(@$"""{tableMetaData.Name}"".""IsDeleted""=false");
//            if (filtercolumns.IsNotNullAndNotEmpty() && filter.IsNotNullAndNotEmpty())
//            {
//                var filters = filtercolumns.Split(',');
//                var filterValues = filter.Split(',');
//                var i = 0;
//                foreach (var item in filters)
//                {
//                    var value = filterValues[i];
//                    condition.Add(@$"""{tableMetaData.Name}"".""{item}""='{value}'");
//                    i++;
//                }
//            }
//            //var fks = pkColumns.Where(x => x.IsForeignKey && x.ForeignKeyTableId.IsNotNullAndNotEmpty() && x.HideForeignKeyTableColumns == false).ToList();
//            //if (fks != null && fks.Count() > 0)
//            //{
//            //    foreach (var item in fks)
//            //    {
//            //        if (item.IsUdfColumn)
//            //        {
//            //            item.ForeignKeyTableAliasName = item.ForeignKeyTableName + "_" + item.Name;
//            //        }
//            //        tables.Add(@$"left join {item.ForeignKeyTableSchemaName}.""{item.ForeignKeyTableName}"" as ""{item.ForeignKeyTableAliasName}"" 
//            //        on ""{tableMetaData.Name}"".""{item.Name}""=""{item.ForeignKeyTableAliasName}"".""{item.ForeignKeyColumnName}"" 
//            //        and ""{item.ForeignKeyTableAliasName}"".""IsDeleted""=false");
//            //    }
//            //    var query = $@"SELECT  fc.*,true as ""IsForeignKeyTableColumn"",c.""ForeignKeyTableName"" as ""TableName""
//            //    ,c.""ForeignKeyTableSchemaName"" as ""TableSchemaName"",c.""ForeignKeyTableAliasName"" as  ""TableAliasName""
//            //    ,c.""Name"" as ""ForeignKeyColumnName"",c.""IsUdfColumn"" as ""IsUdfColumn""
//            //    FROM public.""ColumnMetadata"" c
//            //    join public.""TableMetadata"" ft on c.""ForeignKeyTableId""=ft.""Id"" and ft.""IsDeleted""=false
//            //    join public.""ColumnMetadata"" fc on ft.""Id""=fc.""TableMetadataId""  and fc.""IsDeleted""=false
//            //    where c.""TableMetadataId""='{tableMetaData.Id}'
//            //    and c.""IsForeignKey""=true and c.""IsDeleted""=false";
//            //    var result = await _queryRepo.ExecuteQueryList<ColumnMetadataViewModel>(query, null);
//            //    if (result != null && result.Count() > 0)
//            //    {
//            //        foreach (var item in result)
//            //        {
//            //            var tableName = @$"""{item.TableAliasName}""";
//            //            if (item.IsUdfColumn)
//            //            {
//            //                tableName = @$"""{item.TableAliasName}_{item.ForeignKeyColumnName}""";
//            //            }

//            //            columns.Add(@$"{Environment.NewLine}{tableName}.""{item.Name}"" as ""{item.ForeignKeyColumnName}_{item.Name}""");
//            //        }

//            //    }
//            //}

//            var selectQuery = @$"select {string.Join(",", columns)}  {Environment.NewLine}from {string.Join(Environment.NewLine, tables)}{Environment.NewLine}where {string.Join(Environment.NewLine + "and ", condition)}";
//            return selectQuery;
//        }
//        private async Task<string> GetFormSelectQuery(TableMetadataViewModel tableMetaData, string where = null, string filtercolumns = null, string filter = null, bool ignoreJoins = false, string returnColumns = null, int? limit = null, int? skip = null, bool enableLocalization = false, string lang = null)
//        {
//            var columns = new List<string>();
//            var tables = new List<string>();
//            var condition = new List<string>();
//            var pkColumns = await _repo.GetList<ColumnMetadataViewModel, ColumnMetadata>(x => x.TableMetadataId == tableMetaData.Id && x.IsVirtualColumn == false);
//            FormTemplateViewModel formTemplate = null;
//            string displayColumnId = null;
//            string displayColumn = null;
//            if (enableLocalization || lang != "en-US")
//            {
//                //TODO Change to single qury
//                var template = await _repo.GetSingle<TemplateViewModel, Template>(x => x.TableMetadataId == tableMetaData.Id);
//                if (template != null)
//                {
//                    formTemplate = await _repo.GetSingle<FormTemplateViewModel, FormTemplate>(x => x.TemplateId == template.Id);
//                }

//                displayColumnId = formTemplate?.DisplayColumnId;

//            }
//            string localizedColumn = null;
//            foreach (var item in pkColumns)
//            {
//                if (displayColumnId.IsNotNullAndNotEmpty() && displayColumnId == item.Id)
//                {
//                    displayColumn = item.Name;
//                    var appLanguage = _userContext.CultureName;
//                    var col = @$"""{tableMetaData.Name}"".""{item.Name}"" as ""{item.Name}""";
//                    if (lang.IsNotNullAndNotEmpty())
//                    {
//                        switch (lang)
//                        {
//                            case "ar-SA":
//                                localizedColumn = col = @$"coalesce(lang.""Arabic"",""{tableMetaData.Name}"".""{item.Name}"") as ""{item.Name}""";
//                                break;
//                            case "hi-IN":
//                                localizedColumn = col = @$"coalesce(lang.""Hindi"",""{tableMetaData.Name}"".""{item.Name}"") as ""{item.Name}""";
//                                break;
//                            default:
//                                break;
//                        }
//                    }
//                    else
//                    {
//                        switch (appLanguage)
//                        {
//                            case "ar-SA":
//                                localizedColumn = col = @$"coalesce(lang.""Arabic"",""{tableMetaData.Name}"".""{item.Name}"") as ""{item.Name}""";
//                                break;
//                            case "hi-IN":
//                                localizedColumn = col = @$"coalesce(lang.""Hindi"",""{tableMetaData.Name}"".""{item.Name}"") as ""{item.Name}""";
//                                break;
//                            default:
//                                break;
//                        }
//                    }

//                    columns.Add(@$"{Environment.NewLine}{col}");
//                }
//                else
//                {
//                    columns.Add(@$"{Environment.NewLine}""{tableMetaData.Name}"".""{item.Name}"" as ""{item.Name}""");
//                }

//            }
//            tables.Add(@$"{tableMetaData.Schema}.""{tableMetaData.Name}"" as ""{tableMetaData.Name}""");
//            condition.Add(@$"""{tableMetaData.Name}"".""IsDeleted""=false");
//            var formTemlateViewModel = await _cmsQueryBusiness.GetSelectQueryData(tableMetaData);
//            if (formTemlateViewModel != null && formTemlateViewModel.EnableLegalEntityFilter)
//            {
//                condition.Add(@$"""{tableMetaData.Name}"".""LegalEntityId""='{_userContext.LegalEntityId}'");
//            }
//            if (filtercolumns.IsNotNullAndNotEmpty() && filter.IsNotNullAndNotEmpty())
//            {
//                var filters = filtercolumns.Split(',');
//                var filterValues = filter.Split(',');
//                var i = 0;
//                foreach (var item in filters)
//                {
//                    var value = filterValues[i];
//                    condition.Add(@$"""{tableMetaData.Name}"".""{item}""='{value}'");
//                    i++;
//                }
//            }
//            if (displayColumnId.IsNotNullAndNotEmpty())
//            {
//                tables.Add(@$"left join public.""FormResourceLanguage"" as lang 
//                    on ""{tableMetaData.Name}"".""Id""=lang.""FormTableId"" 
//                    and lang.""IsDeleted""=false");
//            }
//            var fks = pkColumns.Where(x => x.IsForeignKey && x.ForeignKeyTableId.IsNotNullAndNotEmpty() && x.HideForeignKeyTableColumns == false).ToList();
//            if (fks != null && fks.Count() > 0)
//            {
//                foreach (var item in fks)
//                {
//                    if (item.IsUdfColumn)
//                    {
//                        item.ForeignKeyTableAliasName = item.ForeignKeyTableName + "_" + item.Name;
//                    }
//                    tables.Add(@$"left join {item.ForeignKeyTableSchemaName}.""{item.ForeignKeyTableName}"" as ""{item.ForeignKeyTableAliasName}"" 
//                    on ""{tableMetaData.Name}"".""{item.Name}""=""{item.ForeignKeyTableAliasName}"".""{item.ForeignKeyColumnName}"" 
//                    and ""{item.ForeignKeyTableAliasName}"".""IsDeleted""=false");
//                }
//                var result = await _cmsQueryBusiness.GetForeignKeyColumnByTableMetadata(tableMetaData);

//                if (result != null && result.Count() > 0)
//                {
//                    foreach (var item in result)
//                    {
//                        var tableName = @$"""{item.TableAliasName}""";
//                        if (item.IsUdfColumn)
//                        {
//                            tableName = @$"""{item.TableAliasName}_{item.ForeignKeyColumnName}""";
//                        }

//                        columns.Add(@$"{Environment.NewLine}{tableName}.""{item.Name}"" as ""{item.ForeignKeyColumnName}_{item.Name}""");
//                    }

//                }
//            }
//            //var cols = "*";
//            //if (columns.Any())
//            //{
//            //    cols = string.Join(",", columns);
//            //}

//            var selectQuery = @$"select {string.Join(",", columns)}  {Environment.NewLine}from {string.Join(Environment.NewLine, tables)}{Environment.NewLine}where {string.Join(Environment.NewLine + "and ", condition)}";
//            if (where.IsNotNullAndNotEmpty())
//            {
//                where = where.Replace(",", "','");
//                selectQuery = $"{selectQuery} {where}";
//            }
//            if (ignoreJoins)
//            {
//                var langQuery = "";
//                if (returnColumns.IsNullOrEmpty())
//                {
//                    returnColumns = @$"""{tableMetaData.Name}"".*";
//                }
//                else
//                {
//                    returnColumns = @$"""{returnColumns.Replace(",", "\",\"")}""";

//                }
//                if (localizedColumn.IsNotNullAndNotEmpty())
//                {
//                    returnColumns = @$"{returnColumns},{localizedColumn}";
//                    langQuery = @$"left join public.""FormResourceLanguage"" as lang  on ""{tableMetaData.Name}"".""Id""=lang.""FormTableId"" and lang.""IsDeleted""=false";
//                }
//                selectQuery = @$"select {returnColumns} from {tableMetaData.Schema}.""{tableMetaData.Name}"" 
//                {langQuery}
//                where ""{tableMetaData.Name}"".""IsDeleted""=false ";
//                if (where.IsNotNullAndNotEmpty())
//                {
//                    selectQuery = $"{selectQuery} {where}";
//                }
//            }
//            return selectQuery;
//        }

//        private async Task<string> GetSelectQuery(TableMetadataViewModel tableMetaData, string where = null, string filtercolumns = null, string filter = null, bool ignoreJoins = false, string returnColumns = null, int? limit = null, int? skip = null, bool enableLocalization = false)
//        {
//            var columns = new List<string>();
//            var tables = new List<string>();
//            var condition = new List<string>();
//            var pkColumns = await _repo.GetList<ColumnMetadataViewModel, ColumnMetadata>(x => x.TableMetadataId == tableMetaData.Id
//               && x.IsVirtualColumn == false);
//            foreach (var item in pkColumns)
//            {
//                columns.Add(@$"{Environment.NewLine}""{tableMetaData.Name}"".""{item.Name}"" as ""{item.Name}""");
//            }
//            tables.Add(@$"{tableMetaData.Schema}.""{tableMetaData.Name}"" as ""{tableMetaData.Name}""");
//            condition.Add(@$"""{tableMetaData.Name}"".""IsDeleted""=false");
//            if (filtercolumns.IsNotNullAndNotEmpty() && filter.IsNotNullAndNotEmpty())
//            {
//                var filters = filtercolumns.Split(',');
//                var filterValues = filter.Split(',');
//                var i = 0;
//                foreach (var item in filters)
//                {
//                    var value = filterValues[i];
//                    condition.Add(@$"""{tableMetaData.Name}"".""{item}""='{value}'");
//                    i++;
//                }
//            }
//            var fks = pkColumns.Where(x => x.IsForeignKey && x.ForeignKeyTableId.IsNotNullAndNotEmpty() && x.HideForeignKeyTableColumns == false).ToList();
//            if (fks != null && fks.Count() > 0)
//            {
//                foreach (var item in fks)
//                {
//                    if (item.IsUdfColumn)
//                    {
//                        item.ForeignKeyTableAliasName = item.ForeignKeyTableName + "_" + item.Name;
//                    }
//                    tables.Add(@$"left join {item.ForeignKeyTableSchemaName}.""{item.ForeignKeyTableName}"" as ""{item.ForeignKeyTableAliasName}"" 
//                    on ""{tableMetaData.Name}"".""{item.Name}""=""{item.ForeignKeyTableAliasName}"".""{item.ForeignKeyColumnName}"" 
//                    and ""{item.ForeignKeyTableAliasName}"".""IsDeleted""=false");
//                }
//                //var query = $@"SELECT  fc.*,true as ""IsForeignKeyTableColumn"",c.""ForeignKeyTableName"" as ""TableName""
//                //,c.""ForeignKeyTableSchemaName"" as ""TableSchemaName"",c.""ForeignKeyTableAliasName"" as  ""TableAliasName""
//                //,c.""Name"" as ""ForeignKeyColumnName"",c.""IsUdfColumn"" as ""IsUdfColumn""
//                //FROM public.""ColumnMetadata"" c
//                //join public.""TableMetadata"" ft on c.""ForeignKeyTableId""=ft.""Id"" and ft.""IsDeleted""=false
//                //join public.""ColumnMetadata"" fc on ft.""Id""=fc.""TableMetadataId""  and fc.""IsDeleted""=false
//                //where c.""TableMetadataId""='{tableMetaData.Id}'
//                //and c.""IsForeignKey""=true and c.""IsDeleted""=false";

//                //var result = await _queryRepo.ExecuteQueryList<ColumnMetadataViewModel>(query, null);
//                var result = await _cmsQueryBusiness.GetForeignKeyColumnByTableMetadata(tableMetaData);

//                if (result != null && result.Count() > 0)
//                {
//                    foreach (var item in result)
//                    {
//                        var tableName = @$"""{item.TableAliasName}""";
//                        if (item.IsUdfColumn)
//                        {
//                            tableName = @$"""{item.TableAliasName}_{item.ForeignKeyColumnName}""";
//                        }

//                        columns.Add(@$"{Environment.NewLine}{tableName}.""{item.Name}"" as ""{item.ForeignKeyColumnName}_{item.Name}""");
//                    }

//                }
//            }
//            var cols = "*";
//            if (columns.Any())
//            {
//                cols = string.Join(",", columns);
//            }

//            var selectQuery = @$"select {cols}  {Environment.NewLine}from {string.Join(Environment.NewLine, tables)}{Environment.NewLine}where {string.Join(Environment.NewLine + "and ", condition)}";
//            if (where.IsNotNullAndNotEmpty())
//            {
//                where = where.Replace(",", "','");
//                selectQuery = $"{selectQuery} {where}";
//            }
//            if (ignoreJoins)
//            {

//                if (returnColumns.IsNullOrEmpty())
//                {
//                    returnColumns = "*";
//                }
//                else
//                {
//                    returnColumns = @$"""{returnColumns.Replace(",", "\",\"")}""";
//                }
//                selectQuery = @$"select {returnColumns} from {tableMetaData.Schema}.""{tableMetaData.Name}"" where ""IsDeleted""=false ";
//                if (where.IsNotNullAndNotEmpty())
//                {
//                    selectQuery = $"{selectQuery} {where}";
//                }
//            }
//            return selectQuery;
//        }

//        private async Task<string> GetDataByColumn(ColumnMetadataViewModel column, object columnValue, TableMetadataViewModel tableMetaData, string excludeId)
//        {

//            var dt = await _cmsQueryBusiness.GetDataByColumn(column, columnValue, tableMetaData, excludeId);
//            if (dt.Rows.Count > 0)
//            {

//                var dr = dt.Rows[0];
//                var dict = dr.Table.Columns
//                .Cast<DataColumn>()
//                .ToDictionary(c => c.ColumnName, c => dr[c]);
//                var j = JsonConvert.SerializeObject(dict);
//                return j;
//            }
//            return string.Empty;
//        }

//        //public async Task<FormIndexPageTemplateViewModel> GetFormIndexPageViewModel(PageViewModel page)
//        //{
//        //    FormIndexPageTemplateViewModel model = null;
//        //    switch (page.PageType)
//        //    {

//        //        case TemplateTypeEnum.FormIndexPage:
//        //            model = await _repo.GetSingle<FormIndexPageTemplateViewModel, FormIndexPageTemplate>(x => x.TemplateId == page.TemplateId, y => y.Template);
//        //            break;
//        //        case TemplateTypeEnum.Form:
//        //            var form = await _repo.GetSingle<FormTemplateViewModel, FormTemplate>(x => x.TemplateId == page.TemplateId);
//        //            if (form != null)
//        //            {
//        //                model = await _repo.GetSingleById<FormIndexPageTemplateViewModel, FormIndexPageTemplate>(form.IndexPageTemplateId);
//        //            }
//        //            break;
//        //        //case TemplateTypeEnum.Note:
//        //        //    var note = await _repo.GetSingle<NoteTemplateViewModel, NoteTemplate>(x => x.TemplateId == page.TemplateId);
//        //        //    if (note != null)
//        //        //    {
//        //        //        model = await _repo.GetSingleById<FormIndexPageTemplateViewModel, NoteIndexPageTemplate>(note.NoteIndexPageTemplateId);
//        //        //    }
//        //        //    break;
//        //        //case TemplateTypeEnum.Task:
//        //        //    var task = await _repo.GetSingle<TaskTemplateViewModel, TaskTemplate>(x => x.TemplateId == page.TemplateId);
//        //        //    if (task != null)
//        //        //    {
//        //        //        model = await _repo.GetSingleById<FormIndexPageTemplateViewModel, TaskIndexPageTemplate>(task.TaskIndexPageTemplateId);
//        //        //    }
//        //        //    break;
//        //        //case TemplateTypeEnum.Service:
//        //        //    var service = await _repo.GetSingle<ServiceTemplateViewModel, ServiceTemplate>(x => x.TemplateId == page.TemplateId);
//        //        //    if (service != null)
//        //        //    {
//        //        //        model = await _repo.GetSingleById<FormIndexPageTemplateViewModel, FormIndexPageTemplate>(service.ServiceIndexPageTemplateId);
//        //        //    }
//        //        //    break;
//        //        case TemplateTypeEnum.Custom:
//        //            break;
//        //        default:
//        //            break;
//        //    }

//        //    var indexPageColumns = await _repo.GetList<FormIndexPageColumnViewModel, FormIndexPageColumn>
//        //        (x => x.FormIndexPageTemplateId == model.Id && x.IsDeleted == false, x => x.ColumnMetadata);
//        //    var cloumns = new List<FormIndexPageColumnViewModel>();
//        //    foreach (var item in indexPageColumns.OrderBy(x => x.SequenceOrder))
//        //    {
//        //        if (item.IsForeignKeyTableColumn)
//        //        {
//        //            // item.ColumnName = $"{item.ForeignKeyTableAliasName}_{item.ColumnMetadata.Name}";
//        //        }
//        //        else
//        //        {
//        //            item.ColumnName = item.ColumnMetadata.Name;
//        //        }

//        //        cloumns.Add(item);
//        //    }
//        //    model.SelectedTableRows = cloumns;
//        //    return model;
//        //}

//        public async Task<CommandResult<FormTemplateViewModel>> ManageForm(FormTemplateViewModel model)
//        {
//            if (model.DataAction == DataActionEnum.Create)
//            {
//                var presubmit = await ManagePresubmit(model);
//                if (!presubmit.IsSuccess)
//                {
//                    return presubmit;
//                }
//                var result = await CreateForm(model.Json, model.PageId, model.TemplateCode);
//                if (result.Item1)
//                {
//                    model.RecordId = result.Item2;
//                    var table = await _repo.GetSingle<TemplateViewModel, Template>(x => x.Code == model.TemplateCode);
//                    var tableMetaData = await _repo.GetSingleById<TableMetadataViewModel, TableMetadata>(table.TableMetadataId);
//                    var selectQuery = await GetSelectQuery(tableMetaData, @$" and ""{tableMetaData.Name}"".""Id""='{model.RecordId}' limit 1");
//                    dynamic dobject = await _queryRepo.ExecuteQuerySingleDynamicObject(selectQuery, null);
//                    try
//                    {
//                        await ManageBusinessRule(BusinessLogicExecutionTypeEnum.PostSubmit, model, dobject);

//                    }
//                    catch (Exception ex)
//                    {

//                        return CommandResult<FormTemplateViewModel>.Instance(model, false, $"Error in post submit business rule execution: {ex.Message}");
//                    }

//                    return await Task.FromResult(CommandResult<FormTemplateViewModel>.Instance(model));
//                }
//                else
//                {
//                    return await Task.FromResult(CommandResult<FormTemplateViewModel>.Instance(model, result.Item1, result.Item2));
//                }

//            }
//            else if (model.DataAction == DataActionEnum.Delete)
//            {
//                await DeleteFrom(model);
//                return await Task.FromResult(CommandResult<FormTemplateViewModel>.Instance(model));
//            }
//            else if (model.DataAction == DataActionEnum.Edit)
//            {
//                var presubmit = await ManagePresubmit(model);
//                if (!presubmit.IsSuccess)
//                {
//                    return presubmit;
//                }
//                var result = await EditForm(model.RecordId, model.Json, model.PageId, model.TemplateCode);
//                if (result.Item1)
//                {
//                    model.RecordId = result.Item2;
//                    var table = await _repo.GetSingle<TemplateViewModel, Template>(x => x.Code == model.TemplateCode);
//                    var tableMetaData = await _repo.GetSingleById<TableMetadataViewModel, TableMetadata>(table.TableMetadataId);
//                    var selectQuery = await GetSelectQuery(tableMetaData, @$" and ""{tableMetaData.Name}"".""Id""='{model.RecordId}' limit 1");
//                    dynamic dobject = await _queryRepo.ExecuteQuerySingleDynamicObject(selectQuery, null);
//                    try
//                    {
//                        await ManageBusinessRule(BusinessLogicExecutionTypeEnum.PostSubmit, model, dobject);

//                    }
//                    catch (Exception ex)
//                    {

//                        return CommandResult<FormTemplateViewModel>.Instance(model, false, $"Error in post submit business rule execution: {ex.Message}");
//                    }
//                    return await Task.FromResult(CommandResult<FormTemplateViewModel>.Instance(model));
//                }
//                else
//                {
//                    return await Task.FromResult(CommandResult<FormTemplateViewModel>.Instance(model, result.Item1, result.Item2));
//                }

//            }
//            return await Task.FromResult(CommandResult<FormTemplateViewModel>.Instance(model, false, "An error occured while processing your request"));
//        }
   

//        private async Task<string> DeleteFrom(FormTemplateViewModel model)
//        {
//            var page = await _pageBusiness.GetPageForExecution(model.PageId);
//            if (page != null && page.Template != null)
//            {
//                var id = Guid.NewGuid().ToString();
//                var tableMetaData = await _repo.GetSingle<TableMetadataViewModel, TableMetadata>(x => x.Id == page.Template.TableMetadataId);
//                if (tableMetaData != null)
//                {
//                    var tableColumns = await _repo.GetList<ColumnMetadataViewModel, ColumnMetadata>(x => x.TableMetadataId == tableMetaData.Id);
//                    if (tableColumns != null && tableColumns.Count > 0)
//                    {
//                        //var selectQuery = new StringBuilder(@$"update {ApplicationConstant.Database.Schema.Cms}.""{tableMetaData.Name}"" set ");
//                        //selectQuery.Append(Environment.NewLine);
//                        //selectQuery.Append(@$"""IsDeleted""=true,{Environment.NewLine}");
//                        //selectQuery.Append(@$"""LastUpdatedDate""='{DateTime.Now.ToDatabaseDateFormat()}',{Environment.NewLine}");
//                        //selectQuery.Append(@$"""LastUpdatedBy""='{_repo.UserContext.UserId}'{Environment.NewLine}");
//                        //selectQuery.Append(@$"where ""Id""='{model.RecordId}'");
//                        //var queryText = selectQuery.ToString();
//                        //await _queryRepo.ExecuteCommand(queryText, null);

//                        await _cmsQueryBusiness.DeleteFrom(model, tableMetaData);

//                    }

//                }
//                return await Task.FromResult(id);
//            }
//            return await Task.FromResult(string.Empty);
//        }
//        public async Task<Tuple<bool, string>> CreateForm(string data, string pageId, string templateCode = null)
//        {
//            Template template = null;
//            if (templateCode.IsNotNullAndNotEmpty())
//            {
//                template = await _repo.GetSingle(x => x.Code == templateCode);
//            }
       
//            // var page = await _pageBusiness.GetPageForExecution(pageId);
//            if (template != null)
//            {
//                var rowData = JsonConvert.DeserializeObject<Dictionary<string, object>>(data);
//                var id = "";
//                if (rowData.ContainsKey("Id"))
//                {
//                    id = Convert.ToString(rowData["Id"]);
//                }
//                else
//                {
//                    id = Guid.NewGuid().ToString();
//                }
//                if (id.IsNullOrEmpty())
//                {
//                    id = Guid.NewGuid().ToString();
//                }
//                var tableMetaData = await _repo.GetSingle<TableMetadataViewModel, TableMetadata>(x => x.Id == template.TableMetadataId);
//                if (tableMetaData != null)
//                {
//                    var tableColumns = await _repo.GetList<ColumnMetadataViewModel, ColumnMetadata>(x => x.TableMetadataId == tableMetaData.Id);
//                    if (tableColumns != null && tableColumns.Count > 0)
//                    {
//                        var validate = await ValidateForm(data, pageId, rowData, tableMetaData, tableColumns);
//                        if (validate.IsNotNullAndNotEmpty())
//                        {
//                            return await Task.FromResult(new Tuple<bool, string>(false, validate));
//                        }
//                        var selectQuery = new StringBuilder(@$"insert into {ApplicationConstant.Database.Schema.Cms}.""{tableMetaData.Name}""");
//                        var columnKeys = new List<string>();
//                        columnKeys.Add(@"""Id""");
//                        var columnValues = new List<object>();
//                        columnValues.Add(@$"'{id}'");
//                        foreach (var col in tableColumns.Where(x => x.IsSystemColumn == false && x.UdfUIType != UdfUITypeEnum.editgrid && x.UdfUIType != UdfUITypeEnum.datagrid))
//                        {
//                            if (col.Name != "Id")
//                            {
//                                columnKeys.Add(@$"""{col.Name}""");
//                                columnValues.Add(BusinessHelper.ConvertToDbValue(rowData.GetValueOrDefault(col.Name), col.IsSystemColumn, col.DataType));
//                            }
//                        }
//                        foreach (var col in tableColumns.Where(x => x.UdfUIType == UdfUITypeEnum.editgrid || x.UdfUIType == UdfUITypeEnum.datagrid))
//                        {
//                            if (col.Name != "Id")
//                            {
//                                var gridvalue = rowData.GetValueOrDefault(col.Name);
//                                if (gridvalue != null)
//                                {
//                                    columnKeys.Add(@$"""{col.Name}""");
//                                    JArray gridData = JArray.Parse(gridvalue.ToString());
//                                    foreach (JObject jcomp in gridData)
//                                    {
//                                        foreach (var item in jcomp)
//                                        {
//                                            var val = item.Value.ToString();
//                                            var key = item.Key;
//                                            var convertedValue = BusinessHelper.ConvertToDbValue(val, false, col.DataType);
//                                            if (convertedValue != "null")
//                                            {
//                                                var value = convertedValue.ToString();
//                                                value = value.Replace("'", "");
//                                                jcomp[key] = value;
//                                            }

//                                        }

//                                    }
//                                    var colValue = JsonConvert.SerializeObject(gridData);
//                                    columnValues.Add(@$"'{colValue}'");

//                                }
//                            }
//                        }

//                        columnKeys.Add(@$"""SequenceOrder""");
//                        var sq = rowData.GetValueOrDefault("SequenceOrder");
//                        if (sq == null)
//                        {
//                            sq = 1;
//                        }
//                        columnValues.Add(sq);
//                        columnKeys.Add(@"""CreatedDate""");
//                        columnKeys.Add(@"""CreatedBy""");
//                        columnKeys.Add(@"""LastUpdatedDate""");
//                        columnKeys.Add(@"""LastUpdatedBy""");
//                        columnKeys.Add(@"""IsDeleted""");
//                        columnKeys.Add(@"""CompanyId""");
//                        columnKeys.Add(@"""LegalEntityId""");
//                        //columnKeys.Add(@"""SequenceOrder""");
//                        columnKeys.Add(@"""Status""");
//                        columnKeys.Add(@"""VersionNo""");

//                        columnValues.Add(@$"'{DateTime.Now.ToDatabaseDateFormat()}'");
//                        columnValues.Add(@$"'{_repo.UserContext.UserId}'");
//                        columnValues.Add(@$"'{DateTime.Now.ToDatabaseDateFormat()}'");
//                        columnValues.Add(@$"'{_repo.UserContext.UserId}'");
//                        columnValues.Add(@$"false");
//                        if (rowData.ContainsKey("CompanyId"))
//                        {
//                            columnValues.Add(@$"'{Convert.ToString(rowData["CompanyId"])}'");
//                        }
//                        else
//                        {
//                            columnValues.Add(@$"'{_repo.UserContext.CompanyId}'");
//                        }
//                        if (rowData.ContainsKey("LegalEntityId"))
//                        {
//                            columnValues.Add(@$"'{Convert.ToString(rowData["LegalEntityId"])}'");
//                        }
//                        else
//                        {
//                            columnValues.Add(@$"'{_repo.UserContext.LegalEntityId}'");
//                        }
//                        //columnValues.Add(@$"1");
//                        columnValues.Add(@$"{(int)StatusEnum.Active}");
//                        columnValues.Add(@$"1");
//                        selectQuery.Append(Environment.NewLine);
//                        selectQuery.Append($"({string.Join(", ", columnKeys)})");
//                        selectQuery.Append(Environment.NewLine);
//                        selectQuery.Append($"values ({string.Join(", ", columnValues)})");
//                        var queryText = selectQuery.ToString();

//                        //await _queryRepo.ExecuteCommand(queryText, null);
//                        await _cmsQueryBusiness.TableQueryExecute(queryText);
//                        await ManageLocalization(rowData, id);
//                    }
//                    var childTable = tableColumns.Where(x => x.UdfUIType == UdfUITypeEnum.editgrid || x.UdfUIType == UdfUITypeEnum.datagrid).ToList();
//                    foreach (var child in childTable)
//                    {
//                        var gridData = rowData.GetValueOrDefault(child.Name);
//                        if (gridData != null)
//                        {
//                            //data = gridData.ToString();
//                            //data = data.Replace("[", "").Replace("]", "");
//                            //JArray result = JArray.FromObject(gridData);
//                            JArray result = JArray.Parse(gridData.ToString());
//                            foreach (JObject jcomp in result)
//                            {
//                                if (jcomp.Count > 0)
//                                {
//                                    jcomp.Remove("ParentId");
//                                    var newProperty = new JProperty("ParentId", id);
//                                    jcomp.Add(newProperty);
//                                    //jcomp.SelectToken("ParentId").Replace(JToken.FromObject(id));
//                                    var record = await CreateForm(jcomp.ToString(), null, child.Name);
//                                    //jcomp.SelectToken("Id").Replace(JToken.FromObject(record.Item2));
//                                }

//                            }
//                            //var query = @$"update {ApplicationConstant.Database.Schema.Cms}.""{tableMetaData.Name}"" set ""{child.Name}""='{result}'
//                            //                    where ""Id""='{id}' ";

//                            //await _cmsQueryBusiness.TableQueryExecute(query);
//                        }

//                    }
//                }
//                return await Task.FromResult(new Tuple<bool, string>(true, id));
//            }
//            return await Task.FromResult(new Tuple<bool, string>(false, "Page does not exist."));
//        }

//        //private async Task ManageLocalization(Dictionary<string, object> rowData, string id)
//        //{
//        //    var obj = Convert.ToString(rowData.GetValueOrDefault("Localization"));
//        //    var localizedData = JsonConvert.DeserializeObject<Dictionary<string, object>>(obj);
//        //    if (localizedData != null && localizedData.Any())
//        //    {
//        //        string hindi = null;
//        //        if (localizedData.ContainsKey("Hindi"))
//        //        {
//        //            hindi = Convert.ToString(localizedData["Hindi"]);
//        //        }
//        //        string arabic = null;
//        //        if (localizedData.ContainsKey("Arabic"))
//        //        {
//        //            arabic = Convert.ToString(localizedData["Arabic"]);
//        //        }
//        //        var exist = await _repo.GetSingle<FormResourceLanguageViewModel, FormResourceLanguage>(x => x.FormTableId == id);
//        //        if (exist == null)
//        //        {
//        //            await _repo.Create<FormResourceLanguageViewModel, FormResourceLanguage>(new FormResourceLanguageViewModel
//        //            {
//        //                Hindi = hindi,
//        //                Arabic = arabic,
//        //                FormTableId = id
//        //            });
//        //        }
//        //        else
//        //        {
//        //            if (hindi != null)
//        //            {
//        //                exist.Hindi = hindi;
//        //            }
//        //            if (arabic != null)
//        //            {
//        //                exist.Arabic = arabic;
//        //            }
//        //            await _repo.Edit<FormResourceLanguage, FormResourceLanguage>(exist);
//        //        }
//        //    }
//        //}

//        private async Task<string> ValidateForm(string data, string pageId, Dictionary<string, object> rowData, TableMetadataViewModel tableMetaData, List<ColumnMetadataViewModel> tableColumns, string exculdeId = null)
//        {
//            var uniqueColumns = tableColumns.Where(x => x.IsUniqueColumn).ToList();
//            foreach (var item in uniqueColumns)
//            {
//                var value = rowData.GetValueOrDefault(item.Name);
//                var exist = await GetDataByColumn(item, value, tableMetaData, exculdeId);
//                if (exist.IsNotNullAndNotEmpty())
//                {
//                    return await Task.FromResult(@$"An item already exists with '{item.Alias}' as '{value}'. Please enter another value");
//                }
//            }
//            return string.Empty;
//        }


//        public async Task<Tuple<bool, string>> EditForm(string recordId, string data, string pageId, string templateCode = null)
//        {
//            Template template = null;
//            if (templateCode.IsNotNullAndNotEmpty())
//            {
//                template = await _repo.GetSingle(x => x.Code == templateCode);
//            }
//            else
//            {
//                var page = await _pageBusiness.GetPageForExecution(pageId);
//                if (page != null)
//                {
//                    template = page.Template;
//                }
//            }
//            //var page = await _pageBusiness.GetPageForExecution(pageId);
//            //if (page != null && page.Template != null)
//            if (template != null)
//            {
//                var rowData = JsonConvert.DeserializeObject<Dictionary<string, object>>(data);
//                var tableMetaData = await _repo.GetSingle<TableMetadataViewModel, TableMetadata>(x => x.Id == template.TableMetadataId);
//                if (tableMetaData != null)
//                {
//                    var tableColumns = await _repo.GetList<ColumnMetadataViewModel, ColumnMetadata>(x => x.TableMetadataId == tableMetaData.Id);
//                    if (tableColumns != null && tableColumns.Count > 0)
//                    {
//                        var validate = await ValidateForm(data, pageId, rowData, tableMetaData, tableColumns, recordId);
//                        if (validate.IsNotNullAndNotEmpty())
//                        {
//                            return await Task.FromResult(new Tuple<bool, string>(false, validate));
//                        }
//                        var selectQuery = new StringBuilder(@$"update {ApplicationConstant.Database.Schema.Cms}.""{tableMetaData.Name}""");
//                        selectQuery.Append(Environment.NewLine);
//                        selectQuery.Append("set");
//                        selectQuery.Append(Environment.NewLine);
//                        var columnKeys = new List<string>();
//                        foreach (var col in tableColumns.Where(x => x.IsUdfColumn && x.UdfUIType != UdfUITypeEnum.editgrid && x.UdfUIType != UdfUITypeEnum.datagrid))
//                        {
//                            columnKeys.Add(@$"""{col.Name}"" = {BusinessHelper.ConvertToDbValue(rowData.GetValueOrDefault(col.Name), col.IsSystemColumn, col.DataType)}");
//                        }
//                        foreach (var col in tableColumns.Where(x => x.UdfUIType == UdfUITypeEnum.editgrid || x.UdfUIType == UdfUITypeEnum.datagrid))
//                        {
//                            if (col.Name != "Id")
//                            {

//                                var gridvalue = rowData.GetValueOrDefault(col.Name);
//                                if (gridvalue != null)
//                                {
//                                    JArray gridData = JArray.Parse(gridvalue.ToString());
//                                    foreach (JObject jcomp in gridData)
//                                    {
//                                        foreach (var item in jcomp)
//                                        {
//                                            var val = item.Value.ToString();
//                                            var key = item.Key;
//                                            var convertedValue = BusinessHelper.ConvertToDbValue(val, false, col.DataType);
//                                            if (convertedValue != "null")
//                                            {
//                                                var value = convertedValue.ToString();
//                                                value = value.Replace("'", "");
//                                                jcomp[key] = value;
//                                            }

//                                        }

//                                    }
//                                    var colValue = JsonConvert.SerializeObject(gridData);
//                                    columnKeys.Add(@$"""{col.Name}"" = '{colValue}'");
//                                }
//                            }
//                        }
//                        columnKeys.Add(@$"""LastUpdatedBy"" = '{_userContext.UserId}'");
//                        columnKeys.Add(@$"""LastUpdatedDate"" = '{DateTime.Now.ToDatabaseDateFormat()}'");
//                        selectQuery.Append($"{string.Join(",", columnKeys)}");
//                        selectQuery.Append(Environment.NewLine);
//                        selectQuery.Append(@$"where  ""Id"" = '{recordId}'");
//                        var queryText = selectQuery.ToString();

//                        //await _queryRepo.ExecuteCommand(queryText, null);
//                        await _cmsQueryBusiness.TableQueryExecute(queryText);
//                        await ManageLocalization(rowData, recordId);

//                        var childTable = tableColumns.Where(x => x.UdfUIType == UdfUITypeEnum.editgrid || x.UdfUIType == UdfUITypeEnum.datagrid).ToList();
//                        foreach (var child in childTable)
//                        {
//                            var gridData = rowData.GetValueOrDefault(child.Name);
//                            if (gridData != null)
//                            {
//                                //JArray result = JArray.FromObject(gridData);
//                                JArray result = JArray.Parse(gridData.ToString());
//                                var list = JsonConvert.DeserializeObject<IList<IdNameViewModel>>(gridData.ToString());
//                                var ids = list.Select(x => x.Id).ToList();
//                                var tableName = await _repo.GetSingle<TemplateViewModel, Template>(x => x.Code == child.Name);
//                                var delRec = await GetData("cms", tableName.Name, "ParentId", recordId);
//                                var delList = (from DataRow dr in delRec.Rows
//                                               where !ids.Contains(dr["Id"].ToString())
//                                               select new IdNameViewModel()
//                                               {
//                                                   Id = dr["Id"].ToString(),
//                                               }).ToList();
//                                foreach (var del in delList)
//                                {
//                                    await _cmsQueryBusiness.DeleteFrom(new FormTemplateViewModel { RecordId = del.Id }, new TableMetadataViewModel { Name = tableName.Name });
//                                }
//                                foreach (JObject jcomp in result)
//                                {
//                                    if (jcomp.Count > 0)
//                                    {
//                                        var dataId = jcomp.GetValue("Id");
//                                        if (dataId.IsNotNull() && dataId.ToString().IsNotNullAndNotEmptyAndNotValue("null"))
//                                        {
//                                            await EditForm(dataId.ToString(), jcomp.ToString(), null, child.Name);
//                                        }
//                                        else
//                                        {
//                                            jcomp.Remove("ParentId");
//                                            var newProperty = new JProperty("ParentId", recordId);
//                                            jcomp.Add(newProperty);
//                                            //jcomp.SelectToken("ParentId").Replace(JToken.FromObject(recordId));
//                                            await CreateForm(jcomp.ToString(), null, child.Name);

//                                        }
//                                    }

//                                }
//                                //var query = @$"update {ApplicationConstant.Database.Schema.Cms}.""{tableMetaData.Name}"" set ""{child.Name}""='{result}'
//                                //                where ""Id""='{recordId}' ";

//                                //await _cmsQueryBusiness.TableQueryExecute(query);
//                            }
//                        }

//                        return await Task.FromResult(new Tuple<bool, string>(true, recordId));

//                    }

//                }
//            }
//            return await Task.FromResult(new Tuple<bool, string>(false, "Page does not exist."));
//        }

//        //public async Task<IList<EmailTaskViewModel>> ReadEmailTaskData(string refId, ReferenceTypeEnum refType)
//        //{
//        //    var taskdetails = new TaskViewModel();
//        //    if (refType == ReferenceTypeEnum.NTS_Task)
//        //    {
//        //        taskdetails = await _taskBusiness.GetSingle(x => x.Id == refId);
//        //    }

//        //    //var queryData1 = await  _queryRepoEmailTask.ExecuteQueryList(query1, null);
//        //    var queryData1 = await _cmsQueryBusiness.ReadEmailTaskData(refId, refType, taskdetails);
//        //    return queryData1;
//        //}

      

     

     
//        public async Task<FormTemplateViewModel> GetFormDetails(FormTemplateViewModel viewModel)
//        {
//            var template = await _repo.GetSingle<TemplateViewModel, Template>(x => x.Id == viewModel.TemplateId);
//            if (template != null)
//            {
//                viewModel.TableMetadataId = template.TableMetadataId;
//            }
//            viewModel.ColumnList = await _repo.GetList<ColumnMetadataViewModel, ColumnMetadata>(x => x.TableMetadataId == viewModel.TableMetadataId

//                && x.IsVirtualColumn == false && x.IsHiddenColumn == false && x.IsLogColumn == false && x.IsPrimaryKey == false);

//            GetFormUdfDetails(viewModel);
//            //will add ntsId to session varibale here
//            return viewModel;
//        }


//        public void GetFormUdfDetails(FormTemplateViewModel model)
//        {
//            if (model.ColumnList == null || !model.ColumnList.Any())
//            {
//                return;
//            }
//            if (model.Json.IsNotNullAndNotEmpty())
//            {
//                var result = JObject.Parse(model.Json);
//                var rows = (JArray)result.SelectToken("components");
//                ChildComp(rows, model.ColumnList, model);
//                result.Remove("components");
//                result.Add("components", JArray.FromObject(rows));
//                model.Json = result.ToString();
//            }
//        }
//        private void ChildComp(JArray comps, List<ColumnMetadataViewModel> Columns, FormTemplateViewModel model)
//        {

//            foreach (JObject jcomp in comps)
//            {
//                var typeObj = jcomp.SelectToken("type");
//                var keyObj = jcomp.SelectToken("key");
//                if (typeObj.IsNotNull())
//                {
//                    var type = typeObj.ToString();
//                    var key = keyObj.ToString();
//                    if (type == "textfield" || type == "textarea" || type == "number" || type == "password"
//                        || type == "selectboxes" || type == "checkbox" || type == "select" || type == "radio"
//                        || type == "email" || type == "url" || type == "phoneNumber" || type == "tags"
//                        || type == "datetime" || type == "day" || type == "time" || type == "currency" || type == "button"
//                        || type == "signature" || type == "file" || type == "hidden" || type == "datagrid" || type == "editgrid" || (type == "htmlelement" && key == "chartgrid") || (type == "htmlelement" && key == "chartJs"))
//                    {


//                        var reserve = jcomp.SelectToken("reservedKey");
//                        if (reserve == null)
//                        {
//                            var tempmodel = JsonConvert.DeserializeObject<FormFieldViewModel>(jcomp.ToString());
//                            var columnId = jcomp.SelectToken("columnMetadataId");
//                            if (columnId != null)
//                            {
//                                var columnMeta = Columns.FirstOrDefault(x => x.Name == tempmodel.key);
//                                if (columnMeta != null)
//                                {
//                                    ///columnMeta.ActiveUserType = model.ActiveUserType;
//                                    //columnMeta.NtsStatusCode = model.NoteStatusCode;
//                                    var isReadonly = false;
//                                    if (model.ReadoOnlyUdfs != null && model.ReadoOnlyUdfs.ContainsKey(columnMeta.Name))
//                                    {
//                                        isReadonly = model.ReadoOnlyUdfs.GetValueOrDefault(columnMeta.Name);
//                                    }
//                                    var udfValue = new JProperty("udfValue", columnMeta.Value);
//                                    jcomp.Add(udfValue);

//                                    //Set default value
//                                    if (model.Udfs != null && model.Udfs.GetValue(columnMeta.Name) != null)
//                                    {
//                                        var newProperty = new JProperty("defaultValue", model.Udfs.GetValue(columnMeta.Name));
//                                        jcomp.Add(newProperty);
//                                    }
//                                    //if (!columnMeta.IsVisible)
//                                    //{
//                                    //    var hiddenproperty = jcomp.SelectToken("hidden");
//                                    //    if (hiddenproperty == null)
//                                    //    {
//                                    //        var newProperty = new JProperty("hidden", true);
//                                    //        jcomp.Add(newProperty);
//                                    //    }

//                                    //}
//                                    //if (!columnMeta.IsEditable || isReadonly)
//                                    //{
//                                    //    var newhiddenproperty = jcomp.SelectToken("disabled");
//                                    //    if (newhiddenproperty == null)
//                                    //    {
//                                    //        var newProperty = new JProperty("disabled", true);
//                                    //        jcomp.Add(newProperty);
//                                    //    }
//                                    //}

//                                }
//                            }


//                        }
//                    }
//                    else if (type == "columns")
//                    {
//                        JArray cols = (JArray)jcomp.SelectToken("columns");
//                        foreach (var col in cols)
//                        {
//                            JArray rows = (JArray)col.SelectToken("components");
//                            if (rows != null)
//                                ChildComp(rows, Columns, model);
//                        }
//                    }
//                    else if (type == "table")
//                    {
//                        JArray cols = (JArray)jcomp.SelectToken("rows");
//                        foreach (var col in cols)
//                        {
//                            JArray rows = (JArray)col.SelectToken("components");
//                            if (rows != null)
//                                ChildComp(rows, Columns, model);
//                        }
//                    }
//                    else
//                    {
//                        JArray rows = (JArray)jcomp.SelectToken("components");
//                        if (rows != null)
//                            ChildComp(rows, Columns, model);
//                    }
//                }
//            }
//        }

     

      
//        public async Task<string> GetLatestMigrationScript()
//        {
//            var queryData = await _cmsQueryBusiness.GetLatestMigrationScript();
//            return queryData;
//        }

//        public async Task<List<string>> GetAllMigrationsList()
//        {
//            var queryData = await _cmsQueryBusiness.GetAllMigrationsList();
//            return queryData;
//        }

   
//        public async Task<string> ExecuteMigrationScript(string script)
//        {
//            var error = "Query Execution Success";
//            try
//            {
//                var exScript = script;
//                //var exScript = script.Replace("\"","\"\"");
//                var query = $@" " + exScript + " ";
//                //var queryData = await _queryRepo.ExecuteQuerySingle(query, null);
//                var queryData = await _cmsQueryBusiness.ExecuteMigrationScript(script);
//                return error;
//            }
//            catch (Exception ex)
//            {
//                Console.Write(ex.ToString());
//                return ex.ToString();
//            }

//        }

     
     

      

     
       

//        //public async Task<CommandResult<FormTemplateViewModel>> CopyFormTemplate(FormTemplateViewModel oldModel, string newTempId)
//        //{
//        //    var _templateBusiness = _serviceProvider.GetService<ITemplateBusiness>();
//        //    var _formTemplateBusiness = _serviceProvider.GetService<IFormTemplateBusiness>();

//        //    var oldTempData = await _templateBusiness.GetSingleById(oldModel.TemplateId);
//        //    //var oldModel = await _formTemplateBusiness.GetSingle(x => x.TemplateId == oldTempId);
//        //    var newModel = await _formTemplateBusiness.GetSingle(x => x.TemplateId == newTempId);
//        //    FormTemplateViewModel model = new();

//        //    model = newModel;
//        //    newModel = oldModel;

//        //    newModel.Id = model.Id;
//        //    newModel.TemplateId = model.TemplateId;
//        //    newModel.IndexPageTemplateId = model.IndexPageTemplateId;
//        //    newModel.CreatedDate = model.CreatedDate;
//        //    newModel.CreatedBy = model.CreatedBy;
//        //    newModel.LastUpdatedDate = model.LastUpdatedDate;
//        //    newModel.LastUpdatedBy = model.LastUpdatedBy;
//        //    newModel.CompanyId = model.CompanyId;
//        //    newModel.LegalEntityId = model.LegalEntityId;

//        //    var json = Helper.ReplaceJsonProperty(oldTempData.Json, "columnMetadataId");

//        //    newModel.Json = json;

//        //    var formresult = await _formTemplateBusiness.Edit(newModel);
//        //    return formresult;
//        //}

//        //public async Task<CommandResult<FormIndexPageTemplateViewModel>> CopyFormTemplateIndexPageData(FormIndexPageTemplateViewModel model, string newTempId)
//        //{
//        //    var _formIndexPageTemplateBusiness = _serviceProvider.GetService<IFormIndexPageTemplateBusiness>();
//        //    var _formIndexPageColumnBusiness = _serviceProvider.GetService<IFormIndexPageColumnBusiness>();

//        //    var newFormIndexModel = await _formIndexPageTemplateBusiness.GetSingle(x => x.TemplateId == newTempId);
//        //    var rows = await _formIndexPageColumnBusiness.GetList(x => x.FormIndexPageTemplateId == model.Id);
//        //    foreach (var i in rows)
//        //    {
//        //        i.DataAction = DataActionEnum.Create;
//        //        i.Id = null;
//        //        i.Select = true;
//        //        i.FormIndexPageTemplateId = newFormIndexModel.Id;
//        //    }
//        //    model.SelectedTableRows = rows;

//        //    var existingModel = newFormIndexModel;
//        //    newFormIndexModel = model;

//        //    newFormIndexModel.Id = existingModel.Id;
//        //    newFormIndexModel.TemplateId = existingModel.TemplateId;
//        //    newFormIndexModel.CreatedDate = existingModel.CreatedDate;
//        //    newFormIndexModel.CreatedBy = existingModel.CreatedBy;
//        //    newFormIndexModel.LastUpdatedDate = existingModel.LastUpdatedDate;
//        //    newFormIndexModel.LastUpdatedBy = existingModel.LastUpdatedBy;
//        //    newFormIndexModel.CompanyId = existingModel.CompanyId;
//        //    newFormIndexModel.LegalEntityId = existingModel.LegalEntityId;
//        //    newFormIndexModel.SelectedTableRows = model.SelectedTableRows;

//        //    var result = await _formIndexPageTemplateBusiness.Edit(newFormIndexModel);
//        //    return result;
//        //}

//    //public async Task UpdateUdfPermission(List<UdfPermissionViewModel> oldUdfList, List<ColumnMetadataViewModel> columnMetadataList, string newStepTaskTempId, List<ColumnMetadataViewModel> oldColumnMetadataList)
//        //{
//        //    var _UdfPermissionBusiness = _serviceProvider.GetService<IUdfPermissionBusiness>();

//        //    foreach (var udf in oldUdfList)
//        //    {
//        //        var idx = oldColumnMetadataList.FindIndex(x => x.Id == udf.ColumnMetadataId);
//        //        if (idx == -1)
//        //        {
//        //            continue;
//        //        }
//        //        var alias = oldColumnMetadataList[idx].Alias;
//        //        var newIdx = columnMetadataList.FindIndex(x => x.Alias == alias);

//        //        udf.Id = null;
//        //        udf.ColumnMetadataId = columnMetadataList[newIdx].Id;
//        //        udf.TemplateId = newStepTaskTempId;
//        //        await _UdfPermissionBusiness.Create(udf);

//        //    }
//        //}

     
     

//        //--------------------------------------------------------------------------------------------------------

//        //public async Task<bool> CopyForm(string oldTempId, string newTemplateId, bool devImport = false, CopyTemplateViewModel copyModel = null)
//        //{
//        //    var _formTemplateBusiness = _serviceProvider.GetService<IFormTemplateBusiness>();
//        //    var _formIndexPageTemplateBusiness = _serviceProvider.GetService<IFormIndexPageTemplateBusiness>();
//        //    var _resourceLanguageBusiness = _serviceProvider.GetService<IResourceLanguageBusiness>();
//        //    // Form
//        //    if (!devImport)
//        //    {
//        //        var oldFormModel = await _cmsQueryBusiness.GetSingle<FormTemplateViewModel, FormTemplate>(x => x.TemplateId == oldTempId);
//        //        var formResult = await _formTemplateBusiness.CopyFormTemplate(oldFormModel, newTemplateId); // done
//        //        if (formResult.IsSuccess)
//        //        {
//        //            // Index
//        //            var oldFormIndexModel = await _cmsQueryBusiness.GetSingle<FormIndexPageTemplateViewModel, FormIndexPageTemplate>(x => x.TemplateId == oldTempId);
//        //            var formIndexResult = await _formIndexPageTemplateBusiness.CopyFormTemplateIndexPageData(oldFormIndexModel, newTemplateId); // done
//        //            if (formIndexResult.IsSuccess)
//        //            {
//        //                // Language
//        //                var oldLanguageList = await _cmsQueryBusiness.GetList<ResourceLanguageViewModel, ResourceLanguage>(x => x.TemplateId == oldTempId);
//        //                var languageResult = await _resourceLanguageBusiness.CopyLanguage(oldLanguageList, newTemplateId);
//        //                return true;
//        //            }
//        //            else
//        //            {
//        //                return false;
//        //            }

//        //        }
//        //        else
//        //        {
//        //            return false;
//        //        }
//        //    }
//        //    else
//        //    {
//        //        var oldFormModel = copyModel.FormTemplate;
//        //        var formResult = await _formTemplateBusiness.CopyFormTemplate(oldFormModel, newTemplateId, copyModel, devImport);
//        //        if (formResult.IsSuccess)
//        //        {
//        //            // Index
//        //            var oldFormIndexModel = copyModel.FormIndexPageTemplate;
//        //            var formIndexResult = await _formIndexPageTemplateBusiness.CopyFormTemplateIndexPageData(oldFormIndexModel, newTemplateId, devImport, copyModel);
//        //            if (formIndexResult.IsSuccess)
//        //            {
//        //                // Language
//        //                var oldLanguageList = copyModel.ResourceLanguage;
//        //                var languageResult = await _resourceLanguageBusiness.CopyLanguage(oldLanguageList, newTemplateId);
//        //                return true;
//        //            }
//        //            else
//        //            {
//        //                return false;
//        //            }

//        //        }
//        //        else
//        //        {
//        //            return false;
//        //        }
//        //    }
//        //}

  
//    }
//}
