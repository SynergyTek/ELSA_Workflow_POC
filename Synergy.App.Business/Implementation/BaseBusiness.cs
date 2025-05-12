using System.Linq.Expressions;
using Elsa.Workflows.Management;
using Elsa.Workflows.Management.Filters;
using Elsa.Workflows.Models;
using Elsa.Workflows.Runtime;
using Microsoft.Extensions.DependencyInjection;
using Synergy.App.Business.Interface;
using Synergy.App.Data.Models;
using Workflow = Elsa.Workflows.Activities.Workflow;

namespace Synergy.App.Business.Implementation
{
    public class BaseBusiness<TV, TD>(IContextBase<TV, TD> repo, IServiceProvider sp) : IBaseBusiness<TV, TD>
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

        public virtual async Task<TV> GetSingle(Expression<Func<TD, bool>> where,
            params Expression<Func<TD, object>>[] include)
        {
            return await GetSingle<TV, TD>(where, include);
        }

        public virtual async Task<TV> GetSingleById(Guid id, params Expression<Func<TD, object>>[] include)
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

        public virtual async Task<TVm> GetSingle<TVm, TDm>(Expression<Func<TDm, bool>> where,
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
            var workflowDefinitionService = sp.GetService<IWorkflowDefinitionService>();
            var workflowStarter = sp.GetService<IWorkflowStarter>();
            var userContext = sp.GetService<IUserContext>();
            if (workflowDefinitionService == null || workflowStarter == null)
            {
                throw new Exception("Workflow services not registered");
            }

            var workflowName = typeof(TDm).Name.ToUpper();

            #region Pre Submission Logic

            var preWorkflow = await workflowDefinitionService.FindWorkflowDefinitionAsync(new WorkflowDefinitionFilter
            {
                Name = "PRE_" + workflowName
            });
            if (preWorkflow != null)
            {
                var request = new StartWorkflowRequest
                {
                    WorkflowDefinitionHandle = new WorkflowDefinitionHandle
                    {
                        DefinitionId = preWorkflow.DefinitionId
                    },
                    Input = new Dictionary<string, object>
                    {
                        { "Model", model }
                    }
                };
                await workflowStarter.StartWorkflowAsync(request);
            }

            #endregion

            var result = await repo.Create<TVm, TDm>(model, autoCommit);

            #region Post Submission Logic

            var postWorkflow = await workflowDefinitionService.FindWorkflowDefinitionAsync(new WorkflowDefinitionFilter
            {
                Name = "POST_" + workflowName
            });
            if (postWorkflow == null) return CommandResult<TVm>.Instance(result);
            {
                var request = new StartWorkflowRequest
                {
                    WorkflowDefinitionHandle = new WorkflowDefinitionHandle
                    {
                        DefinitionId = postWorkflow.DefinitionId
                    },
                    Input = new Dictionary<string, object>
                    {
                        { "Model", model },
                        { "ByUserId", userContext.Id },
                    }
                };
                await workflowStarter.StartWorkflowAsync(request);
            }

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
}