namespace BLL.Policy
{
    // ReSharper disable once InconsistentNaming
    public interface IPolicyBl
    {
        void CalculatePremium(DAL.BusinessObjects.Policy bo);
        //void SaveObject(Policy bo);
    }
}