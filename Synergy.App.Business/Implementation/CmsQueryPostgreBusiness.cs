using System.Data;
using System.Text;
using AutoMapper;
using Synergy.App.Business.Interface;
using Synergy.App.Common;
using Synergy.App.Data;
using Synergy.App.Data.Models;
using Synergy.App.Data.ViewModels;

namespace Synergy.App.Business.Implementation
{
    public class CmsQueryPostgreBusiness(
        IContextBase<TemplateViewModel, Template> repo,
        IMapper autoMapper,
        IUserContext userContext,
        IRepositoryQueryBase<TemplateViewModel> queryRepo,
        IServiceProvider serviceProvider)
        : BaseBusiness<TemplateViewModel, Template>(repo, serviceProvider), ICmsQueryBusiness
    {
        private readonly IUserContext _userContext = userContext;
        private readonly IRepositoryQueryBase<TemplateViewModel> _queryRepo = queryRepo;
        private readonly IServiceProvider _serviceProvider = serviceProvider;

        //private readonly IHangfireScheduler _hangfireScheduler;

        //_hangfireScheduler = hangfireScheduler;

        public async Task<bool> ManageTableExists(TableMetadataViewModel existingTableMetadata)
        {
            var query = @$"select exists (
	                        select 1
	                        from information_schema.{("tables")} 
	                        where table_schema = '{existingTableMetadata.Schema}'
	                        and table_name = '{existingTableMetadata.Name}'
                        ) ";
            var exist = await _queryRepo.ExecuteScalar<bool>(query, null);
            return exist;
        }

        public async Task<bool?> ManageTableRecordExists(TableMetadataViewModel existingTableMetadata)
        {
            var query = @$"select true from  
                        {existingTableMetadata.Schema}.""{existingTableMetadata.Name}"" limit 1 ";

            var recordExists = await _queryRepo.ExecuteScalar<bool?>(query, null);
            return recordExists;
        }

        public async Task EditTableSchema(TableMetadataViewModel tableMetadata)
        {
            var tableVarWithSchema = "<<table-schema>>";
            var query = new StringBuilder();
            var oldTableWithSchema = $"{tableMetadata.OldSchema}.\"{tableMetadata.OldName}\"";
            query.Append($"DROP View {oldTableWithSchema};");
            query.Append($"CREATE OR REPLACE VIEW {tableVarWithSchema} As {tableMetadata.Query} ;");
            query.Append($"ALTER TABLE {tableVarWithSchema} OWNER to {ApplicationConstant.Database.Owner.Postgres};");

            var tableWithSchema = $"{tableMetadata.Schema}.\"{tableMetadata.Name}\"";
            var table = $"{tableMetadata.Name}";
            var tableQuery = query.ToString().Replace("<<table-schema>>", tableWithSchema).Replace("<<table>>", table);

            await _queryRepo.ExecuteCommand(tableQuery, null);
        }

        public async Task CreateTableSchema(TableMetadataViewModel tableMetadata, bool dropTable)
        {
            var tableVarWithSchema = "<<table-schema>>";
            var query = new StringBuilder();
            if (dropTable)
            {
                query.Append($"DROP View {tableVarWithSchema};");
            }

            query.Append($"CREATE OR REPLACE VIEW {tableVarWithSchema} As {tableMetadata.Query} ;");
            query.Append($"ALTER TABLE {tableVarWithSchema} OWNER to {ApplicationConstant.Database.Owner.Postgres};");

            var tableWithSchema = $"{tableMetadata.Schema}.\"{tableMetadata.Name}\"";
            var table = $"{tableMetadata.Name}";
            var tableQuery = query.ToString().Replace("<<table-schema>>", tableWithSchema).Replace("<<table>>", table);

            await _queryRepo.ExecuteCommand(tableQuery, null);
        }

        public async Task TableQueryExecute(string tableQuery)
        {
            await _queryRepo.ExecuteCommand(tableQuery, null);
        }

