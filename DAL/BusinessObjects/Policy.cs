using DAL.ValueObjects;
using DevExpress.Xpo;

namespace DAL.BusinessObjects
{
    public class Policy : DSEntityBase<Policy>
    {

        public Policy(Session session) : base(session) { }

        public decimal SumInsured { get; set; }
        public decimal Rate { get; set; }
        public Currency Currency { get; set; }
        public Client Client { get; set; }

        decimal _premium;
        public decimal Premium
        {
            get => _premium;
            set => SetPropertyValue(nameof(Premium), ref _premium, value);
        }

        public void SetPolicyPremium() => new PolicyPremium(Premium, Currency);
        public PolicyPremium PolicyPremium { get; set; }
    }

    public class Currency : DSEntityBase<Currency>
    {
        public Currency(Session session) : base(session) { }
        public string CurrencyName { get; set; }
    }
}