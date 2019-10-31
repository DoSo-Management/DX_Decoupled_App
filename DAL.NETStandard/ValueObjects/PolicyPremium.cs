using System.Collections.Generic;
using System.Linq;
using CSharpFunctionalExtensions;
using DAL.BusinessObjects;

namespace DAL.ValueObjects
{
    public class PolicyPremium : ValueObject
    {
        public const string PremiumShouldBeMoreThan0 = "Premium Should Be More Than 0";
        public const string CurrencyShouldNotBeNull = "Currency Should Not Be Null";
        public const string CurrencyNameShouldNotBeNull = "Currency Name Should Not Be Null";
        PolicyPremium(decimal premium, Currency currency)
        {
            AddInvalidReason(premium < 0, PremiumShouldBeMoreThan0);
            AddInvalidReason(currency == null, CurrencyShouldNotBeNull);

            AddInvalidReason(currency?.CurrencyName == null, CurrencyNameShouldNotBeNull);

            Premium = premium;
            Currency = currency;
        }

        public static PolicyPremium Create(decimal premium, Currency currency) => new PolicyPremium(premium, currency);

        public decimal Premium { get; }
        public Currency Currency { get; }


        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Currency?.CurrencyName?.ToUpper();
            yield return Premium;
        }

        public override string ToString() => $"{Premium}[{Currency.CurrencyName}]";
    }
}