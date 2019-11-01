using System;
using System.Linq;

namespace Domain.NETStandard
{
    public class DomainLogicValidationException : InvalidOperationException
    {
        public DomainLogicValidationException(ValidationResults results) : base(string.Join("\r\n", results.Results))
        {

        }
    }
}
