using System;
using System.Collections.Generic;
using System.Linq;
using CSharpFunctionalExtensions;
using DAL.BusinessObjects;

namespace DAL.ValueObjects
{
    public class PolicyPremium : ValueObject<PolicyPremium>
    {
        public const string PremiumShouldBeMoreThan0 = "Premium Should Be More Than 0";
        public const string CurrencyShouldNotBeNull = "Currency Should Not Be Null";
        public const string CurrencyNameShouldNotBeNull = "Currency Name Should Not Be Null";
        PolicyPremium(decimal premium, Currency currency)
        {
            Premium = premium;
            Currency = currency;
        }

        public static Result<PolicyPremium> Create(decimal premium, Currency currency)
        {
            var errorsList = new List<string>();
            if (premium < 0)
                errorsList.Add(PremiumShouldBeMoreThan0);
            if (currency == null)
                errorsList.Add(CurrencyShouldNotBeNull);
            if (currency?.CurrencyName == null)
                errorsList.Add(CurrencyNameShouldNotBeNull);

            return errorsList.Any() ? Result.Fail<PolicyPremium>(errorsList.ToString()) : Result.Ok(new PolicyPremium(premium, currency));
        }

        public decimal Premium { get; }
        public Currency Currency { get; }
        protected override bool EqualsCore(PolicyPremium other) => Premium == other.Premium;

        protected override int GetHashCodeCore() => GetHashCode();

        
    }
}