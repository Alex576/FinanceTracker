namespace FinanceTracker.Core.Models.OperationResult
{
    public enum ResultCode
    {
        Success = 1,
        Error = 2,
        Warning = 3,
    }

    public static class ResultCodeExtensions
    {
        public static bool IsSuccess(this ResultCode code) => code == ResultCode.Success;
        public static bool IsError(this ResultCode code) => code == ResultCode.Error;
    }
}
