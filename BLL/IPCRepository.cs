using DAL.BusinessObjects;

namespace ClassLibrary3
{
    public interface IPCRepository
    {
        EntityClasses2 GetFromDb(int key);
        void SaveObject(EntityClasses2 bo);
        EntityClasses2 CreateNewObject();
    }
}