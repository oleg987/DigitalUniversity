using System.ComponentModel.DataAnnotations;

namespace UserService.Requests;

public record CreateSubjectRequest([Required] string Title)
{
    public Guid Id { get; } = Guid.NewGuid();
}