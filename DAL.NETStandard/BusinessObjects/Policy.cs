using System.Collections.Generic;
using DAL.ValueObjects;
using DevExpress.Xpo;

namespace DAL.BusinessObjects
{
    [DSტრუხა]
    public class Policy : DSEntityBase<Policy>
    {
        public Policy(Session session) : base(session) { }

        public string Number { get; set; }
        public decimal SumInsured { get; set; }
        public decimal Rate { get; set; }
        public Currency Currency { get; set; }
        [Association]
        public Client Client { get; set; }

        decimal _premium;
        public decimal Premium
        {
            get => _premium;
            set => SetPropertyValue(nameof(Premium), ref _premium, value);
        }

        [Association]
        public XPCollection<Schedule> SchedulesCollection => GetCollection<Schedule>(nameof(SchedulesCollection));

        public PolicyPremium PPremium { get; set; }

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

    public class CurrencyV : ValueObject
    {
        public CurrencyV(string currencyName)
        {
            CurrencyName = currencyName;
        }

        public string CurrencyName { get; }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return CurrencyName;
        }
    }
}