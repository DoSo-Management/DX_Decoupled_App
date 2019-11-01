using System;
using System.Collections.Generic;
using CSharpFunctionalExtensions;

namespace Domain.NETStandard
{
    public class CurrencyV : ValueObject
    {
        public CurrencyV(string currencyName)
        {
            CurrencyName = currencyName ?? throw new ArgumentNullException(nameof(currencyName));
        }

        public string CurrencyName { get; }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return CurrencyName;
        }

        public override string ToString() => CurrencyName;
    }
}