namespace Common.Auth;

public record JwtTokenResponse(string AccessToken, string RefreshToken, long RefreshTokenExpires);