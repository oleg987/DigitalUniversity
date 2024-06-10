using System.ComponentModel.DataAnnotations;

namespace AuthService.Requests;

public record RefreshTokenRequest([Required]string RefreshToken);