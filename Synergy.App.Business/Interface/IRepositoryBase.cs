//using Synergy.App.Common;
////using Synergy.App.DataModel;
////using MongoDB.Driver;
//using System;
//using System.Collections.Generic;
//using System.Linq.Expressions;
//using System.Threading.Tasks;
//using System.IO;
//using Synergy.App.Data;
//using Synergy.App.Data.Models;
//using Synergy.App.Business.Implementation;
//using Synergy.App.Data.ViewModels;

//namespace Synergy.App.Business.Interface
//{
//    public interface IRepositoryBase<V, D> where V : BaseModel where D : BaseModel
//    {
//        IUserContext UserContext { get; set; }
//    //    ContextBase<UserViewModel,User> GetDbContext();
//        Task CommitChanges();
//       // Task DisposeDbContext(ContextBase<UserViewModel,User> context);

//        Task<List<V>> GetList();
//     //   Task<List<V>> GetActiveList();
//        Task<List<V>> GetList(Expression<Func<D, bool>> where, params Expression<Func<D, object>>[] include);
//        Task<V> GetSingle(Expression<Func<D, bool>> where, params Expression<Func<D, object>>[] include);
//        Task<V> GetSingleGlobal(Expression<Func<D, bool>> where, params Expression<Func<D, object>>[] include);
//        Task<V> GetSingleById(string id, params Expression<Func<D, object>>[] include);
//        Task<V> Create(V model, bool autoCommit = true);
//        Task<V> CreateMigrate(V model, bool autoCommit = true);
//     //   Task<V> CreateGlobal(V model, bool autoCommit = true);
//        Task<List<V>> CreateMany(List<V> models, bool autoCommit = true);
//        Task<V> Edit(V model, bool autoCommit = true);
//     //   Task<V> EditGlobal(V model, bool autoCommit = true);
//        Task Delete(string id, bool autoCommit = true);
//        Task<List<V>> GetListGlobal();
//        Task<List<V>> GetListGlobal(Expression<Func<D, bool>> where, params Expression<Func<D, object>>[] include);


//        Task<List<VM>> GetList<VM, DM>() where VM : BaseModel where DM : BaseModel;
//      //  Task<List<VM>> GetActiveList<VM, DM>() where VM : BaseModel where DM : BaseModel;
//        Task<List<VM>> GetList<VM, DM>(Expression<Func<DM, bool>> where, params Expression<Func<DM, object>>[] include) where VM : BaseModel where DM : BaseModel;
//        Task<VM> GetSingle<VM, DM>(Expression<Func<DM, bool>> where, params Expression<Func<DM, object>>[] include) where VM : BaseModel where DM : BaseModel;
//        Task<VM> GetSingleGlobal<VM, DM>(Expression<Func<DM, bool>> where, params Expression<Func<DM, object>>[] include) where VM : BaseModel where DM : BaseModel;
//        Task<VM> GetSingleById<VM, DM>(string id, params Expression<Func<DM, object>>[] include) where VM : BaseModel where DM : BaseModel;
//        Task<VM> Create<VM, DM>(VM model, bool autoCommit = true) where VM : BaseModel where DM : BaseModel;
//        Task<VM> CreateMigrate<VM, DM>(VM model, bool autoCommit = true) where VM : BaseModel where DM : BaseModel;
//      //  Task<VM> CreateGlobal<VM, DM>(VM model, bool autoCommit = true) where VM : BaseModel where DM : BaseModel;

//        Task<List<VM>> CreateMany<VM, DM>(List<VM> models, bool autoCommit = true) where VM : BaseModel where DM : BaseModel;
//        Task<VM> Edit<VM, DM>(VM model, bool autoCommit = true) where VM : BaseModel where DM : BaseModel;
//        //Task<VM> EditGlobal<VM, DM>(VM model, bool autoCommit = true) where VM : BaseModel where DM : BaseModel;
//        Task Delete<VM, DM>(string id, bool autoCommit = true) where VM : BaseModel where DM : BaseModel;
//        //Task<List<VM>> GetListGlobal<VM, DM>() where VM : BaseModel where DM : BaseModel;
//        //Task<List<VM>> GetListGlobal<VM, DM>(Expression<Func<DM, bool>> where, params Expression<Func<DM, object>>[] include) where VM : BaseModel where DM : BaseModel;
     
//    }
//}
