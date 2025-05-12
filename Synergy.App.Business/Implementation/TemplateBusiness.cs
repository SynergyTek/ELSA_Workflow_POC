using Synergy.App.Business.Interface;
using Synergy.App.Data.Models;
using Synergy.App.Data.ViewModels;

namespace Synergy.App.Business.Implementation;

public class TemplateBusiness(
    IContextBase<TemplateViewModel, Template> repo,
    IServiceProvider sp)
    : BaseBusiness<TemplateViewModel, Template>(repo, sp), ITemplateBusiness
{
}