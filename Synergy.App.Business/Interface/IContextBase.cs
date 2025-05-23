using System.Linq.Expressions;
using Synergy.App.Data;
using Synergy.App.Data.Models;
using Task = System.Threading.Tasks.Task;

namespace Synergy.App.Business.Interface
{
    public interface IContextBase<TV, TD> where TV : BaseModel where TD : BaseModel
    {
        IUserContext UserContext { get; set; }
        ApplicationDbContext GetDbContext();
        Task DisposeDbContext(ApplicationDbContext context);

        Task<List<TV>> GetList();
        Task<List<TV>> GetList(Expression<Func<TD, bool>> where, params Expression<Func<TD, object>>[] include);
        Task<TV> GetSingle(Expression<Func<TD, bool>> where, params Expression<Func<TD, object>>[] include);
        Task<TV> GetSingleById(Guid id, params Expression<Func<TD, object>>[] include);
        Task<TV?> Create(TV model, bool autoCommit = true);
        Task<TV> Edit(TV model, bool autoCommit = true);
        Task Delete(Guid id, bool autoCommit = true);

        Task<List<TVm>> GetList<TVm, TDm>() where TVm : BaseModel where TDm : BaseModel;

        Task<List<TVm>> GetList<TVm, TDm>(Expression<Func<TDm, bool>> where, params Expression<Func<TDm, object>>[] include)
            where TVm : BaseModel where TDm : BaseModel;

        Task<TVm?> GetSingle<TVm, TDm>(Expression<Func<TDm, bool>> where, params Expression<Func<TDm, object>>[] include)
            where TVm : BaseModel where TDm : BaseModel;


        Task<TVm?> GetSingleById<TVm, TDm>(Guid id, params Expression<Func<TDm, object>>[] include)
            where TVm : BaseModel where TDm : BaseModel;

        Task<TVm?> Create<TVm, TDm>(TVm model, bool autoCommit = true) where TVm : BaseModel where TDm : BaseModel;

        Task<TVm> Edit<TVm, TDm>(TVm model, bool autoCommit = true) where TVm : BaseModel where TDm : BaseModel;
        Task Delete<TVm, TDm>(Guid id, bool autoCommit = true) where TVm : BaseModel where TDm : BaseModel;

    }
}