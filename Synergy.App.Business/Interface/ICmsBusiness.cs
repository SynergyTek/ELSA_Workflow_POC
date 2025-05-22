using System.Data;
using Synergy.App.Data;
using Synergy.App.Data.Models;
using Synergy.App.Data.ViewModels;

namespace Synergy.App.Business.Interface
{
    public interface ICmsBusiness : IBusinessBase<TemplateViewModel, TemplateModel>
    {
        Task ManageTable(TableViewModel table);


    }
}