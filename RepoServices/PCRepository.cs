using BLL;
using DAL.BusinessObjects;
using DevExpress.Xpo;

namespace RepoServices
{
    // ReSharper disable once InconsistentNaming
    public class PCRepository : IPCRepository
    {
        public EntityClasses2 GetFromDb(int key)
        {
            var uow = new UnitOfWork();

            return uow.GetObjectByKey<EntityClasses2>(key);
        }

        public void SaveObject(EntityClasses2 bo)
        {
            var uow = bo.Session as UnitOfWork;
            uow.CommitChanges();
        }

        public EntityClasses2 CreateNewObject()
        {
            var uow = new UnitOfWork();

            return new EntityClasses2(uow);
        }
    }
}
