using System.Data;
using Synergy.App.Data;
using Synergy.App.Data.Model;
using Synergy.App.Data.ViewModel;

namespace Synergy.App.Business.Interface
{
    public interface ICmsBusiness : IBusinessBase<TemplateViewModel, TemplateModel>
    {
    }
}