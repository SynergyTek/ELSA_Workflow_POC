using System.Linq.Expressions;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Synergy.App.Data;
using Synergy.App.Data.Models;

namespace Synergy.App.Business.Implementation
{
    public class ContextBase<TV, TD> : IContextBase<TV, TD> where TV : BaseModel where TD : BaseModel
    {

        private readonly IMapper _autoMapper;
        private ApplicationDbContext _dbContext;

        public DbContextOptions<ApplicationDbContext> DbOptions { get; set; }

        public ContextBase(DbContextOptions<ApplicationDbContext> dbOptions, IConfiguration configuration, IMapper autoMapper
            , IServiceProvider sp, ApplicationDbContext dbContext)
        {
            _autoMapper = autoMapper;
            _dbContext = dbContext;
            DbOptions = dbOptions;
        }
        public async Task<TV> Create(TV model, bool autoCommit = true)
        {

            return await Create<TV, TD>(model, autoCommit);

        }


        public async Task Delete(Guid id, bool autoCommit = true)
        {
            await Delete<TV, TD>(id, autoCommit);
        }

        public async Task<TV> Edit(TV model, bool autoCommit = true)
        {
            return await Edit<TV, TD>(model, autoCommit);
        }

        public async Task<TV> GetSingle(Expression<Func<TD, bool>> where, params Expression<Func<TD, object>>[] include)
        {
            return await GetSingle<TV, TD>(where, include);
        }

        public async Task<TV> GetSingleById(Guid id, params Expression<Func<TD, object>>[] include)
        {
            return await GetSingleById<TV, TD>(id, include);
        }

        public async Task<List<TV>> GetList()
        {
            return await GetList<TV, TD>();
        }

        public async Task<List<TV>> GetList(Expression<Func<TD, bool>> where, params Expression<Func<TD, object>>[] include)
        {
            return await GetList<TV, TD>(where, include);
        }
        private static Expression<Func<E, bool>> AndAlso<E>(Expression<Func<E, bool>> expr1, Expression<Func<E, bool>> expr2) where E : BaseModel
        {
            var parameter = Expression.Parameter(typeof(E));

            var leftVisitor = new ReplaceExpressionVisitor(expr1.Parameters[0], parameter);
            var left = leftVisitor.Visit(expr1.Body);

            var rightVisitor = new ReplaceExpressionVisitor(expr2.Parameters[0], parameter);
            var right = rightVisitor.Visit(expr2.Body);

            return Expression.Lambda<Func<E, bool>>(
                Expression.AndAlso(left, right), parameter);
        }
        public static async Task CreateMaster<TDm>(ApplicationDbContext context, TDm baseModel) where TDm : BaseModel
        {
            await context.Set<TDm>().AddAsync(baseModel);
        }
        private async Task EditMaster<DM>(ApplicationDbContext context, DM existingItem) where DM : BaseModel
        {
            context.Entry<DM>(existingItem).State = EntityState.Modified;
        }

        public async Task<List<TVm>> GetList<TVm, TDm>() where TVm : BaseModel where TDm : BaseModel
        {
            var context = GetDbContext();
            try
            {

                var data = await context.Set<TDm>().AsNoTracking().Where(x => x.IsDeleted == false).ToListAsync();
                return data.ToViewModelList<TVm, TDm>(_autoMapper);
            }
            catch (Exception e)
            {

                throw;
            }
            finally
            {
                await DisposeDbContext(context);
            }

        }

        public async Task<List<TVm>> GetList<TVm, TDm>(Expression<Func<TDm, bool>> where, params Expression<Func<TDm, object>>[] include) where TVm : BaseModel where TDm : BaseModel
        {

            var context = GetDbContext();
            try
            {
                where = AndAlso<TDm>(x => x.IsDeleted == false, where);
                if (include != null && include.Length > 0)
                {

                    var set = context.Set<TDm>().Include(include[0]);
                    foreach (var item in include.Skip(1))
                    {
                        set = set.Include(item);
                    }
                    var data = await set.AsNoTracking().Where(where).ToListAsync();
                    return data.ToViewModelList<TVm, TDm>(_autoMapper);
                }

                var data2 = await context.Set<TDm>().AsNoTracking().Where(where).ToListAsync();
                return data2.ToViewModelList<TVm, TDm>(_autoMapper);
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                await DisposeDbContext(context);
            }
        }

