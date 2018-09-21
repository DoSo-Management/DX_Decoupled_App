using DAL.BusinessObjects;

namespace BLL
{
    // ReSharper disable once InconsistentNaming
    public interface IPCRepository
    {
        EntityClasses2 GetFromDb(int key);
        void SaveObject(EntityClasses2 bo);
        EntityClasses2 CreateNewObject();
    }
}