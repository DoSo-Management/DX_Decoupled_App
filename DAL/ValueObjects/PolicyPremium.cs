using System;
using DAL.BusinessObjects;

namespace DAL.ValueObjects
{
    public class PolicyPremium : ValueObject<PolicyPremium>
    {
        public const string PremiumShouldBeMoreThan0 = "Premium Should Be More Than 0";
        public const string CurrencyShouldNotBeNull = "Currency Should Not Be Null";
        public const string CurrencyNameShouldNotBeNull = "Currency Name Should Not Be Null";
        public PolicyPremium(decimal premium, Currency currency)
        {
            if (premium < 0)
                throw new InvalidOperationException(PremiumShouldBeMoreThan0);
            if(currency == null)
                throw new InvalidOperationException(CurrencyShouldNotBeNull);
            if(currency.CurrencyName == null)
                throw new InvalidOperationException(CurrencyNameShouldNotBeNull);

            Premium = premium;
            Currency = currency;
        }

        public decimal Premium { get; }
        public Currency Currency { get; }
        protected override bool EqualsCore(PolicyPremium other) => Premium == other.Premium;

        protected override int GetHashCodeCore() => GetHashCode();
    }
}