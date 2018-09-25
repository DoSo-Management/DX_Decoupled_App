using BLL.Policy;
using DAL.BusinessObjects;

namespace BLL
{
    // ReSharper disable once InconsistentNaming
    public class PolicyBl : BlBase<Policy>, IPolicyBl
    {
        public virtual void CalculatePremium(Policy bo) => bo.Premium = CalculatePremiumCore(bo.SumInsured, bo.Rate);
        public virtual bool ValidatePremium(Policy bo) => bo.Premium >= 0;
        public virtual decimal CalculatePremiumCore(decimal sumInsured, decimal rate) => sumInsured * rate;
    }
}