        public async Task<DataTable> GetQueryDataTable(string selectQuery)
        {
            var dt = await _queryRepo.ExecuteQueryDataTable(selectQuery, null);
            return dt;
        }

        public async Task<DataTable> GetEditNoteTableExistColumn(TableMetadataViewModel tableMetadata)
        {
            var query = @$"select column_name,data_type,is_nullable	   
            from information_schema.columns where table_schema = '{tableMetadata.Schema}'
	        and table_name = '{tableMetadata.Name}'";

            var dt = await _queryRepo.ExecuteQueryDataTable(query, null);
            return dt;
        }

        public async Task<DataTable> GetEditNoteTableConstraints(TableMetadataViewModel tableMetadata)
        {
            var query = @$"SELECT con.conname as conname
            FROM pg_catalog.pg_constraint con
            INNER JOIN pg_catalog.pg_class rel ON rel.oid = con.conrelid
            INNER JOIN pg_catalog.pg_namespace nsp ON nsp.oid = connamespace
            WHERE nsp.nspname = '{tableMetadata.Schema}' AND rel.relname = '{tableMetadata.Name}' ";

            var dt = await _queryRepo.ExecuteQueryDataTable(query, null);
            return dt;
        }

        public async Task<DataTable> GetEditTaskTableExistColumn(TableMetadataViewModel tableMetadata)
        {
            var query = @$"select column_name,data_type,is_nullable	   
            from information_schema.columns where table_schema = '{tableMetadata.Schema}'
	        and table_name = '{tableMetadata.Name}'";

            var dt = await _queryRepo.ExecuteQueryDataTable(query, null);
            return dt;
        }

        public async Task<DataTable> GetEditTaskTableConstraints(TableMetadataViewModel tableMetadata)
        {
            var query = @$"SELECT con.conname as conname
            FROM pg_catalog.pg_constraint con
            INNER JOIN pg_catalog.pg_class rel ON rel.oid = con.conrelid
            INNER JOIN pg_catalog.pg_namespace nsp ON nsp.oid = connamespace
            WHERE nsp.nspname = '{tableMetadata.Schema}' AND rel.relname = '{tableMetadata.Name}';";

            var dt = await _queryRepo.ExecuteQueryDataTable(query, null);
            return dt;
        }

        public async Task<DataTable> GetEditServiceTableExistColumn(TableMetadataViewModel tableMetadata)
        {
            var query = @$"select column_name,data_type,is_nullable	   
            from information_schema.columns where table_schema = '{tableMetadata.Schema}'
	        and table_name = '{tableMetadata.Name}'";

            var dt = await _queryRepo.ExecuteQueryDataTable(query, null);
            return dt;
        }

        public async Task<DataTable> GetEditServiceTableConstraints(TableMetadataViewModel tableMetadata)
        {
            var query = @$"SELECT con.conname as conname
            FROM pg_catalog.pg_constraint con
            INNER JOIN pg_catalog.pg_class rel ON rel.oid = con.conrelid
            INNER JOIN pg_catalog.pg_namespace nsp ON nsp.oid = connamespace
            WHERE nsp.nspname = '{tableMetadata.Schema}' AND rel.relname = '{tableMetadata.Name}';";

            var dt = await _queryRepo.ExecuteQueryDataTable(query, null);
            return dt;
        }

        public async Task<DataTable> GetEditFormTableExistColumn(TableMetadataViewModel tableMetadata)
        {
            var query = @$" select column_name,data_type,is_nullable	   
            from information_schema.columns where table_schema = '{tableMetadata.Schema}'
	        and table_name = '{tableMetadata.Name}' ";
            var dt = await _queryRepo.ExecuteQueryDataTable(query, null);
            return dt;
        }

        public async Task<DataTable> GetEditFormTableConstraints(TableMetadataViewModel tableMetadata)
        {
            var query = @$"SELECT con.conname as conname
            FROM pg_catalog.pg_constraint con
            INNER JOIN pg_catalog.pg_class rel ON rel.oid = con.conrelid
            INNER JOIN pg_catalog.pg_namespace nsp ON nsp.oid = connamespace
            WHERE nsp.nspname = '{tableMetadata.Schema}' AND rel.relname = '{tableMetadata.Name}';";

            var dt = await _queryRepo.ExecuteQueryDataTable(query, null);
            return dt;
        }

