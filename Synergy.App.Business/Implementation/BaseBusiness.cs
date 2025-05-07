
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Synergy.App.Business.Interface;
using Synergy.App.Data.Models;

namespace Synergy.App.Business.Implementation
{
    public class BaseBusiness<TV, TD> : IBaseBusiness<TV, TD>
        where TV : BaseModel
        where TD : BaseModel
    {
        internal  IContextBase<TV, TD> _repo;

        public BaseBusiness(IContextBase<TV, TD> repo)
        {
            _repo = repo;
        }


        public virtual async Task<CommandResult<TV>> Create(TV model, bool autoCommit = true)
        {
            return await Create<TV, TD>(model, autoCommit);
        }

        public virtual async Task Delete(Guid id, bool autoCommit = true)
        {
            await this.Delete<TV, TD>(id, autoCommit);
        }

        public async virtual Task<CommandResult<TV>> Edit(TV model, bool autoCommit = true)
        {
            return await this.Edit<TV, TD>(model, autoCommit);
        }

        public virtual async Task<TV> GetSingle(Expression<Func<TD, bool>> where, params Expression<Func<TD, object>>[] include)
        {
            return await this.GetSingle<TV, TD>(where, include);
        }

        public virtual async Task<TV> GetSingleById(Guid id, params Expression<Func<TD, object>>[] include)
        {
            return await this.GetSingleById<TV, TD>(id, include);
        }

        public async virtual Task<List<TV>> GetList()
        {
            return await this.GetList<TV, TD>();
        }

        public virtual async Task<List<TV>> GetList(Expression<Func<TD, bool>> where, params Expression<Func<TD, object>>[] include)
        {
            return await this.GetList<TV, TD>(where, include);
        }


        public virtual async Task<List<TVm>> GetList<TVm, TDm>()
            where TVm : BaseModel
            where TDm : BaseModel
        {
            return await _repo.GetList<TVm, TDm>();
        }

        public virtual async Task<List<TVm>> GetList<TVm, TDm>(Expression<Func<TDm, bool>> where, params Expression<Func<TDm, object>>[] include)
            where TVm : BaseModel
            where TDm : BaseModel
        {
            return await _repo.GetList<TVm, TDm>(where, include);
        }

        public virtual async Task<TVm> GetSingle<TVm, TDm>(Expression<Func<TDm, bool>> where, params Expression<Func<TDm, object>>[] include)
            where TVm : BaseModel
            where TDm : BaseModel
        {
            return await _repo.GetSingle<TVm, TDm>(where, include);
        }

        public virtual async Task<TVm> GetSingleById<TVm, TDm>(Guid id, params Expression<Func<TDm, object>>[] include)
            where TVm : BaseModel
            where TDm : BaseModel
        {
            return await _repo.GetSingleById<TVm, TDm>(id, include);
        }

        public virtual async Task<CommandResult<TVm>> Create<TVm, TDm>(TVm model, bool autoCommit = true)
            where TVm : BaseModel
            where TDm : BaseModel
        {
            var result = await _repo.Create<TVm, TDm>(model, autoCommit);
            return CommandResult<TVm>.Instance(result);
        }

        public virtual async Task<CommandResult<TVm>> Edit<TVm, TDm>(TVm model, bool autoCommit = true)
            where TVm : BaseModel
            where TDm : BaseModel
        {
            var result = await _repo.Edit<TVm, TDm>(model, autoCommit);
            return CommandResult<TVm>.Instance(result);
        }

        public virtual async Task Delete<TVm, TDm>(Guid id, bool autoCommit = true)
            where TVm : BaseModel
            where TDm : BaseModel
        {
            await _repo.Delete<TVm, TDm>(id, autoCommit);
        }

    }
}
