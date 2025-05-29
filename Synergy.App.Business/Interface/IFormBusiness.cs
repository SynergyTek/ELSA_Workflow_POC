using System.Linq.Expressions;
using Synergy.App.Data.Model;
using Synergy.App.Data.ViewModel;

namespace Synergy.App.Business.Interface;

public interface IFormBusiness
{
    Task<CommandResult<List<IDictionary<string,object>>>> GetList(string templateCode);
    Task<CommandResult<dynamic>> GetSingleById(string templateCode, Guid id);
    Task<CommandResult<dynamic>> GetSingle(string templateCode, Expression<Func<TemplateViewModel, bool>> where);
    Task<CommandResult<dynamic>> Create(FormViewModel model);
    Task<CommandResult<dynamic>> Edit(FormViewModel model);
    Task<CommandResult<bool>> Delete(Guid id);
}