        public async Task<List<ColumnMetadataViewModel>> GetForeignKeyColumnByTableMetadata(
            TableMetadataViewModel tableMetaData)
        {
            var query = $@"SELECT  fc.*,true as ""IsForeignKeyTableColumn"",c.""ForeignKeyTableName"" as ""TableName""
                ,c.""ForeignKeyTableSchemaName"" as ""TableSchemaName"",c.""ForeignKeyTableAliasName"" as  ""TableAliasName""
                ,c.""Name"" as ""ForeignKeyColumnName"",c.""IsUdfColumn"" as ""IsUdfColumn""
                FROM public.""ColumnMetadata"" c
                join public.""TableMetadata"" ft on c.""ForeignKeyTableId""=ft.""Id"" and ft.""IsDeleted""=false
                join public.""ColumnMetadata"" fc on ft.""Id""=fc.""TableMetadataId""  and fc.""IsDeleted""=false
                where c.""TableMetadataId""='{tableMetaData.Id}'
                and c.""IsForeignKey""=true and c.""IsDeleted""=false";

            var result = await _queryRepo.ExecuteQueryList<ColumnMetadataViewModel>(query, null);
            return result;
        }

        public async Task<DataTable> GetDataByColumn(ColumnMetadataViewModel column, object columnValue,
            TableMetadataViewModel tableMetaData, Guid excludeId)
        {
            var selectQuery =
                @$"select * from {ApplicationConstant.Database.Schema.Cms}.""{tableMetaData.Name}"" where  ""IsDeleted""=false and ""{column.Name}""='{columnValue}'";
            if (excludeId != Guid.Empty)
            {
                selectQuery = @$"{selectQuery} and ""Id""<>'{excludeId}'";
            }

            selectQuery = @$"{selectQuery} limit 1";
            var dt = await _queryRepo.ExecuteQueryDataTable(selectQuery, null);
            return dt;
        }