        public async Task<TVm> GetSingle<TVm, TDm>(Expression<Func<TDm, bool>> where, params Expression<Func<TDm, object>>[] include) where TVm : BaseModel where TDm : BaseModel
        {
            where = AndAlso<TDm>(x => x.IsDeleted == false, where);
            var context = GetDbContext();
            try
            {
                if (include != null && include.Length > 0)
                {
                    var set = context.Set<TDm>().Include(include[0]);
                    foreach (var item in include.Skip(1))
                    {
                        set = set.Include(item);
                    }
                    var data = await set.AsNoTracking().FirstOrDefaultAsync(where);
                    return data.ToViewModel<TVm, TDm>(_autoMapper);
                }
                var result2 = await context.Set<TDm>().AsNoTracking().FirstOrDefaultAsync(where);
                return result2.ToViewModel<TVm, TDm>(_autoMapper);
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                await DisposeDbContext(context);
            }


        }

        public async Task<TVm> GetSingleById<TVm, TDm>(Guid id, params Expression<Func<TDm, object>>[] include) where TVm : BaseModel where TDm : BaseModel
        {
            var context = GetDbContext();
            try
            {
                if (include != null && include.Length > 0)
                {
                    var set = context.Set<TDm>().Include(include[0]);
                    foreach (var item in include.Skip(1))
                    {
                        set = set.Include(item);
                    }
                    var data = await set.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
                    var result = data.ToViewModel<TVm, TDm>(_autoMapper);
                    return result;
                }
                var result2 = await context.Set<TDm>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
                return result2.ToViewModel<TVm, TDm>(_autoMapper);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                await DisposeDbContext(context);
            }

        }

        public async Task<TVm> Create<TVm, TDm>(TVm model, bool autoCommit = true) where TVm : BaseModel where TDm : BaseModel
        {
            TDm baseModel = null;
            baseModel = _autoMapper.Map<TVm, TDm>(model);

            var context = GetDbContext();
            try
            {
                CreateMaster(context, baseModel).Wait();
                if (autoCommit)
                {
                    await context.SaveChangesAsync();
                }

                return model;
            }
            finally
            {
                await DisposeDbContext(context);
            }

        }


        public async Task DisposeDbContext(ApplicationDbContext context)
        {
            await context.Database.CloseConnectionAsync();
            await context.DisposeAsync();
        }

        public ApplicationDbContext GetDbContext()
        {
            return new ApplicationDbContext(DbOptions);
        }


        public async Task<TVm> Edit<TVm, TDm>(TVm model, bool autoCommit = true) where TVm : BaseModel where TDm : BaseModel
        {
            var existingItem = await GetSingleById<TDm, TDm>(model.Id);
            if (existingItem != null)
            {
                model.CreatedDate = existingItem.CreatedDate;
                model.CreatedBy = existingItem.CreatedBy;
            }

            model.LastUpdatedDate = DateTime.UtcNow;
            var baseModel = _autoMapper.Map<TVm, TDm>(model, existingItem);
            var context = GetDbContext();
            try
            {

               await EditMaster<TDm>(context, baseModel);
                await context.SaveChangesAsync();
                return model;
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                await DisposeDbContext(context);
            }

        }

        public async Task Delete<TVm, TDm>(Guid id, bool autoCommit = true) where TVm : BaseModel where TDm : BaseModel
        {
            var model = await GetSingleById<TVm, TDm>(id);
            model.IsDeleted = true;
            await Edit<TVm, TDm>(model);
        }

        private class ReplaceExpressionVisitor
            : ExpressionVisitor
        {
            private readonly Expression _oldValue;
            private readonly Expression _newValue;

            public ReplaceExpressionVisitor(Expression oldValue, Expression newValue)
            {
                _oldValue = oldValue;
                _newValue = newValue;
            }

            public override Expression Visit(Expression node)
            {
                if (node == _oldValue)
                    return _newValue;
                return base.Visit(node);
            }
        }
    }

}
