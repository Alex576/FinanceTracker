namespace FinanceTracker.Core.Models.OperationResult
{
    public class OperationResult
    {
        public ResultCode Code { get; }
        public string? Description { get; }

        public OperationResult(ResultCode code)
        {
            Code = code;
        }

        public OperationResult(ResultCode code, string description) : this(code)
        {
            Description = description;
        }
    }
}
