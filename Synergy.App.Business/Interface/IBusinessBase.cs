using System.Linq.Expressions;
using Synergy.App.Data.Models;
using Task = System.Threading.Tasks.Task;

namespace Synergy.App.Business.Interface
{
    public interface IBusinessBase<TV, TD> where TV : BaseModel where TD : BaseModel
    {
        Task<List<TV>> GetList();
        Task<List<TV>> GetList(Expression<Func<TD, bool>> where, params Expression<Func<TD, object>>[] include);
        Task<TV> GetSingle(Expression<Func<TD, bool>> where, params Expression<Func<TD, object>>[] include);
        Task<TV> GetSingleById(Guid id, params Expression<Func<TD, object>>[] include);
        Task<CommandResult<TV>> Create(TV model, bool autoCommit = true);

        Task<CommandResult<TV>> Edit(TV model, bool autoCommit = true);
        Task Delete(Guid id, bool autoCommit = true);


        Task<List<TVm>> GetList<TVm, TDm>() where TVm : BaseModel where TDm : BaseModel;

        Task<List<TVm>> GetList<TVm, TDm>(Expression<Func<TDm, bool>> where,
            params Expression<Func<TDm, object>>[] include) where TVm : BaseModel where TDm : BaseModel;

        Task<TVm> GetSingle<TVm, TDm>(Expression<Func<TDm, bool>> where, params Expression<Func<TDm, object>>[] include)
            where TVm : BaseModel where TDm : BaseModel;

        Task<TVm?> GetSingleById<TVm, TDm>(Guid id, params Expression<Func<TDm, object>>[] include)
            where TVm : BaseModel where TDm : BaseModel;

        Task<CommandResult<TVm>> Create<TVm, TDm>(TVm model, bool autoCommit = true)
            where TVm : BaseModel where TDm : BaseModel;

        Task<CommandResult<TVm>> Edit<TVm, TDm>(TVm model, bool autoCommit = true)
            where TVm : BaseModel where TDm : BaseModel;

        Task Delete<TVm, TDm>(Guid id, bool autoCommit = true) where TVm : BaseModel where TDm : BaseModel;
    }
}