using System;
using System.Linq;
using BLL;
using DevExpress.Xpo;

namespace RepoServices
{
    // ReSharper disable once InconsistentNaming
    public class DBRepository<T> : IRepositoryID<T> where T : PersistentBase
    {
        public T Get(int key)
        {
            var uow = new UnitOfWork();

            return uow.GetObjectByKey<T>(key);
        }

        public IQueryable<T> GetAll()
        {
            var uow = new UnitOfWork();

            return uow.Query<T>();
        }

        public T Save(T obj)
        {
            var uow = obj.Session as UnitOfWork;
            uow.CommitChanges();

            return obj;
        }

        public T Create()
        {
            var uow = new UnitOfWork();

            return (T)Activator.CreateInstance(typeof(T), uow);
        }
    }
}
