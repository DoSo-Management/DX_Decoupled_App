using System.Collections.Generic;
using DAL.ValueObjects;
using DevExpress.Xpo;

namespace DAL.BusinessObjects
{
    public class Policy : DSEntityBase<Policy>
    {
        public Policy(Session session) : base(session) { }

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
        public PolicyPremium PPremium { get; set; }

        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);

            if (propertyName != nameof(Premium) && propertyName != nameof(Currency)) return;
        }

        public override IEnumerable<string> ValueObjectNames => new[] { nameof(Premium), nameof(Currency) };

        public override void SetValueObjects()
        {
            PPremium = PolicyPremium.Create(Premium, Currency);
            ClearAndAddValueObject(PPremium);
        }
    }


    public class Currency : DSEntityBase<Currency>
    {
        public Currency(Session session) : base(session) { }
        public string CurrencyName { get; set; }
    }
}