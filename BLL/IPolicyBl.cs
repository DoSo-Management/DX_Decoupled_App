using DAL.BusinessObjects;

namespace BLL
{
    // ReSharper disable once InconsistentNaming
    public interface IPolicyBl
    {
        void CalculatePremium(Policy bo);
        //void SaveObject(Policy bo);
    }
}