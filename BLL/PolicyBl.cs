using DAL.BusinessObjects;

namespace BLL
{
    //ReSharper disable once InconsistentNaming
    public class PolicyBl : BlBase<Policy>, IPolicyBl
    {
        public virtual void CalculatePremium(Policy bo) => bo.SetPremiumAndSumInsured(CalculatePremiumCore(bo.SumInsured, bo.Rate), 0);
        public virtual bool ValidatePremium(Policy bo) => bo.Premium >= 0;
        public virtual decimal CalculatePremiumCore(decimal sumInsured, decimal rate) => sumInsured * rate;

        public override void OnSaving(Policy bo)
        {
            base.OnSaving(bo);
        }
    }
}