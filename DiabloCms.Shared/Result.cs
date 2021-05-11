using System;
using System.Collections.Generic;
using System.Linq;

namespace DiabloCms.Shared
{
    public class Result
    {
        private readonly List<string> _errors;

        internal Result(bool succeeded, List<string> errors)
        {
            Succeeded = succeeded;
            _errors = errors;
        }

        public bool Succeeded { get; }

        public List<string> Errors
            => Succeeded
                ? new List<string>()
                : _errors;

        public static Result Success
            => new(true, new List<string>());

        public static Result Failure(IEnumerable<string> errors)
        {
            return new(false, errors.ToList());
        }

        public static implicit operator Result(string error)
        {
            return Failure(new List<string> {error});
        }

        public static implicit operator Result(List<string> errors)
        {
            return Failure(errors.ToList());
        }

        public static implicit operator Result(bool success)
        {
            return success ? Success : Failure(new[] {"Unsuccessful operation."});
        }

        public static implicit operator bool(Result result)
        {
            return result.Succeeded;
        }
    }

    public class Result<TData> : Result
    {
        private readonly TData _data;

        private Result(bool succeeded, TData data, List<string> errors)
            : base(succeeded, errors)
        {
            _data = data;
        }

        public TData Data
            => Succeeded
                ? _data
                : throw new InvalidOperationException(
                    $"{nameof(Data)} is not available with a failed result. Use {Errors} instead.");

        public static Result<TData> SuccessWith(TData data)
        {
            return new(true, data, new List<string>());
        }

        public new static Result<TData> Failure(IEnumerable<string> errors)
        {
            return new(false, default, errors.ToList());
        }

        public static Result<TData> FailureParams(params string[] errors)
        {
            return Failure(errors);
        }

        public static implicit operator Result<TData>(string error)
        {
            return Failure(new List<string> {error});
        }

        public static implicit operator Result<TData>(List<string> errors)
        {
            return Failure(errors);
        }

        public static implicit operator Result<TData>(TData data)
        {
            return SuccessWith(data);
        }

        public static implicit operator bool(Result<TData> result)
        {
            return result.Succeeded;
        }
    }
}