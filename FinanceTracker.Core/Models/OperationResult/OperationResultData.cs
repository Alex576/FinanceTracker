namespace FinanceTracker.Core.Models.OperationResult
{
    public class OperationResultData<T> : OperationResult where T : class
    {
        public T? Result { get; }

        public OperationResultData(T? result, ResultCode code) : base(code)
        {
            Result = result;
        }

        public OperationResultData(T? result, ResultCode code, string description) : base(code, description)
        {
            Result = result;
        }

        public OperationResultData(OperationResult operationResult, T result) : this(result, operationResult.Code, operationResult.Description) { }
    }
}