        public async Task DeleteFrom(TemplateViewModel model, TableMetadataViewModel tableMetaData)
        {
            var selectQuery =
                new StringBuilder(@$"update {ApplicationConstant.Database.Schema.Cms}.""{tableMetaData.Name}"" set ");
            selectQuery.Append(Environment.NewLine);
            selectQuery.Append(@$"""IsDeleted""=true,{Environment.NewLine}");
            selectQuery.Append(@$"""LastUpdatedDate""='{DateTime.Now.ToDatabaseDateFormat()}',{Environment.NewLine}");
            selectQuery.Append(@$"""LastUpdatedBy""='{_userContext.UserId}'{Environment.NewLine}");
            selectQuery.Append(@$"where ""Id""='{model.RecordId}'");
            var queryText = selectQuery.ToString();

            await _queryRepo.ExecuteCommand(queryText, null);
        }

        public async Task<string> GetLatestMigrationScript()
        {
            var query = $@"select ""MigrationId"" from public.""__EFMigrationsHistory"" 
                        order by left(""MigrationId"", strpos(""MigrationId"", '_') - 1) desc limit 1 ";
            var queryData = await _queryRepo.ExecuteScalar<string>(query, null);
            return queryData;
        }

        public async Task<List<string>> GetAllMigrationsList()
        {
            var query = $@"select ""MigrationId"" from public.""__EFMigrationsHistory"" 
                        order by left(""MigrationId"", strpos(""MigrationId"", '_') - 1) ";
            var queryData = await _queryRepo.ExecuteScalarList<string>(query, null);
            return queryData;
        }


        public async Task<TableMetadataViewModel> GetViewableColumnMetadataListData(string schemaName, string tableName)
        {
            var query = @$"select ta.""Id"",t.""TemplateType"" from public.""TableMetadata"" ta 
            left join public.""Template"" t on ta.""Id""=coalesce(t.""UdfTableMetadataId"",t.""TableMetadataId"") and t.""IsDeleted""=false
            where ta.""Schema""='{schemaName}' and ta.""Name""='{tableName}' and ta.""IsDeleted""=false";
            var tableMetadata = await _queryRepo.ExecuteQuerySingle<TableMetadataViewModel>(query, null);
            return tableMetadata;
        }

        public async Task<List<ColumnMetadataViewModel>> GetViewableForeignKeyColumnListForFormData(
            Guid tableMetadataId)
        {
            var query = $@"SELECT  fc.*,true as IsForeignKeyTableColumn,c.""ForeignKeyTableName"" as TableName
                ,c.""ForeignKeyTableSchemaName"" as TableSchemaName,c.""ForeignKeyTableAliasName"" as  TableAliasName
                ,c.""Name"" as ""ForeignKeyBaseColumnName""
                FROM public.""ColumnMetadata"" c
                join public.""TableMetadata"" ft on c.""ForeignKeyTableId""=ft.""Id"" and ft.""IsDeleted""=false
                join public.""ColumnMetadata"" fc on ft.""Id""=fc.""TableMetadataId"" and fc.""IsDeleted""=false
                where c.""TableMetadataId""='{tableMetadataId}' and fc.""IsHiddenColumn"" = false  and c.""HideForeignKeyTableColumns""=false  
                and c.""IsForeignKey""=true and c.""IsDeleted""=false  ";
            var result = await _queryRepo.ExecuteQueryList<ColumnMetadataViewModel>(query, null);
            return result;
        }

        public async Task<List<ColumnMetadataViewModel>> GetViewableForeignKeyColumnListForNoteData(
            Guid tableMetadataId)
        {
            var query = $@"SELECT  fc.*,true as IsForeignKeyTableColumn,c.""ForeignKeyTableName"" as TableName
                ,c.""ForeignKeyTableSchemaName"" as TableSchemaName,c.""ForeignKeyTableAliasName"" as  TableAliasName
                FROM public.""ColumnMetadata"" c
                join public.""TableMetadata"" ft on c.""ForeignKeyTableId""=ft.""Id"" and ft.""IsDeleted""=false
                join public.""ColumnMetadata"" fc on ft.""Id""=fc.""TableMetadataId"" and fc.""IsDeleted""=false
                where c.""TableMetadataId""='{tableMetadataId}' and fc.""IsHiddenColumn"" = false and c.""HideForeignKeyTableColumns""=false 
                and c.""IsForeignKey""=true and c.""IsDeleted""=false  ";
            var result = await _queryRepo.ExecuteQueryList<ColumnMetadataViewModel>(query, null);
            return result;
        }

        public async Task<List<ColumnMetadataViewModel>> GetViewableForeignKeyColumnListForTaskData(
            Guid tableMetadataId)
        {
            var query = $@"SELECT  fc.*,true as IsForeignKeyTableColumn,c.""ForeignKeyTableName"" as TableName
                ,c.""ForeignKeyTableSchemaName"" as TableSchemaName,c.""ForeignKeyTableAliasName"" as  TableAliasName
                FROM public.""ColumnMetadata"" c
                join public.""TableMetadata"" ft on c.""ForeignKeyTableId""=ft.""Id"" and ft.""IsDeleted""=false
                join public.""ColumnMetadata"" fc on ft.""Id""=fc.""TableMetadataId"" and fc.""IsDeleted""=false
                where c.""TableMetadataId""='{tableMetadataId}' and fc.""IsHiddenColumn"" = false and c.""HideForeignKeyTableColumns""=false  
                and c.""IsForeignKey""=true and c.""IsDeleted""=false  ";
            var result = await _queryRepo.ExecuteQueryList<ColumnMetadataViewModel>(query, null);
            return result;
        }

        public async Task<List<ColumnMetadataViewModel>> GetViewableForeignKeyColumnListForTaskData2()
        {
            var query1 = $@"SELECT c.* ,c.""Name"" as ""Name"",c.""LabelName"" as ""LabelName""
            FROM public.""ColumnMetadata"" c
            join public.""TableMetadata"" t on c.""TableMetadataId""=t.""Id"" and t.""IsDeleted""=false
            where t.""Name""='NtsTask' and  c.""IsHiddenColumn"" = false and c.""IsDeleted""=false 
            union
            SELECT  c.*,'Template'||c.""Name"" as ""Name"",c.""LabelName"" as ""LabelName""
            FROM public.""ColumnMetadata"" c
            join public.""TableMetadata"" t on c.""TableMetadataId""=t.""Id"" and t.""IsDeleted""=false
            where t.""Name""='Template' and  c.""IsHiddenColumn"" = false and c.""IsDeleted""=false 
            and c.""Name"" in ('Name','Code')
            union
            SELECT  c.*,'CreatedByUser'||c.""Name"" as ""Name"",'CreatedByUser '||c.""LabelName"" as ""LabelName""
            FROM public.""ColumnMetadata"" c
            join public.""TableMetadata"" t on c.""TableMetadataId""=t.""Id"" and t.""IsDeleted""=false 
            where t.""Name""='User' and  c.""IsHiddenColumn"" = false and c.""IsDeleted""=false 
            and c.""Name"" in ({Helper.GetViewableUserColumns})
            union
            SELECT  c.*,'UpdatedByUser'||c.""Name"" as ""Name"",'UpdatedByUser '||c.""LabelName"" as ""LabelName""
            FROM public.""ColumnMetadata"" c
            join public.""TableMetadata"" t on c.""TableMetadataId""=t.""Id"" and t.""IsDeleted""=false
            where t.""Name""='User' and  c.""IsHiddenColumn"" = false and c.""IsDeleted""=false  
            and c.""Name"" in ({Helper.GetViewableUserColumns})
            ";
            var result1 = await _queryRepo.ExecuteQueryList<ColumnMetadataViewModel>(query1, null);
            return result1;
        }

        public async Task<List<ColumnMetadataViewModel>> GetViewableForeignKeyColumnListForServiceData(
            Guid tableMetadataId)
        {
            var query = $@"SELECT  fc.*,true as IsForeignKeyTableColumn,c.""ForeignKeyTableName"" as TableName
                ,c.""ForeignKeyTableSchemaName"" as TableSchemaName,c.""ForeignKeyTableAliasName"" as  TableAliasName
                FROM public.""ColumnMetadata"" c
                join public.""TableMetadata"" ft on c.""ForeignKeyTableId""=ft.""Id"" and ft.""IsDeleted""=false
                join public.""ColumnMetadata"" fc on ft.""Id""=fc.""TableMetadataId"" and fc.""IsDeleted""=false
                where c.""TableMetadataId""='{tableMetadataId}' and fc.""IsHiddenColumn"" = false and c.""HideForeignKeyTableColumns""=false  
                and c.""IsForeignKey""=true and c.""IsDeleted""=false  ";
            var result = await _queryRepo.ExecuteQueryList<ColumnMetadataViewModel>(query, null);
            return result;
        }

        public async Task<List<ColumnMetadataViewModel>> GetViewableForeignKeyColumnListForServiceData1(
            Guid tableMetadataId)
        {
            var query1 = $@"SELECT c.* ,c.""Name"" as ""Name"",c.""LabelName"" as ""LabelName""
            FROM public.""ColumnMetadata"" c
            join public.""TableMetadata"" t on c.""TableMetadataId""=t.""Id"" and t.""IsDeleted""=false 
            where t.""Name""='NtsService' and  c.""IsHiddenColumn"" = false and c.""IsDeleted""=false 
            union
            SELECT  c.*,'Template'||c.""Name"" as ""Name"",c.""LabelName"" as ""LabelName""
            FROM public.""ColumnMetadata"" c
            join public.""TableMetadata"" t on c.""TableMetadataId""=t.""Id"" and t.""IsDeleted""=false 
            where t.""Name""='Template' and  c.""IsHiddenColumn"" = false and c.""IsDeleted""=false 
            and c.""Name"" in ('Name','Code')
            union
            SELECT  c.*,'CreatedByUser'||c.""Name"" as ""Name"",'CreatedByUser '||c.""LabelName"" as ""LabelName""
            FROM public.""ColumnMetadata"" c
            join public.""TableMetadata"" t on c.""TableMetadataId""=t.""Id"" and t.""IsDeleted""=false 
            where t.""Name""='User' and  c.""IsHiddenColumn"" = false and c.""IsDeleted""=false 
            and c.""Name"" in ({Helper.GetViewableUserColumns})
            union
            SELECT  c.*,'UpdatedByUser'||c.""Name"" as ""Name"",'UpdatedByUser '||c.""LabelName"" as ""LabelName""
            FROM public.""ColumnMetadata"" c
            join public.""TableMetadata"" t on c.""TableMetadataId""=t.""Id"" and t.""IsDeleted""=false 
            where t.""Name""='User' and  c.""IsHiddenColumn"" = false and c.""IsDeleted""=false 
            and c.""Name"" in ({Helper.GetViewableUserColumns})
            ";
            var result1 = await _queryRepo.ExecuteQueryList<ColumnMetadataViewModel>(query1, null);
            return result1;
        }


        public async Task<DataTable> GetTableData(Guid tableMetadataId, string recordId, string name, string schema)
        {
            var query = $@"select * from {schema}.""{name}"" where ""Id""='{recordId}' limit 1";
            var dt = await _queryRepo.ExecuteQueryDataTable(query, null);
            return dt;
        }

        public async Task<DataTable> GetTableDataByColumnData(string schema, string name, string udfName,
            string udfValue)
        {
            var selectQuery =
                @$"select * from {schema}.""{name}"" where  ""IsDeleted""=false and ""{udfName}""='{udfValue}' limit 1";
            var dt = await _queryRepo.ExecuteQueryDataTable(selectQuery, null);
            return dt;
        }

        public async Task<DataTable> GetTableDataByHeaderIdData(string schema, string name, string fieldName,
            string headerId)
        {
            var selectQuery =
                @$"select * from {schema}.""{name}"" where  ""IsDeleted""=false and ""{fieldName}""='{headerId}' limit 1";
            var dt = await _queryRepo.ExecuteQueryDataTable(selectQuery, null);
            return dt;
        }

        public async Task<DataTable> DeleteTableDataByHeaderIdData(string schema, string name, string fieldName,
            string headerId)
        {
            var selectQuery =
                @$"update {schema}.""{name}"" set  ""IsDeleted""=true where ""{fieldName}""='{headerId}' ";
            var dt = await _queryRepo.ExecuteQueryDataTable(selectQuery, null);
            return dt;
        }

        public async Task EditTableDataByHeaderIdData(string schema, string name, List<string> columnKeys,
            string fieldName, string headerId)
        {
            var selectQuery =
                @$"update {schema}.""{name}"" set {string.Join(",", columnKeys)} where ""{fieldName}""='{headerId}' ";
            await _queryRepo.ExecuteCommand(selectQuery, null);
        }

        public async Task<DataTable> GetViewColumnByTableNameData(string schema, string tableName)
        {
            var query =
                $@"select * from INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '{tableName}' and TABLE_SCHEMA='{schema}'";
            var columns = await _queryRepo.ExecuteQueryDataTable(query, null);
            return columns;
        }


        public async Task<string> GetForeignKeyId(ColumnMetadataViewModel col, string val)
        {
            string query =
                $@"Select ""Id"" from ""{col.ForeignKeyTableSchemaName}"".""{col.ForeignKeyTableName}"" where ""{col.ForeignKeyDisplayColumnName}""='{val}' ";
            var res = await _queryRepo.ExecuteScalar<string>(query, null);
            return res;
        }
    }
}