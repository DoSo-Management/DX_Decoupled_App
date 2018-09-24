using System;
using System.Linq;
using DevExpress.Xpo;

namespace BLL
{
    // ReSharper disable once InconsistentNaming
    public interface IRepositoryID<T> : IRepository<T, int> where T : PersistentBase { }
    // ReSharper disable once InconsistentNaming
    public interface IRepositoryOID<T> : IRepository<T, Guid> where T : PersistentBase { }

    // ReSharper disable once InconsistentNaming
    public interface IRepository<T, in TKeyType> where T : PersistentBase
    {
        T Get(TKeyType key);
        IQueryable<T> GetAll();
        T Save(T obj);
        T Create();
    }
}