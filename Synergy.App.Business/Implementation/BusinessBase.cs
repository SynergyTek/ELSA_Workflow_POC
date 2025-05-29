using System.Linq.Expressions;
using Microsoft.Extensions.DependencyInjection;
using Synergy.App.Business.Interface;
using Synergy.App.Data.Model;

namespace Synergy.App.Business.Implementation;

public class BusinessBase<TV, TD>(IContextBase<TV, TD> repo, IServiceProvider sp) : IBusinessBase<TV, TD>
    where TV : BaseModel
    where TD : BaseModel
{
    public virtual async Task<CommandResult<TV>> Create(TV model, bool autoCommit = true)
    {
        return await Create<TV, TD>(model, autoCommit);
    }

    public virtual async Task Delete(Guid id, bool autoCommit = true)
    {
        await Delete<TV, TD>(id, autoCommit);
    }

    public virtual async Task<CommandResult<TV>> Edit(TV model, bool autoCommit = true)
    {
        return await Edit<TV, TD>(model, autoCommit);
    }

    public virtual async Task<TV?> GetSingle(Expression<Func<TD, bool>> where,
        params Expression<Func<TD, object>>[] include)
    {
        return await GetSingle<TV, TD>(where, include);
    }

    public virtual async Task<TV?> GetSingleById(Guid id, params Expression<Func<TD, object>>[] include)
    {
        return await GetSingleById<TV, TD>(id, include);
    }

    public virtual async Task<List<TV>> GetList()
    {
        return await GetList<TV, TD>();
    }

    public virtual async Task<List<TV>> GetList(Expression<Func<TD, bool>> where,
        params Expression<Func<TD, object>>[] include)
    {
        return await GetList<TV, TD>(where, include);
    }


    public virtual async Task<List<TVm>> GetList<TVm, TDm>()
        where TVm : BaseModel
        where TDm : BaseModel
    {
        return await repo.GetList<TVm, TDm>();
    }

    public virtual async Task<List<TVm>> GetList<TVm, TDm>(Expression<Func<TDm, bool>> where,
        params Expression<Func<TDm, object>>[] include)
        where TVm : BaseModel
        where TDm : BaseModel
    {
        return await repo.GetList<TVm, TDm>(where, include);
    }

    public virtual async Task<TVm?> GetSingle<TVm, TDm>(Expression<Func<TDm, bool>> where,
        params Expression<Func<TDm, object>>[] include)
        where TVm : BaseModel
        where TDm : BaseModel
    {
        return await repo.GetSingle<TVm, TDm>(where, include);
    }

    public virtual async Task<TVm?> GetSingleById<TVm, TDm>(Guid id, params Expression<Func<TDm, object>>[] include)
        where TVm : BaseModel
        where TDm : BaseModel
    {
        return await repo.GetSingleById<TVm, TDm>(id, include);
    }

    public virtual async Task<CommandResult<TVm>> Create<TVm, TDm>(TVm model, bool autoCommit = true)
        where TVm : BaseModel
        where TDm : BaseModel
    {
        var workflowBusiness = sp.GetService<IWorkflowBusiness>();
        var userContext = sp.GetService<IUserContext>();

        if (workflowBusiness == null || userContext == null)
            throw new Exception("WorkflowBusiness or UserContext is not registered in the service provider.");
        var workflowName = typeof(TDm).Name.ToUpper();

        #region Pre Submission Logic

        await workflowBusiness.StartWorkflow("PRE_" + workflowName, new()
        {
            { "Model", model },
            { "User", userContext },
        });

        #endregion

        var result = await repo.Create<TVm, TDm>(model, autoCommit);

        if (result == null)
        {
            return CommandResult<TVm>.Instance(model, false,
                new Dictionary<string, string> { { "Error", "Failed to create the model." } });
        }

        #region Post Submission Logic

        await workflowBusiness.StartWorkflow("POST_" + workflowName, new()
        {
            { "Model", model },
            { "User", userContext },
        });

        #endregion

        return CommandResult<TVm>.Instance(result);
    }

    public virtual async Task<CommandResult<TVm>> Edit<TVm, TDm>(TVm model, bool autoCommit = true)
        where TVm : BaseModel
        where TDm : BaseModel
    {
        var result = await repo.Edit<TVm, TDm>(model, autoCommit);
        return CommandResult<TVm>.Instance(result);
    }

    public virtual async Task Delete<TVm, TDm>(Guid id, bool autoCommit = true)
        where TVm : BaseModel
        where TDm : BaseModel
    {
        await repo.Delete<TVm, TDm>(id, autoCommit);
    }
}