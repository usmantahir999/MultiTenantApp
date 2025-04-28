namespace Application.Features.Identity.Tokens
{
    public class TokenResponse
    {

        public string Jwt { get; set; } = default!;
        public string RefreshToken { get; set; } = default!;
        public DateTime RefreshTokenExpiryDate { get; set; }
    }
}
