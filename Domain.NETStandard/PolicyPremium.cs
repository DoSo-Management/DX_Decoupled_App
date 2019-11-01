using System.Collections.Generic;
using CSharpFunctionalExtensions;
// ReSharper disable PossibleNullReferenceException

namespace Domain.NETStandard
{
    public abstract class ValueObject2 : ValueObject
    {
        public abstract override string ToString();
    }

    public class PolicyPremium : ValueObject2
    {
        public const string PremiumShouldBeMoreThan0 = "Premium Should Be More Than 0";
        public const string CurrencyShouldNotBeNull = "Currency Should Not Be Null";
        public const string CurrencyNameShouldNotBeNull = "Currency Name Should Not Be Null";
        public PolicyPremium(decimal premium, CurrencyV currency)
        {
            Check.That(
                ExceptionExtensions.FailWith(() => premium < 0, PremiumShouldBeMoreThan0)
                        .And(() => currency == null, CurrencyShouldNotBeNull)
                        .And(() => currency.CurrencyName == null, CurrencyNameShouldNotBeNull));

            Premium = premium;
            Currency = currency;
        }

        //public static PolicyPremium Create(decimal premium, Currency currency) => new PolicyPremium(premium, currency);

        public decimal Premium { get; }
        public CurrencyV Currency { get; }


        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Currency.CurrencyName.ToUpper();
            yield return Premium;
        }

        public override string ToString() => $"{Premium}[{Currency.CurrencyName}]";
    }
}