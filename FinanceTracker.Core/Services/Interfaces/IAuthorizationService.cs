namespace FinanceTracker.Core.Services.Interfaces
{
    public interface IAuthorizationService
    {
        Task<string?> TryRefreshToken(string accessToken, string refreshToken);
    }
}