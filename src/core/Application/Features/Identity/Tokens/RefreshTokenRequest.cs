namespace Application.Features.Identity.Tokens
{
    public class RefreshTokenRequest
    {
        public string CurrentJwt { get; set; } = default!;
        public string CurrentRefreshToken { get; set; } = default!;
        public DateTime RefreshTokenExpiryDate { get; set; }
    }
}
