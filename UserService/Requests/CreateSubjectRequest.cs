using System.ComponentModel.DataAnnotations;

namespace UserService.Requests;

public record CreateSubjectRequest([Required] Guid ProfessorId, [Required] string Title)
{
    public Guid Id { get; } = Guid.NewGuid();
}