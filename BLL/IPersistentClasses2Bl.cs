using System.Collections.Generic;
using DAL.BusinessObjects;

namespace BLL
{
    public interface IPersistentClasses2Bl
    {
        void CalculatePremium(EC2 bo);
        //EC2 GetFromDb(int key);
        //IEnumerable<EC2> GetAllFromDb();
        void SaveObject(EC2 bo);

        //EC2 CreateNewObject();
    }
}