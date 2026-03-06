namespace FinanceTracker.Core.Models
{
    public class JWTModel
    {
        public string? Issuer { get; set; }
        public string? Audience { get; set; }
        public string SecretKey { get; set; } = "";
        public string SecretRefreshKey { get; set; } = "";
        public int AccessTokenExpireInMinutes { get; set; } = 15;
        public int RefreshTokenExpireInDays { get; set; } = 7;
    }
}
