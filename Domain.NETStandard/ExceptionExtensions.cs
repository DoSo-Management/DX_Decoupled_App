using System;
using System.Linq;
using System.Linq.Expressions;

namespace Domain.NETStandard
{
    public static class ExceptionExtensions
    {
        public static ValidationResults FailWith(this Expression<Func<bool>> checkFunc, string errorIfNot) => new ValidationResults(new ValidationResult(checkFunc, errorIfNot));
        public static ValidationResults And(this ValidationResults results, Expression<Func<bool>> checkF, string errorIfNot) => results.Add(new ValidationResult(checkF, errorIfNot));
        //public static void Finally(this ValidationResults results, bool check, string errorIfNot)
        //{
        //    results.Add(new ValidationResult(check, errorIfNot));

        //    if (results.Results.Any(r => r.IsInvalid)) throw new DomainLogicValidationException(results);
        //}
    }

    public static class Check
    {
        public static void That(ValidationResults results)
        {
            //results.Add(new ValidationResult(check, errorIfNot));

            if (results.Results.Any(r => r.IsInvalid)) throw new DomainLogicValidationException(results);
        }
    }
}