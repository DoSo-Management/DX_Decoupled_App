using DAL.BusinessObjects;

namespace ClassLibrary3
{
    public interface ILogicSpecificTo<T> { }
    public interface IPersistentClasses2Bl
    {
        void CalculatePremium(EntityClasses2 bo);
        EntityClasses2 GetFromDb(int key);
        void SaveObject(EntityClasses2 bo);

        EntityClasses2 CreateNewObject();
        //EntityClasses2 GetFromDb(int key);
    }
}