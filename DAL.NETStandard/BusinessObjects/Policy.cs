using System.Collections.Generic;
using System.Linq;
using DevExpress.Xpo;
using Domain.NETStandard;


namespace DAL.BusinessObjects
{
    //public class Policy : PolicyAbstract
    //{
    //    public Policy(Session session) : base(session)
    //    {
    //    }
    //}

    [DSტრუხა]
    //[NonPersistent]
    public class Policy : DSEntityBase<Policy>
    {
        protected Policy(Session session) : base(session) { }

        public string Number { get; private set; }
        public decimal SumInsured { get; private set; }
        public decimal Rate { get; private set; }
        public Currency Currency { get; private set; }

        [Association]
        public Client Client { get; private set; }

        public decimal Premium { get; private set; }

        public void SetPremiumAndSumInsured(decimal premium, decimal sumInsured)
        {
            Premium = premium;
            SumInsured = sumInsured;
        }

        public void SetPremium(decimal premium) => Premium = premium;

        [Association]
        public XPCollection<Schedule> SchedulesCollection => GetCollection<Schedule>(nameof(SchedulesCollection));

        public DoWrapper<PolicyPremium> PPremium => DoWrapper.Create(() => new PolicyPremium(Premium, Currency.CurrencyV.Value.Value));

        public override IEnumerable<string> ValueObjectNames => new[] { nameof(Premium), nameof(Currency) };

        //public override void SetValueObjects()
        //{
        //    PPremium = PolicyPremium.Create(Premium, Currency);
        //    ClearAndAddValueObject(PPremium);
        //}
    }
}