using Synergy.App.Data.Models;
using Synergy.App.Data.ViewModels;

namespace Synergy.App.Business.Interface;

public interface IColumnBusiness : IBusinessBase<ColumnViewModel, ColumnModel>
{
    Task<List<ColumnViewModel>> GetList(string templateCode);
}