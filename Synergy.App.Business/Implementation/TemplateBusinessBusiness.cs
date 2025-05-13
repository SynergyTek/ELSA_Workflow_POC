using Synergy.App.Business.Interface;
using Synergy.App.Data.Models;
using Synergy.App.Data.ViewModels;

namespace Synergy.App.Business.Implementation;

public class TemplateBusinessBusiness(
    IContextBase<TemplateViewModel, Data.Models.Template> repo,
       ITableMetadataBusiness _tableMetadataBusiness,
    IServiceProvider sp)
    : BusinessBase<TemplateViewModel, Data.Models.Template>(repo, sp), ITemplateBusiness
{
    public async override Task<CommandResult<TemplateViewModel>> Create(TemplateViewModel model, bool autoCommit = true)
    {
        var result = await base.Create(model, autoCommit);
        if (result.IsSuccess)
        {

                var tableResult = await _tableMetadataBusiness.ManageTemplateTable(model, false, model.ParentId);
                if (!tableResult.IsSuccess)
                {
                    return CommandResult<TemplateViewModel>.Instance(model, tableResult.IsSuccess, tableResult.Messages);
                }
            

        }
        return CommandResult<TemplateViewModel>.Instance(model, result.IsSuccess, result.Messages);
    }

    public async override Task<CommandResult<TemplateViewModel>> Edit(TemplateViewModel model, bool autoCommit = true)
    {
     
        var result = await base.Edit(model, autoCommit);
        if (result.IsSuccess)
        {
            
               // template.Json = model.Json;
             
                var tableResult = await _tableMetadataBusiness.ManageTemplateTable(model, false, model.ParentId);
                if (!tableResult.IsSuccess)
                {
                    return CommandResult<TemplateViewModel>.Instance(model, tableResult.IsSuccess, tableResult.Messages);
                }
            
        }
        return CommandResult<TemplateViewModel>.Instance(model, result.IsSuccess, result.Messages);
    }
}