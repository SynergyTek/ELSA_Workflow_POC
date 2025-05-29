using Synergy.App.Data.Model;
using Synergy.App.Data.ViewModel;

namespace Synergy.App.Business.Interface;

public interface IColumnBusiness : IBusinessBase<ColumnViewModel, ColumnModel>
{
    Task<List<ColumnViewModel>> GetList(string templateCode);
}