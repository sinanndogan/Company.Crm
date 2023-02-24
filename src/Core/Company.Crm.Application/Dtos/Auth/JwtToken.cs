namespace Company.Crm.Application.Dtos.Auth;

public class JwtToken
{
    public JwtToken(string accessToken, string refreshToken, DateTime expireAt)
    {
        AccessToken = accessToken;
        RefreshToken = refreshToken;
        ExpireAt = expireAt;
    }

    public string? AccessToken { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? ExpireAt { get; set; }
}