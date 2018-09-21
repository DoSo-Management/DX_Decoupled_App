using System.Collections.Generic;
using DAL.BusinessObjects;

namespace BLL
{
    // ReSharper disable once InconsistentNaming
    public interface IPCRepository
    {
        EC2 GetFromDb(int key);
        IEnumerable<EC2> GetAllFromDb();
        void SaveObject(EC2 bo);
        EC2 CreateNewObject();
    }
}