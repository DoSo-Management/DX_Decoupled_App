using System.Collections.Generic;
using BLL;
using DAL.BusinessObjects;
using DevExpress.Xpo;

namespace RepoServices
{
    // ReSharper disable once InconsistentNaming
    public class PCRepository : IPCRepository
    {
        public EC2 GetFromDb(int key)
        {
            var uow = new UnitOfWork();

            return uow.GetObjectByKey<EC2>(key);
        }

        public IEnumerable<EC2> GetAllFromDb()
        {
            var uow = new UnitOfWork();

            return uow.Query<EC2>();
        }

        public void SaveObject(EC2 bo)
        {
            var uow = bo.Session as UnitOfWork;
            uow.CommitChanges();
        }

        public EC2 CreateNewObject()
        {
            var uow = new UnitOfWork();

            return new EC2(uow);
        }
    }
}
