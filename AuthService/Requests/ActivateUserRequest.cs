using System.ComponentModel.DataAnnotations;

namespace AuthService.Requests;

public record ActivateUserRequest([Required] string InviteCode, [Required] string Password);
