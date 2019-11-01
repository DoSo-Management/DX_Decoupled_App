using System;
using System.Collections.Generic;
using System.Linq;
using CSharpFunctionalExtensions;
using DevExpress.Xpo;
using Domain.NETStandard;


namespace DAL.BusinessObjects
{
    [DSტრუხა]
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

    public class DoWrapper
    {
        public static DoWrapper<T2> Create<T2>(Func<T2> funcT) => new DoWrapper<T2>(funcT);
    }

    public class DoWrapper<T>
    {
        public DoWrapper(Func<T> funcT)
        {
            Value = Result.Try(funcT, exception => exception.Message);
        }

        public Result<T> Value { get; }
        public override string ToString() =>
            Value.IsSuccess
                ? $"S[{Value.Value}]"
                : $"F[{Value.Error}]";
    }

    public class Currency : DSEntityBase<Currency>
    {
        public Currency(Session session) : base(session) { }

        public string CurrencyName { get; set; }
        //public DoWrapperContainer
        public DoWrapper<CurrencyV> CurrencyV => DoWrapper.Create(() => new CurrencyV(CurrencyName));
    }

    
}