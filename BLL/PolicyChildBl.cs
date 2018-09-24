using System;
using DAL.BusinessObjects;

namespace BLL
{
    // ReSharper disable once InconsistentNaming
    public class PolicyChildBl : PolicyBl
    {
        public override Type BoType => typeof(PolicyChld);

        public override void CalculatePremium(Policy bo)
        {
            base.CalculatePremium(bo);

            bo.Premium = bo.Premium * 4;
        }

        public void CalculatePremium2(Policy bo)
        {
            base.CalculatePremium(bo);
        }
    }
}