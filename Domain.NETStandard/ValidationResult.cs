using System;
using System.Linq.Expressions;

namespace Domain.NETStandard
{
    //[DebuggerDisplay("{" + nameof(Method) + "}, Message")]
    public class ValidationResult
    {
        public ValidationResult(Expression<Func<bool>> isInvalidIf, string message)
        {
            Method = isInvalidIf.ToString();

            var func = isInvalidIf.Compile();

            IsValid = !func();
            Message = message;
        }

        public bool IsValid { get; }
        public bool IsInvalid => !IsValid;
        public string Message { get; }
        //MethodInfo MethodInfo { get; }
        public string Method { get; }

        public override string ToString() => $"{(IsValid ? "Y" : "N") }[{Message}-[{Method}]]";
    }
}