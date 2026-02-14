using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceTracker.Core.Models.OperationResult
{
    public class OperationResult<T> where T : class
    {
        public T? Result { get; }
        public ResultCode Code { get; }
        public string? Description { get; }

        public OperationResult(T? result, ResultCode code)
        {
            Result = result;
            Code = code;
        }

        public OperationResult(T? result, ResultCode code, string description) : this(result, code)
        {
            Description = description;
        }
    }
}
