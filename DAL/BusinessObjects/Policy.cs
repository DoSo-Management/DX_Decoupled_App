using DAL.ValueObjects;
using DevExpress.Xpo;

namespace DAL.BusinessObjects
{
    public class Policy : DSEntityBase<Policy>
    {
        public Policy(Session session) : base(session) { }

        protected override void OnLoaded()
        {
            base.OnLoaded();

            PPremium = new PPremium(Premium, Currency);
        }

        public string Number { get; set; }
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
        public PPremium PPremium { get; set; }

        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);

            if (propertyName != nameof(PPremium))
                PPremium = new PPremium(Premium, Currency);
        }

        public void SetPolicyPremium() => PolicyPremium.Create(Premium, Currency);
    }

    public class PPremium : ValueObject<PPremium>
    {
        public PPremium(decimal premium, Currency currency)
        {
            if (currency != null)
            {
                Premium = premium;
                Currency = currency;
            }
        }

        public decimal Premium { get; }
        public decimal Premium2 => Premium * 2;
        public Currency Currency { get; }

        protected override bool EqualsCore(PPremium other) => other.Premium == Premium;

        protected override int GetHashCodeCore() => 5;
    }

    public class Currency : DSEntityBase<Currency>
    {
        public Currency(Session session) : base(session) { }
        public string CurrencyName { get; set; }
    }
}