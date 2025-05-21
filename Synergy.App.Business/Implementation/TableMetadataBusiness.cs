using System.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Synergy.App.Business.Interface;
using Synergy.App.Common;
using Synergy.App.Data;
using Synergy.App.Data.Models;
using Synergy.App.Data.ViewModels;

namespace Synergy.App.Business.Implementation
{
    public class TableMetadataBusiness(
        IContextBase<TableMetadataViewModel, TableMetadataModel> repo,
        IColumnMetadataBusiness columnMetadataBusiness,
        ICmsBusiness cmsBusiness,
        ICmsQueryBusiness cmsQueryBusiness,
        IUserContext userContext,
        IServiceProvider serviceProvider)
        : BusinessBase<TableMetadataViewModel, TableMetadataModel>(repo, serviceProvider),
            ITableMetadataBusiness
    {
        private readonly IServiceProvider _serviceProvider = serviceProvider;
        private readonly IContextBase<TableMetadataViewModel, TableMetadataModel> _repo = repo;

        public override async Task<CommandResult<TableMetadataViewModel>> Create(TableMetadataViewModel model,
            bool autoCommit = true)
        {
            var errorList = new Dictionary<string, string>();


            var tableName = await base.GetSingle(x => x.Name == model.Name);
            if (!tableName.IsNotNull())
            {
                errorList.Add("Name", "Name already exist.");
            }

            var tableAlias = await base.GetSingle(x => x.Alias == model.Alias);
            if (!tableAlias.IsNotNull())
            {
                errorList.Add("Alias", "Alias/Short name already exist.");
            }

            if (model.ColumnMetadatas.IsNotNull())
            {
                foreach (var col in model.ColumnMetadatas.Where(col => col.ForeignKeyConstraintName.IsNotNullAndNotEmpty()))
                {
                    var columnconstraint = await columnMetadataBusiness.GetSingle(c =>
                        c.ForeignKeyConstraintName == col.ForeignKeyConstraintName);
                    if (columnconstraint != null)
                    {
                        errorList.Add("ForeignKeyConstraintName",
                            "Column - " + col.ForeignKeyConstraintName +
                            ", Foreign key constraint name already exist.");
                    }
                }
            }

            if (errorList.Count > 0)
            {
                return CommandResult<TableMetadataViewModel>.Instance(model, false, errorList);
            }
            else
            {
                var result = await base.Create(model, autoCommit);

                if (result.IsSuccess)
                {
                    model.Id = result.Item.Id;
                    foreach (var col in model.ColumnMetadatas)
                    {
                        col.TableMetadataId = result.Item.Id;
                        if (col.DataAction == DataActionEnum.Create)
                        {
                            // col.IsNullable = true;
                            var colresult = await columnMetadataBusiness.Create(col);
                            if (colresult.IsSuccess)
                            {
                                col.Id = colresult.Item.Id;
                            }
                        }
                        else if (col.DataAction == DataActionEnum.Edit)
                        {
                            var colresult = await columnMetadataBusiness.Edit(col);
                            if (colresult.IsSuccess)
                            {
                                //col.Id = colresult.Item.Id;
                            }
                        }
                    }

                    await cmsBusiness.ManageTable(model);
                }

                return CommandResult<TableMetadataViewModel>.Instance(model, result.IsSuccess, result.Messages);
            }
        }

        public List<ColumnMetadataViewModel> AddBaseColumns(TableMetadataViewModel table,
            IContextBase<TableMetadataViewModel, TableMetadataModel> repo, DataActionEnum dataAction)
        {
            var list = new List<ColumnMetadataViewModel>();
            if (table.ColumnMetadatas.IsNotNull())
            {
                list = table.ColumnMetadatas.OrderBy(x => x.SequenceOrder).ToList();
            }

            list.Insert(0, AddColumn(new ColumnMetadataViewModel
            {
                Id = Guid.NewGuid(),
                Name = "Id",
                LabelName = "Id",
                Alias = "Id",
                DataType = DataColumnTypeEnum.Text,
                IsPrimaryKey = true,
                IsSystemColumn = true,
                IsHiddenColumn = true,
                IsNullable = false,
            }, table, dataAction));
            list.Add(AddColumn(new ColumnMetadataViewModel
            {
                Id = Guid.NewGuid(),
                Name = "CreatedDate",
                LabelName = "Created Date",
                Alias = "CreatedDate",
                DataType = DataColumnTypeEnum.DateTime,
                IsSystemColumn = true,
                IsLogColumn = true,
                IsNullable = false,
            }, table, dataAction));
            list.Add(AddColumn(new ColumnMetadataViewModel
            {
                Id = Guid.NewGuid(),
                Name = "CreatedBy",
                LabelName = "Created By User Id",
                Alias = "CreatedBy",
                IsNullable = false,
                DataType = DataColumnTypeEnum.Text,
                IsSystemColumn = true,
                IsLogColumn = true,

                IsForeignKey = true,
                IsVirtualForeignKey = true,
                ForeignKeyColumnName = "Id",
                ForeignKeyTableName = "User",
                ForeignKeyTableSchemaName = ApplicationConstant.Database.Schema._Public,
                ForeignKeyDisplayColumnName = "Name",
                ForeignKeyDisplayColumnAlias = "CreatedByUserName",
                ForeignKeyDisplayColumnLabelName = "Created By User",
                ForeignKeyConstraintName = $"FK_{table.Name}_User_CreatedBy_Id",
                ForeignKeyDisplayColumnDataType = DataColumnTypeEnum.Text,
                // HideForeignKeyTableColumns = true,
                ForeignKeyTableAliasName = "CreatedByUser"
            }, table, dataAction));
            list.Add(AddColumn(new ColumnMetadataViewModel
            {
                Id = Guid.NewGuid(),
                Name = "LastUpdatedDate",
                LabelName = "Last Updated Date",
                Alias = "LastUpdatedDate",
                DataType = DataColumnTypeEnum.DateTime,
                IsSystemColumn = true,
                IsLogColumn = true,
                IsNullable = false,
            }, table, dataAction));
            list.Add(AddColumn(new ColumnMetadataViewModel
            {
                Id = Guid.NewGuid(),
                Name = "LastUpdatedBy",
                LabelName = "Updated By User Id",
                Alias = "LastUpdatedBy",
                DataType = DataColumnTypeEnum.Text,
                IsSystemColumn = true,
                IsLogColumn = true,
                IsNullable = false,

                IsForeignKey = true,
                IsVirtualForeignKey = true,
                ForeignKeyColumnName = "Id",
                ForeignKeyTableName = "User",
                ForeignKeyTableSchemaName = ApplicationConstant.Database.Schema._Public,
                ForeignKeyDisplayColumnName = "Name",
                ForeignKeyDisplayColumnAlias = "LastUpdatedByUserName",
                ForeignKeyDisplayColumnLabelName = "Updated By User",
                ForeignKeyConstraintName = $"FK_{table.Name}_User_LastUpdatedBy_Id",
                ForeignKeyDisplayColumnDataType = DataColumnTypeEnum.Text,
                //  HideForeignKeyTableColumns = true,
                ForeignKeyTableAliasName = "UpdatedByUser"
            }, table, dataAction));
            list.Add(AddColumn(new ColumnMetadataViewModel
            {
                Id = Guid.NewGuid(),
                Name = "IsDeleted",
                LabelName = "Is Deleted",
                Alias = "IsDeleted",
                DataType = DataColumnTypeEnum.Bool,
                IsSystemColumn = true,
                IsHiddenColumn = true,
                IsNullable = false,
            }, table, dataAction));
            list.Add(AddColumn(new ColumnMetadataViewModel
            {
                Id = Guid.NewGuid(),
                Name = "CompanyId",
                LabelName = "Company Id",
                Alias = "CompanyId",
                DataType = DataColumnTypeEnum.Text,
                IsSystemColumn = true,
                IsHiddenColumn = true,
                IsNullable = false,
            }, table, dataAction));
            list.Add(AddColumn(new ColumnMetadataViewModel
            {
                Id = Guid.NewGuid(),
                Name = "LegalEntityId",
                LabelName = "LegalEntity Id",
                Alias = "LegalEntityId",
                DataType = DataColumnTypeEnum.Text,
                IsSystemColumn = true,
                IsHiddenColumn = true,
                IsNullable = true,
            }, table, dataAction));
            list.Add(AddColumn(new ColumnMetadataViewModel
            {
                Id = Guid.NewGuid(),
                Name = "SequenceOrder",
                LabelName = "Sequence Order",
                Alias = "SequenceOrder",
                DataType = DataColumnTypeEnum.Long,
                IsSystemColumn = true,
                IsHiddenColumn = true,
                IsNullable = true,
            }, table, dataAction));
            list.Add(AddColumn(new ColumnMetadataViewModel
            {
                Id = Guid.NewGuid(),
                Name = "Status",
                LabelName = "Status",
                Alias = "Status",
                DataType = DataColumnTypeEnum.Integer,
                IsSystemColumn = true,
                IsHiddenColumn = true,
            }, table, dataAction));
            list.Add(AddColumn(new ColumnMetadataViewModel
            {
                Id = Guid.NewGuid(),
                Name = "VersionNo",
                LabelName = "VersionNo",
                Alias = "VersionNo",
                DataType = DataColumnTypeEnum.Long,
                IsSystemColumn = true,
                IsHiddenColumn = true,
            }, table, dataAction));

            return list;
        }


        private ColumnMetadataViewModel AddColumn(ColumnMetadataViewModel col, TableMetadataViewModel table,
            DataActionEnum dataAction)
        {
            col.Id = Guid.NewGuid();
            col.CreatedBy = userContext.UserId;
            col.CreatedDate = DateTime.Now;
            col.LastUpdatedBy = userContext.UserId;
            col.LastUpdatedDate = DateTime.Now;
            col.IsDeleted = false;
            col.TableMetadataId = table.Id;
            col.DataAction = dataAction;
            return col;
        }


        public override async Task<CommandResult<TableMetadataViewModel>> Edit(TableMetadataViewModel data,
            bool autoCommit = true)
        {
            var errorList = new Dictionary<string, string>();

            var tablename = await base.GetSingle(x => x.Name == data.Name && x.Id != data.Id);
            if (tablename != null)
            {
                errorList.Add("Name", "Name already exist.");
            }

            var tablealias = await base.GetSingle(x => x.Alias == data.Alias && x.Id != data.Id);
            if (tablealias != null)
            {
                errorList.Add("Alias", "Alias/Short name already exist.");
            }

            if (data.ColumnMetadatas.IsNotNull())
            {
                foreach (var col in data.ColumnMetadatas)
                {
                    if (col.ForeignKeyConstraintName.IsNotNullAndNotEmpty())
                    {
                        //var columnconstraint = await repo.GetSingle<ColumnMetadataViewModel, ColumnMetadata>(c => c.ForeignKeyConstraintName == col.ForeignKeyConstraintName && c.TableMetadataId!=model.Id);
                        var columnconstraint = await columnMetadataBusiness.GetSingle(c =>
                            c.ForeignKeyConstraintName == col.ForeignKeyConstraintName && c.TableMetadataId != data.Id);
                        if (columnconstraint != null)
                        {
                            errorList.Add("ForeignKeyConstraintName",
                                "Column - " + col.ForeignKeyConstraintName +
                                ", Foreign key constraint name already exist.");
                        }
                    }
                }
            }

            if (errorList.Count > 0)
            {
                return CommandResult<TableMetadataViewModel>.Instance(data, false, errorList);
            }
            else
            {
                var result = await base.Edit(data, autoCommit);
                if (result.IsSuccess)
                {
                    //if (data.TableType != TableTypeEnum.View)
                    //{
                    //    data.ColumnMetadatas = AddBaseColumns(data, repo, DataActionEnum.Edit).ToList();
                    //}
                    var existList = await columnMetadataBusiness.GetList(x => x.TableMetadataId == data.Id);
                    var curlist = data.ColumnMetadatas.Select(x => x.Name).ToList();
                    var notexist = existList.Where(x => !curlist.Contains(x.Name)).ToList();
                    foreach (var delitem in notexist)
                    {
                        await columnMetadataBusiness.Delete(delitem.Id);
                    }

                    foreach (var col2 in data.ColumnMetadatas)
                    {
                        col2.IgnorePermission = data.IgnorePermission;
                        if (col2.IsSystemColumn)
                        {
                            var changed = existList.FirstOrDefault(x =>
                                x.Name == col2.Name && x.DataType == col2.DataType && x.IsNullable == col2.IsNullable
                                && x.LabelName == col2.LabelName && x.Alias == col2.Alias &&
                                x.IsUniqueColumn == col2.IsUniqueColumn
                                && x.IsForeignKey == col2.IsForeignKey);
                            if (changed == null)
                            {
                                col2.TableMetadataId = result.Item.Id;
                                var isInDb = existList.Any(x => x.Name == col2.Name);
                                if (isInDb == false)
                                {
                                    var colresult = await columnMetadataBusiness.Create(col2);
                                    if (colresult.IsSuccess)
                                    {
                                        col2.Id = colresult.Item.Id;
                                    }
                                }
                                else if (col2.DataAction == DataActionEnum.Edit)
                                {
                                    var colresult = await columnMetadataBusiness.Edit(col2);
                                    if (colresult.IsSuccess)
                                    {
                                    }
                                }
                            }
                        }
                        else
                        {
                            var exist = existList.FirstOrDefault(x => x.Name == col2.Name);
                            if (exist == null)
                            {
                                col2.TableMetadataId = result.Item.Id;
                                var colresult = await columnMetadataBusiness.Create(col2);
                                if (colresult.IsSuccess)
                                {
                                    col2.Id = colresult.Item.Id;
                                    //  await ManageUdfPermission(data, col2);
                                }
                            }
                            else
                            {
                                var isChanged = exist.Name != col2.Name || exist.DataType != col2.DataType ||
                                                exist.IsNullable != col2.IsNullable
                                                || exist.LabelName != col2.LabelName || exist.Alias != col2.Alias ||
                                                exist.IsUniqueColumn != col2.IsUniqueColumn
                                                || exist.IsForeignKey != col2.IsForeignKey
                                                || exist.UdfUIType != col2.UdfUIType;
                                //|| !Helper.CompareStringArray(exist.ViewableBy, col2.ViewableBy)
                                //|| !Helper.CompareStringArray(exist.ViewableContext, col2.ViewableContext)
                                //|| !Helper.CompareStringArray(exist.EditableBy, col2.EditableBy)
                                //|| !Helper.CompareStringArray(exist.EditableContext, col2.EditableContext);
                                if (isChanged)
                                {
                                    col2.TableMetadataId = result.Item.Id;
                                    var isInDb = existList.Any(x => x.Name == col2.Name);
                                    if (isInDb == false)
                                    {
                                        var colresult = await columnMetadataBusiness.Create(col2);
                                        if (colresult.IsSuccess)
                                        {
                                            col2.Id = colresult.Item.Id;
                                            //  await ManageUdfPermission(data, col2);
                                        }
                                    }
                                    else if (col2.DataAction == DataActionEnum.Edit)
                                    {
                                        if (exist.Id == col2.Id)
                                        {
                                            var colresult = await columnMetadataBusiness.Edit(col2);
                                            col2.Id = colresult.Item.Id;
                                            //  await ManageUdfPermission(data, col2);
                                        }
                                        else
                                        {
                                            errorList.Add("ColumnName",
                                                "Column - " + col2.Name + ", columnid mismatch");
                                            return CommandResult<TableMetadataViewModel>.Instance(data, false,
                                                errorList);
                                        }
                                    }
                                }
                            }
                        }
                    }

                    await cmsBusiness.ManageTable(data);
                }

                return CommandResult<TableMetadataViewModel>.Instance(data);
            }
        }

        private async Task<CommandResult<TableMetadataViewModel>> CreateTemplateTable(TemplateViewModel model)
        {
            var table = new TableMetadataViewModel
            {
                IsChildTable = model.IsChildTable
            };
            var temp = await _repo.GetSingleById<TemplateViewModel, TemplateModel>(model.Id);
            if (temp != null)
            {
                table.ColumnMetadatas = new List<ColumnMetadataViewModel>();
                table.ChildTable = new List<TableMetadataViewModel>();

                if (model.Json.IsNotNullAndNotEmpty())
                {
                    var result = JObject.Parse(model.Json);
                    temp.Json = result.ToString();
                    JArray rows = (JArray)result.SelectToken("components");
                    await ChildComp(rows, table, 1);
                }

                table.Name = temp.Name;
                table.Alias = temp.Code;
                table.Schema = ApplicationConstant.Database.Schema.Cms;
                table.TemplateId = model.Id;
                var res = await Create(table);
                if (res.IsSuccess)
                {
                    temp.TableMetadataId = res.Item.Id;
                    await _repo.Edit<TemplateViewModel, TemplateModel>(temp);
                }

                return res;
            }

            return CommandResult<TableMetadataViewModel>.Instance(table, false,
                new Dictionary<string, string>() { { "Name", "Name already exist." } });
        }

        private async Task<CommandResult<TableMetadataViewModel>> EditTemplateTable(TemplateViewModel model,
            bool ignorePermission, Guid parentTemplateId)
        {
            try
            {
                if (model.Json.IsNullOrEmpty())
                {
                    model.Json = "{}";
                }

                var result = JObject.Parse(model.Json);
                JArray rows = (JArray)result.SelectToken("components");
                var temp = await _repo.GetSingleById<TemplateViewModel, TemplateModel>(model.Id);
                var table = await GetSingleById(temp.TableMetadataId);
                table.IsChildTable = model.IsChildTable;
                if (table != null)
                {
                    table.ColumnMetadatas = new List<ColumnMetadataViewModel>();
                    table.ChildTable = new List<TableMetadataViewModel>();
                    await ChildComp(rows, table, 1);
                    table.IgnorePermission = ignorePermission;
                    table.TemplateId = parentTemplateId;

                    var res = await Edit(table);
                    if (res.IsSuccess)
                    {
                        temp.Json = model.Json = result.ToString();

                        if (ignorePermission)
                        {
                            var obj = JObject.Parse(result.ToString());
                            var rows1 = (JArray)obj.SelectToken("components");
                            //  await NoteUdfPermission(rows1);
                            temp.Json = obj.ToString();
                        }

                        await _repo.Edit<TemplateViewModel, TemplateModel>(temp);
                        var formbusi = _serviceProvider.GetService<ITemplateBusiness>();
                        foreach (var child in table.ChildTable)
                        {
                            var exist = await formbusi.GetSingle(x => x.Code == child.Name);
                            if (exist == null)
                            {
                                var ChildformTemp = new TemplateViewModel()
                                {
                                    //  PortalId = model.PortalId,
                                    Name = child.Name,
                                    Code = child.Code,
                                    Json = child.Json,
                                    IsChildTable = true
                                };
                                await formbusi.Create(ChildformTemp);
                            }
                            else
                            {
                                exist.IsChildTable = true;
                                exist.Json = child.Json;
                                await ManageTemplateTable(exist, false, model.Id);
                            }
                        }
                    }

                    return res;
                }

                return null;
            }
            catch (Exception ex)
            {
                var error = ex.ToString();
                throw;
            }
        }

        public async Task<CommandResult<TableMetadataViewModel>> ManageTemplateTable(TemplateViewModel model,
            bool ignorePermission, Guid parentTemplateId)
        {
            var table = await GetSingleById(model.TableMetadataId);
            if (table == null)
            {
                table = await GetSingle(x => x.Name == model.Name);
                if (table == null)
                {
                    return await CreateTemplateTable(model);
                }
                else
                {
                    return await EditTemplateTable(model, ignorePermission, parentTemplateId);
                }
            }
            else
            {
                return await EditTemplateTable(model, ignorePermission, parentTemplateId);
            }

            // return res;
        }

        public async Task ChildComp(JArray comps, TableMetadataViewModel table, int seqNo)
        {
            if (comps == null)
            {
                return;
            }

            foreach (JObject jcomp in comps)
            {
                var typeObj = jcomp.SelectToken("type");
                var keyObj = jcomp.SelectToken("key");
                if (typeObj.IsNotNull() && keyObj.ToString() != "Id")
                {
                    var type = typeObj.ToString();
                    var key = keyObj.ToString();
                    if (type == "textfield" || type == "textarea" || type == "number" || type == "password"
                        || type == "selectboxes" || type == "checkbox" || type == "select" || type == "radio"
                        || type == "email" || type == "url" || type == "phoneNumber" || type == "tags"
                        || type == "datetime" || type == "day" || type == "time" || type == "currency" ||
                        type == "button"
                        || type == "signature" || type == "file" || type == "hidden" || type == "datagrid" ||
                        type == "editgrid"
                        || (type == "htmlelement" && key == "chartgrid") || (type == "htmlelement" && key == "chartJs"))
                    {
                        var reserve = jcomp.SelectToken("reservedKey");
                        if (reserve == null)
                        {
                            var tempmodel = JsonConvert.DeserializeObject<FormFieldViewModel>(jcomp.ToString());
                            var uiTye = tempmodel.type.ToEnum<UdfUITypeEnum>();
                            DataColumnTypeEnum ftype;
                            switch (uiTye)
                            {
                                case UdfUITypeEnum.textfield:
                                case UdfUITypeEnum.datagrid:
                                case UdfUITypeEnum.editgrid:
                                case UdfUITypeEnum.textarea:
                                case UdfUITypeEnum.password:
                                case UdfUITypeEnum.selectboxes:
                                case UdfUITypeEnum.content:
                                    ftype = DataColumnTypeEnum.Text;
                                    break;
                                case UdfUITypeEnum.number:
                                    ftype = DataColumnTypeEnum.Integer;
                                    break;

                                case UdfUITypeEnum.checkbox:

                                case UdfUITypeEnum.radio:
                                    ftype = DataColumnTypeEnum.Bool;
                                    break;

                                case UdfUITypeEnum.select:
                                    ftype = DataColumnTypeEnum.Text;
                                    if (tempmodel.multiple)
                                    {
                                        ftype = DataColumnTypeEnum.TextArray;
                                    }

                                    break;
                                case UdfUITypeEnum.datetime:
                                    ftype = DataColumnTypeEnum.DateTime;
                                    break;
                                case UdfUITypeEnum.time:
                                    ftype = DataColumnTypeEnum.Time;
                                    break;
                                case UdfUITypeEnum.file:

                                case UdfUITypeEnum.hidden:
                                case UdfUITypeEnum.signature:
                                case UdfUITypeEnum.day:
                                case UdfUITypeEnum.currency:
                                case UdfUITypeEnum.tags:
                                case UdfUITypeEnum.phoneNumber:
                                case UdfUITypeEnum.url:
                                case UdfUITypeEnum.email:
                                case UdfUITypeEnum.htmlelement:
                                case UdfUITypeEnum.button:
                                default:
                                    ftype = DataColumnTypeEnum.Text;
                                    break;
                            }


                            var columnId = jcomp.SelectToken("columnMetadataId");
                            var fieldmodel = new ColumnMetadataViewModel()
                            {
                                Name = tempmodel.key,
                                LabelName = tempmodel.label,
                                Alias = tempmodel.key,
                                DataType = ftype,
                                IsNullable = true,
                                IsUniqueColumn = tempmodel.unique,
                                IsUdfColumn = true,
                                EditableBy = tempmodel.EditableBy,
                                ViewableBy = tempmodel.ViewableBy,
                                EditableContext = tempmodel.EditableContext,
                                ViewableContext = tempmodel.ViewableContext,
                                IsForeignKey = false,
                                UdfUIType = uiTye,
                                SequenceOrder = seqNo,
                                IsMultiValueColumn = tempmodel.multiple
                            };

                            seqNo++;

                            if (tempmodel.multiple == false && tempmodel.allTable.IsNotNullAndNotEmpty() &&
                                tempmodel.allTable != "public.enum")
                            {
                                var split = tempmodel.allTable.Split('.');
                                var tableschema = split[0];
                                var tablename = split[1];
                                fieldmodel.IsForeignKey = true;
                                fieldmodel.ForeignKeyColumnName = "Id";
                                fieldmodel.ForeignKeyTableName = tablename;
                                fieldmodel.ForeignKeyTableAliasName = tablename;
                                fieldmodel.IsNullable = true;
                                fieldmodel.ForeignKeyTableSchemaName = split[0];
                                fieldmodel.ForeignKeyDisplayColumnName = tempmodel.mapValue;
                                fieldmodel.ForeignKeyDisplayColumnAlias = tablename + "_" + tempmodel.mapValue;
                                fieldmodel.ForeignKeyDisplayColumnLabelName = tablename + " " + tempmodel.mapValue;
                                fieldmodel.ForeignKeyConstraintName = "FK_" + table.Name + "_" + tablename + "_" +
                                                                      tempmodel.key + "_Id";
                                fieldmodel.ForeignKeyDisplayColumnDataType = DataColumnTypeEnum.Text;
                            }

                            if (tempmodel.multiple == false && tempmodel.loadTable.IsNotNullAndNotEmpty() &&
                                tempmodel.loadTable == "LOV")
                            {
                                fieldmodel.IsForeignKey = true;
                                fieldmodel.ForeignKeyColumnName = "Id";
                                fieldmodel.ForeignKeyTableName = "LOV";
                                fieldmodel.ForeignKeyTableAliasName = "LOV";
                                fieldmodel.IsNullable = true;
                                //fieldmodel.HideForeignKeyTableColumns = false;
                                fieldmodel.ForeignKeyTableSchemaName = "public";
                                fieldmodel.ForeignKeyDisplayColumnName = "Name";
                                fieldmodel.ForeignKeyDisplayColumnAlias = "LOV_" + "Name";
                                fieldmodel.ForeignKeyDisplayColumnLabelName = "LOV " + "Name";
                                // fieldmodel.ForeignKeyConstraintName = $"FK_{table.Name}_{tablename}_{tempmodel.key}_{tempmodel.mapId}";
                                fieldmodel.ForeignKeyConstraintName = string.Format("FK_{0}_LOV_{1}_Id", table.Name, tempmodel.key);
                                fieldmodel.ForeignKeyDisplayColumnDataType = DataColumnTypeEnum.Text;
                            }

                            if (columnId == null)
                            {
                                var column = await columnMetadataBusiness.GetSingle(x =>
                                    x.TableMetadataId == table.Id && x.Name == tempmodel.key);
                                if (column == null)
                                {
                                    fieldmodel.Id = Guid.NewGuid();
                                    fieldmodel.DataAction = DataActionEnum.Create;
                                }
                                else
                                {
                                    fieldmodel.Id = column.Id;
                                    fieldmodel.DataAction = DataActionEnum.Edit;
                                }

                                var newProperty = new JProperty("columnMetadataId", fieldmodel.Id);
                                jcomp.Add(newProperty);
                            }
                            else
                            {
                                var column = await columnMetadataBusiness.GetSingle(x =>
                                    x.Id == tempmodel.columnMetadataId && x.Name == tempmodel.key);
                                if (column == null)
                                {
                                    fieldmodel.Id = Guid.NewGuid();
                                    fieldmodel.DataAction = DataActionEnum.Create;
                                    jcomp.SelectToken("columnMetadataId").Replace(JToken.FromObject(fieldmodel.Id));
                                }
                                else
                                {
                                    fieldmodel.Id = tempmodel.columnMetadataId;
                                    fieldmodel.DataAction = DataActionEnum.Edit;
                                }
                            }

                            table.ColumnMetadatas.Add(fieldmodel);
                        }
                    }
                    else if (type == "columns")
                    {
                        JArray cols = (JArray)jcomp.SelectToken("columns");
                        foreach (var col in cols)
                        {
                            JArray rows = (JArray)col.SelectToken("components");
                            if (rows != null)
                                await ChildComp(rows, table, seqNo);
                        }
                    }
                    else if (type == "table")
                    {
                        JArray cols1 = (JArray)jcomp.SelectToken("rows");

                        foreach (var col in cols1)
                        {
                            foreach (var child in col.Children())
                            {
                                JArray rows = (JArray)child.SelectToken("components");
                                if (rows != null)
                                    await ChildComp(rows, table, seqNo);
                            }
                        }
                    }
                    else
                    {
                        JArray rows = (JArray)jcomp.SelectToken("components");
                        if (rows != null)
                            await ChildComp(rows, table, seqNo);
                    }

                    //Child Grid
                    if ((type == "datagrid" || type == "editgrid") && table.IsChildTable)
                    {
                        JArray rows = (JArray)jcomp.SelectToken("components");
                        if (rows != null)
                            await ChildComp(rows, table, seqNo);
                    }
                    else if (type == "datagrid" || type == "editgrid")
                    {
                        bool parent = false;
                        JArray rows = (JArray)jcomp.SelectToken("components");
                        foreach (JObject jc in rows)
                        {
                            var parentId = jc.SelectToken("key");
                            if (parentId.IsNotNull() && parentId.ToString() == "ParentId")
                            {
                                parent = true;
                            }
                        }

                        if (!parent)
                        {
                            JObject jb = new JObject();
                            var newProperty = new JProperty("key", "ParentId");
                            jb.Add(newProperty);
                            var newProperty1 = new JProperty("type", "textfield");
                            jb.Add(newProperty1);
                            var newProperty11 = new JProperty("hidden", true);
                            jb.Add(newProperty11);
                            var newProperty12 = new JProperty("clearOnHide", false);
                            jb.Add(newProperty12);
                            rows.Add(jb);
                            //ID
                            JObject jb1 = new JObject();
                            var newProperty2 = new JProperty("key", "Id");
                            jb1.Add(newProperty2);
                            var newProperty3 = new JProperty("type", "textfield");
                            jb1.Add(newProperty3);
                            var newProperty4 = new JProperty("hidden", true);
                            jb1.Add(newProperty4);
                            var newProperty5 = new JProperty("clearOnHide", false);
                            jb1.Add(newProperty5);
                            rows.Add(jb1);
                        }

                        var childmodel = new TableMetadataViewModel()
                        {
                            Name = key,
                            IsChildTable = true,
                            Json = jcomp.ToString()
                        };
                        table.ChildTable.Add(childmodel);
                        //JArray rows = (JArray)jcomp.SelectToken("components");
                        //if (rows != null)
                        //    await ChildComp(rows, table, seqNo);
                    }
                }
            }
        }

        public async Task UpdateStaticTables(string tableName = null)
        {
            var assembly = AppDomain.CurrentDomain.GetAssemblies()
                .FirstOrDefault(x => x.FullName.Contains("Synergy.App.DataModel"));
            if (assembly == null)
            {
                return;
            }

            var allClasses = assembly.GetTypes().Where(a =>
                !a.Name.EndsWith("Log") && a.IsClass && a.Namespace != null &&
                a.Namespace.Contains(@"Synergy.App.DataModel")).ToList();
            if (tableName.IsNotNullAndNotEmpty())
            {
                allClasses = allClasses.Where(x => x.Name == tableName).ToList();
            }

            var tables = await _repo.GetList();
            // var columns = await repo.GetList<ColumnMetadataViewModel, ColumnMetadata>();

            foreach (var table in allClasses)
            {
                var schema = "public";
                var customAttributes =
                    table.GetCustomAttributes(typeof(System.ComponentModel.DataAnnotations.Schema.TableAttribute),
                        false).FirstOrDefault();
                if (customAttributes != null)
                {
                    var t = customAttributes as System.ComponentModel.DataAnnotations.Schema.TableAttribute;
                    if (t.Schema.IsNotNullAndNotEmpty())
                    {
                        if (t.Schema != schema)
                        {
                            continue;
                        }

                        schema = t.Schema;
                    }
                }

                var tableExist = tables.FirstOrDefault(x => x.Name == table.Name);
                if (tableExist != null)
                {
                    tableExist.Schema = schema;
                    tableExist.Name = table.Name;
                    tableExist.Alias = table.Name;
                    tableExist.DataAction = DataActionEnum.Edit;
                    tableExist.LastUpdatedBy = userContext.UserId;
                    tableExist.LastUpdatedDate = DateTime.Now;
                    tableExist.CreateTable = false;
                    var props = table.GetProperties().Where(p => p.DeclaringType != typeof(BaseModel)).ToList();

                    props = props.Where(x =>
                        x.Name != "Id" &&
                        x.Name != "CreatedDate" &&
                        x.Name != "CreatedBy" &&
                        x.Name != "LastUpdatedDate" &&
                        x.Name != "LastUpdatedBy" &&
                        x.Name != "IsDeleted" &&
                        x.Name != "CompanyId" &&
                        x.Name != "SequenceOrder" &&
                        x.Name != "DataAction" &&
                        x.Name != "Status" &&
                        x.Name != "VersionNo"
                        && (x.PropertyType.BaseType == null || x.PropertyType.BaseType.Name != "DataModelBase")
                    ).ToList();
                    tableExist.ColumnMetadatas = new List<ColumnMetadataViewModel>();
                    foreach (var item in props)
                    {
                        tableExist.ColumnMetadatas.Add(new ColumnMetadataViewModel
                        {
                            Name = item.Name,
                            Alias = item.Name,
                            IsNullable = true,
                            DataType = item.PropertyType.ToDatabaseColumn(),
                            TableMetadataId = tableExist.Id,
                            DataAction = DataActionEnum.Create,
                            LabelName = item.Name.ToSentenceCase(),
                            Status = StatusEnum.Active,
                        });
                    }

                    await this.Edit(tableExist);
                }
                else
                {
                    var id = Guid.NewGuid();
                    tableExist = new TableMetadataViewModel { Id = id };
                    tableExist.Schema = schema;
                    tableExist.Name = table.Name;
                    tableExist.Alias = table.Name;
                    tableExist.DataAction = DataActionEnum.Create;
                    tableExist.LastUpdatedBy = userContext.UserId;
                    tableExist.LastUpdatedDate = DateTime.Now;
                    tableExist.CreatedBy = userContext.UserId;
                    tableExist.CreatedDate = DateTime.Now;
                    tableExist.CreateTable = false;
                    tableExist.Status = StatusEnum.Active;
                    var props = table.GetProperties();
                    tableExist.ColumnMetadatas = new List<ColumnMetadataViewModel>();
                    foreach (var item in props.Where(x =>
                                 x.Name != "Id" &&
                                 x.Name != "CreatedDate" &&
                                 x.Name != "CreatedBy" &&
                                 x.Name != "LastUpdatedDate" &&
                                 x.Name != "LastUpdatedBy" &&
                                 x.Name != "IsDeleted" &&
                                 x.Name != "CompanyId" &&
                                 x.Name != "SequenceOrder" &&
                                 x.Name != "Status" &&
                                 x.Name != "VersionNo"
                                 && (x.PropertyType.BaseType == null || x.PropertyType.BaseType.Name != "DataModelBase")
                             ))
                    {
                        tableExist.ColumnMetadatas.Add(new ColumnMetadataViewModel
                        {
                            Name = item.Name,
                            Alias = item.Name,
                            IsNullable = true,
                            DataType = item.PropertyType.ToDatabaseColumn(),
                            TableMetadataId = id,
                            DataAction = DataActionEnum.Create,
                            LabelName = item.Name.ToSentenceCase(),
                            Status = StatusEnum.Active,
                        });
                    }

                    await this.Create(tableExist);
                }
            }
        }

        public async Task<List<ColumnMetadataViewModel>> GetTableData(string tableMetadataId, string recordId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ColumnMetadataViewModel>> GetTableData(Guid tableMetadataId, string recordId)
        {
            var list = await _repo.GetList<ColumnMetadataViewModel, ColumnMetadataModel>(x =>
                x.TableMetadataId == tableMetadataId);
            var table =
                await _repo.GetSingleById<TableMetadataViewModel, TableMetadataModel>(tableMetadataId);
            if (table != null)
            {
                var name = table.Name;
                var schema = table.Schema;
                var dt = await cmsQueryBusiness.GetTableData(tableMetadataId, recordId, name, schema);
                if (dt != null && dt.Rows.Count > 0)
                {
                    var row = dt.Rows[0];
                    foreach (var item in list)
                    {
                        item.Value = row[item.Name];
                    }
                }
            }

            return list;
        }

        public async Task<DataRow> GetTableDataByColumn(string templateCode, Guid templateId, string udfName,
            string udfValue)
        {
            var template =
                await _repo.GetSingle<TemplateViewModel, TemplateModel>(x =>
                    x.Code == templateCode || x.Id == templateId);
            if (template != null)
            {
                var tableId =  template.TableMetadataId;
                var tableMetadata =
                    await _repo.GetSingleById<TableMetadataViewModel, TableMetadataModel>(tableId);
                if (tableMetadata != null)
                {
                    var name = tableMetadata.Name;
                    var schema = tableMetadata.Schema;
                    var dt = await cmsQueryBusiness.GetTableDataByColumnData(schema, name, udfName, udfValue);
                    if (dt.Rows.Count > 0)
                    {
                        return dt.Rows[0];
                    }
                }
            }

            return null;
        }

        public async Task<DataRow> GetTableDataByHeaderId(Guid templateId, string headerId)
        {
            var template = await _repo.GetSingleById<TemplateViewModel, TemplateModel>(templateId);
            if (template != null)
            {
                var fieldName = "";

                var tableId =  template.TableMetadataId;
                var tableMetadata =
                    await _repo.GetSingleById<TableMetadataViewModel, TableMetadataModel>(tableId);
                if (tableMetadata != null)
                {
                    var schema = tableMetadata.Schema;
                    var name = tableMetadata.Name;

                    var dt = await cmsQueryBusiness.GetTableDataByHeaderIdData(schema, name, fieldName, headerId);
                    if (dt.Rows.Count > 0)
                    {
                        return dt.Rows[0];
                    }
                }
            }

            return null;
        }

        public async Task<DataRow> DeleteTableDataByHeaderId(string templateCode, Guid templateId, string headerId)
        {
            var template =
                await _repo.GetSingle<TemplateViewModel, TemplateModel>(x =>
                    x.Code == templateCode || x.Id == templateId);
            if (template != null)
            {
                var fieldName = "";

                var tableId =  template.TableMetadataId;
                var tableMetadata =
                    await _repo.GetSingleById<TableMetadataViewModel, TableMetadataModel>(tableId);
                if (tableMetadata != null)
                {
                    var schema = tableMetadata.Schema;
                    var name = tableMetadata.Name;

                    var dt = await cmsQueryBusiness.DeleteTableDataByHeaderIdData(schema, name, fieldName, headerId);
                    if (dt.Rows.Count > 0)
                    {
                        return dt.Rows[0];
                    }
                }
            }

            return null;
        }

        public async Task EditTableDataByHeaderId(string templateCode, Guid templateId, string headerId,
            Dictionary<string, object> columnsToUpdate)
        {
            var template =
                await _repo.GetSingle<TemplateViewModel, TemplateModel>(x =>
                    x.Code == templateCode || x.Id == templateId);
            if (template != null)
            {
                var fieldName = "";

                var tableId =  template.TableMetadataId;
                var tableMetadata =
                    await _repo.GetSingleById<TableMetadataViewModel, TableMetadataModel>(tableId);
                if (tableMetadata != null)
                {
                    var columnKeys = columnsToUpdate.Select(col => @$"""{col.Key}"" = {BusinessHelper.ConvertToDbValue(col.Value, false, DataColumnTypeEnum.Text)}").ToList();

                    columnKeys.Add(@$"""LastUpdatedBy"" = '{userContext.UserId}'");
                    columnKeys.Add(@$"""LastUpdatedDate"" = '{DateTime.Now.ToDatabaseDateFormat()}'");
                    var schema = tableMetadata.Schema;
                    var name = tableMetadata.Name;
                    var selectQuery =
                        $"""update {schema}."{name}" set {string.Join(",", columnKeys)} where "{fieldName}"='{headerId}' """;
                    await cmsQueryBusiness.EditTableDataByHeaderIdData(schema, name, columnKeys, fieldName, headerId);
                }
            }
            // return ;
        }

        public async Task<List<ColumnMetadataViewModel>> GetViewColumnByTableName(string schema, string tableName)
        {
            var columns = await cmsQueryBusiness.GetViewColumnByTableNameData(schema, tableName);
            var tableColumnList = new List<ColumnMetadataViewModel>();
            foreach (DataRow row in columns.Rows)
            {
                var data = new ColumnMetadataViewModel();
                data.Name = row["column_name"].ToString();
                data.Alias = row["column_name"].ToString();
                data.LabelName = row["column_name"].ToString();
                if (row["data_type"].ToString() == "boolean")
                {
                    data.DataType = DataColumnTypeEnum.Bool;
                }
                else if (row["data_type"].ToString() == "timestamp without time zone")
                {
                    data.DataType = DataColumnTypeEnum.DateTime;
                }
                else if (row["data_type"].ToString() == "bigint")
                {
                    data.DataType = DataColumnTypeEnum.Long;
                }
                else if (row["data_type"].ToString() == "integer")
                {
                    data.DataType = DataColumnTypeEnum.Integer;
                }
                else if (row["data_type"].ToString() == "double precision")
                {
                    data.DataType = DataColumnTypeEnum.Double;
                }
                else if (row["data_type"].ToString() == "text[]")
                {
                    data.DataType = DataColumnTypeEnum.TextArray;
                }
                else
                {
                    data.DataType =
                        (DataColumnTypeEnum)Enum.Parse(typeof(DataColumnTypeEnum), row["data_type"].ToString(), true);
                }

                data.IsNullable = Convert.ToString(row["is_nullable"]) == "YES" ? true : false;
                tableColumnList.Add(data);
            }

            return tableColumnList;
        }

        public async Task<ColumnMetadataViewModel> GetColumnByTableName(string schema, string tableName,
            string columnName)
        {
            tableName = tableName.Replace("\"", ""); // Regex.Replace(tableName, @"[^0-9a-zA-Z]+", "");
            columnName = columnName.Replace("\"", ""); // Regex.Replace(columnName, @"[^0-9a-zA-Z]+", "");
            var model = await GetSingle(x => x.Schema == schema.Trim() && x.Name == tableName.Trim());
            if (model == null) return new ColumnMetadataViewModel();
            {
                var column =
                    await columnMetadataBusiness.GetSingle(x => x.TableMetadataId == model.Id && x.Name == columnName);
                return column;
            }

        }
    }
}