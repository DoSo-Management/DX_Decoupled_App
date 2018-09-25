using System;
using System.Linq;
using BLL;
using DevExpress.Xpo;

namespace RepoServices
{
    // ReSharper disable once InconsistentNaming
    public class DBRepository<T> : IRepositoryID<T> where T : PersistentBase
    {
        readonly UnitOfWork uow;
        public DBRepository(UnitOfWork uow)
        {
            this.uow = uow;
        }
        public T Get(int key)
        {
            return uow.GetObjectByKey<T>(key);
        }

        public IQueryable<T> GetAll()
        {
            return uow.Query<T>();
        }

        public T Save(T obj)
        {
            uow.CommitChanges();

            return obj;
        }

        public T Create()
        {
            return (T)Activator.CreateInstance(typeof(T), uow);
        }
    }
}
