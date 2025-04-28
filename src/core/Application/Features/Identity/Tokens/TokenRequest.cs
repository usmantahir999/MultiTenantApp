namespace Application.Features.Identity.Tokens
{
    public class TokenRequest
    {
        public string Username { get; set; } = default!;
        public string Password { get; set; } = default!;
    }
}
