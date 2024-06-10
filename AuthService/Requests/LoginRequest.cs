using System.ComponentModel.DataAnnotations;

namespace AuthService.Requests;

public record LoginRequest([Required] string Email, [Required] string Password);