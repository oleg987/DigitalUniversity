using System.ComponentModel.DataAnnotations;
using AuthService.Entities;

namespace AuthService.Requests;

public record CreateUserRequest([Required]string Name, [Required]string Email, [Required]UserRole Role)
{
    public Guid Id { get; } = Guid.NewGuid();
}