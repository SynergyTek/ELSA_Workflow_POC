using Newtonsoft.Json;
using Synergy.App.Business.Interface;
using Synergy.App.Common;
using Synergy.App.Data;
using Synergy.App.Data.Model;
using Synergy.App.Data.ViewModel;
using static System.String;

namespace Synergy.App.Business.Implementation;

public class ColumnBusiness(
    IContextBase<ColumnViewModel, ColumnModel> repo,
    IServiceProvider sp)
    : BusinessBase<ColumnViewModel, ColumnModel>(repo, sp), IColumnBusiness
{
    private readonly IContextBase<ColumnViewModel, ColumnModel> _repo = repo;

    public async Task<List<ColumnViewModel>> GetList(string templateCode)
    {
        return await _repo.GetList(x => x.Table.Template.Key == templateCode);
    }

}