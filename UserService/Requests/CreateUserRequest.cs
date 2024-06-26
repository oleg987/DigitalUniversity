﻿using System.ComponentModel.DataAnnotations;
using Domain.Entities;

namespace UserService.Requests;

public record CreateUserRequest([Required] string Name, [Required] string Email, [Required] UserRole Role)
{
    public Guid Id { get; } = Guid.NewGuid();
}