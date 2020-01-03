using System;
using CSharpFunctionalExtensions;

namespace DAL.BusinessObjects
{
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

    public class DoWrapper
    {
        public static DoWrapper<T2> Create<T2>(Func<T2> funcT) => new DoWrapper<T2>(funcT);
    }
}