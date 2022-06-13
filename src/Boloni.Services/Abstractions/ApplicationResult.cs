using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boloni.Services.Abstractions
{
    [Serializable]
    public class ApplicationResult
    {
        public ApplicationResult(IEnumerable<string> errors)
        {
            Errors = errors;
        }

        public ApplicationResult() { }

        public IEnumerable<string> Errors { get;private set; } = Array.Empty<string>();

        public bool Succeed => !Errors.Any();

        public static ApplicationResult Success() => new();
        public static ApplicationResult Failed(params string[] errors) => new(errors);

    }

    [Serializable]
    public class ApplicationResult<TResult> : ApplicationResult
    {
        internal ApplicationResult(TResult data)
        {
            Data = data;
        }

        public TResult Data { get; }

        public static ApplicationResult<TResult> Success(TResult data) => new(data);
    }
}
