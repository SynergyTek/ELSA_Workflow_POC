using Synergy.App.Business.Interface;
using Synergy.App.Data.Models;
using Synergy.App.Data.ViewModels;

namespace Synergy.App.Business.Implementation;

public class TemplateBusiness(
    IContextBase<TemplateViewModel, TemplateModel> repo,
       ITableBusiness tableBusiness,
    IServiceProvider sp)
    : BusinessBase<TemplateViewModel, TemplateModel>(repo, sp), ITemplateBusiness
{
    public override async Task<CommandResult<TemplateViewModel>> Create(TemplateViewModel model, bool autoCommit = true)
    {
        var result = await base.Create(model, autoCommit);
        if (!result.IsSuccess)
            return CommandResult<TemplateViewModel>.Instance(model, result.IsSuccess, result.Messages);
        var tableResult = await tableBusiness.ManageTemplateTable(model, false, model.ParentId);
        return !tableResult.IsSuccess ? CommandResult<TemplateViewModel>.Instance(model, tableResult.IsSuccess, tableResult.Messages) : CommandResult<TemplateViewModel>.Instance(model, result.IsSuccess, result.Messages);
    }

    public override async Task<CommandResult<TemplateViewModel>> Edit(TemplateViewModel model, bool autoCommit = true)
    {
     
        var result = await base.Edit(model, autoCommit);
        if (!result.IsSuccess)
            return CommandResult<TemplateViewModel>.Instance(model, result.IsSuccess, result.Messages);
        var tableResult = await tableBusiness.ManageTemplateTable(model, false, model.ParentId);
        return !tableResult.IsSuccess ? CommandResult<TemplateViewModel>.Instance(model, tableResult.IsSuccess, tableResult.Messages) : CommandResult<TemplateViewModel>.Instance(model, result.IsSuccess, result.Messages);
    }
}