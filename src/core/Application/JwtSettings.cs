namespace Application
{
    public class JwtSettings
    {
        public string Secret { get; set; } = default!;
        public int TokenExpiryInMinutes { get; set; }
        public int RefreshTokenExpiryTimeInDays { get; set; }
    }
}
