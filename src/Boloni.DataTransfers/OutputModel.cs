using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boloni.DataTransfers
{
    public class OutputModel
    {
        internal OutputModel(IEnumerable<string> errors)
        {
            Errors = errors;
        }

        public OutputModel() { }

        public IEnumerable<string> Errors { get; private set; } = Array.Empty<string>();

        public bool Succeed => !Errors.Any();

        public static OutputModel Success() => new();
        public static OutputModel Failed(params string[] errors) => new(errors);
    }

    public class OutputModel<TResult> : OutputModel
    {
        internal OutputModel(TResult data)
        {
            Data = data;
        }

        public TResult Data { get; }

        public static OutputModel<TResult> Success(TResult data) => new OutputModel<TResult>(data);
    }
}
