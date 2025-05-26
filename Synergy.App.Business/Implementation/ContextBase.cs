using System.Linq.Expressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Synergy.App.Business.Interface;
using Synergy.App.Data;
using Synergy.App.Data.Models;

namespace Synergy.App.Business.Implementation;

public class ContextBase<TV, TD>(
    DbContextOptions<ApplicationDbContext> dbOptions,
    IUserContext userContext,
    IMapper autoMapper)
    : IContextBase<TV, TD>
    where TV : BaseModel
    where TD : BaseModel
{
    public IUserContext UserContext { get; set; }
    private DbContextOptions<ApplicationDbContext> DbOptions { get; set; } = dbOptions;

    public async Task<TV?> Create(TV model, bool autoCommit = true)
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

    public async Task<List<TV>> GetList(Expression<Func<TD, bool>> where,
        params Expression<Func<TD, object>>[] include)
    {
        return await GetList<TV, TD>(where, include);
    }

    private static Expression<Func<TE, bool>> AndAlso<TE>(Expression<Func<TE, bool>> expr1,
        Expression<Func<TE, bool>> expr2) where TE : BaseModel
    {
        var parameter = Expression.Parameter(typeof(TE));

        var leftVisitor = new ReplaceExpressionVisitor(expr1.Parameters[0], parameter);
        var left = leftVisitor.Visit(expr1.Body);

        var rightVisitor = new ReplaceExpressionVisitor(expr2.Parameters[0], parameter);
        var right = rightVisitor.Visit(expr2.Body);

        return Expression.Lambda<Func<TE, bool>>(
            Expression.AndAlso(left, right), parameter);
    }

    private static async Task CreateMaster<TDm>(ApplicationDbContext context, TDm baseModel) where TDm : BaseModel
    {
        await context.Set<TDm>().AddAsync(baseModel);
    }

    private static void EditMaster<TDm>(ApplicationDbContext context, TDm existingItem) where TDm : BaseModel
    {
        context.Entry(existingItem).State = EntityState.Modified;
    }

    public async Task<List<TVm>> GetList<TVm, TDm>() where TVm : BaseModel where TDm : BaseModel
    {
        var context = GetDbContext();
        try
        {
            var data = await context.Set<TDm>().AsNoTracking().Where(x => x.IsDeleted == false).ToListAsync();
            return data.ToViewModelList<TVm, TDm>(autoMapper);
        }
        finally
        {
            await DisposeDbContext(context);
        }
    }

    public async Task<List<TVm>> GetList<TVm, TDm>(Expression<Func<TDm, bool>> where,
        params Expression<Func<TDm, object>>[] include) where TVm : BaseModel where TDm : BaseModel
    {
        var context = GetDbContext();
        try
        {
            where = AndAlso(x => x.IsDeleted == false, where);
            if (include.Length > 0)
            {
                var set = context.Set<TDm>().Include(include[0]);
                set = include.Skip(1).Aggregate(set, (current, item) => current.Include(item));

                var data = await set.AsNoTracking().Where(where).ToListAsync();
                return data.ToViewModelList<TVm, TDm>(autoMapper);
            }

            var data2 = await context.Set<TDm>().AsNoTracking().Where(where).ToListAsync();
            return data2.ToViewModelList<TVm, TDm>(autoMapper);
        }
        finally
        {
            await DisposeDbContext(context);
        }
    }

    public async Task<TVm?> GetSingle<TVm, TDm>(Expression<Func<TDm, bool>> where,
        params Expression<Func<TDm, object>>[] include) where TVm : BaseModel where TDm : BaseModel
    {
        where = AndAlso(x => x.IsDeleted == false, where);
        var context = GetDbContext();
        try
        {
            if (include.Length > 0)
            {
                var set = context.Set<TDm>().Include(include[0]);
                set = include.Skip(1).Aggregate(set, (current, item) => current.Include(item));

                var data = await set.AsNoTracking().FirstOrDefaultAsync(where);
                return data?.ToViewModel<TVm, TDm>(autoMapper);
            }

            var result2 = await context.Set<TDm>().AsNoTracking().FirstOrDefaultAsync(where);
            return result2?.ToViewModel<TVm, TDm>(autoMapper);
        }
        finally
        {
            await DisposeDbContext(context);
        }
    }

    public async Task<TVm?> GetSingleById<TVm, TDm>(Guid id, params Expression<Func<TDm, object>>[] include)
        where TVm : BaseModel where TDm : BaseModel
    {
        if (id == Guid.Empty)
        {
            return null;
        }

        var context = GetDbContext();
        try
        {
            if (include.Length > 0)
            {
                var set = context.Set<TDm>().Include(include[0]);
                set = include.Skip(1).Aggregate(set, (current, item) => current.Include(item));
                var data = await set.AsNoTracking().FirstAsync(x => x.Id == id);

                var result = data.ToViewModel<TVm, TDm>(autoMapper);
                return result;
            }

            var result2 = await context.Set<TDm>().AsNoTracking().FirstAsync(x => x.Id == id);
            return result2.ToViewModel<TVm, TDm>(autoMapper);
        }
        finally
        {
            await DisposeDbContext(context);
        }
    }

    public async Task<TVm?> Create<TVm, TDm>(TVm model, bool autoCommit = true)
        where TVm : BaseModel where TDm : BaseModel
    {
        var baseModel = autoMapper.Map<TVm, TDm>(model);
        baseModel.Id = Guid.NewGuid();
        baseModel.CreatedBy = userContext.User;
        baseModel.UpdatedBy = userContext.User;
        var context = GetDbContext();
        try
        {
            await CreateMaster(context, baseModel);
            if (!autoCommit) return baseModel.ToViewModel<TVm, TDm>(autoMapper);
            context.Users.Attach(userContext.User);
            await context.SaveChangesAsync();

            return baseModel.ToViewModel<TVm, TDm>(autoMapper);
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


    public async Task<TVm> Edit<TVm, TDm>(TVm model, bool autoCommit = true)
        where TVm : BaseModel where TDm : BaseModel
    {
        var existingItem = await GetSingleById<TDm, TDm>(model.Id);
        if (existingItem == null)
        {
            throw new KeyNotFoundException($"Item with ID {model.Id} not found.");
        }

        model.CreatedAt = existingItem.CreatedAt;
        model.CreatedBy = existingItem.CreatedBy;
        model.UpdatedAt = DateTime.UtcNow;
        model.UpdatedBy = userContext.User;
        var baseModel = autoMapper.Map(model, existingItem);
        var context = GetDbContext();
        try
        {
            EditMaster(context, baseModel);
            await context.SaveChangesAsync();
            return model;
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

    private class ReplaceExpressionVisitor(Expression oldValue, Expression newValue) : ExpressionVisitor
    {
        public override Expression Visit(Expression node)
        {
            return node == oldValue ? newValue : base.Visit(node);
        }
    }
}