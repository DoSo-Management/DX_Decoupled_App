using System.Collections.Generic;

namespace Domain.NETStandard
{
    public class ValidationResults
    {
        public ValidationResults(ValidationResult result) { _results.Add(result); }
        public ValidationResults Add(ValidationResult result)
        {
            _results.Add(result);

            return this;
        }

        readonly List<ValidationResult> _results = new List<ValidationResult>();
        public IReadOnlyList<ValidationResult> Results => _results.AsReadOnly();

    }
}