namespace ContentPlatform.Dto_s
{
    public class TokensDto
    {
        public required string JwtToken { get; set; } = string.Empty;
        public required string RefreshToken { get; set; } = string.Empty;
    } 
}
