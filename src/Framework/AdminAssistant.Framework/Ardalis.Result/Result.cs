#pragma warning disable CA1721 // Property names should not match get methods
#pragma warning disable CA1000 // Do not declare static members on generic types
#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
#pragma warning disable CS8603 // Possible null reference return.
#pragma warning disable CA1724 // Type names should not match namespaces.
//----------------------------------------------------------------------------------------------------------------------
//     Author:  Steve Smith (https://github.com/ardalis)
//     GitHub:  https://github.com/ardalis/Result/blob/master/src/Ardalis.Result/Result.cs
//     License: MIT
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Ardalis.Result
{
    public partial class Result<T> : IResult
    {
        public Result(T value)
        {
            Value = value;
        }
        private Result(ResultStatus status)
        {
            Status = status;
        }

        public T Value { get; }
        public Type ValueType
        {
            get
            {
                return Value.GetType();
            }
        }
        public ResultStatus Status { get; } = ResultStatus.Ok;
        public IEnumerable<string> Errors { get; private set; } = new List<string>();
        public Dictionary<string, string> ValidationErrors { get; private set; } = new Dictionary<string, string>();

        public object GetValue()
        {
            return this.Value;
        }

        public static Result<T> Success(T value)
        {
            return new Result<T>(value);
        }

        public static Result<T> Error(params string[] errorMessages)
        {
            return new Result<T>(ResultStatus.Error) { Errors = errorMessages };
        }

        public static Result<T> Invalid(Dictionary<string, string> validationErrors)
        {
            return new Result<T>(ResultStatus.Invalid) { ValidationErrors = validationErrors };
        }

        public static Result<T> NotFound()
        {
            return new Result<T>(ResultStatus.NotFound);
        }

        public static Result<T> Forbidden()
        {
            return new Result<T>(ResultStatus.Forbidden);
        }
    }
}
#pragma warning restore CA1724 // Type names should not match namespaces.
#pragma warning restore CS8603 // Possible null reference return.
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning restore CA1000 // Do not declare static members on generic types
#pragma warning restore CA1721 // Property names should not match get methods
