using DAL.BusinessObjects;

namespace BLL
{
    public interface IPersistentClasses2Bl
    {
        void CalculatePremium(EntityClasses2 bo);
        EntityClasses2 GetFromDb(int key);
        void SaveObject(EntityClasses2 bo);

        EntityClasses2 CreateNewObject();
    }
}