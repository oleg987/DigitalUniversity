using System.ComponentModel.DataAnnotations;

namespace UserService.Requests;

public record SelectSubjectRequest([Required]Guid StudentId, [Required]Guid SubjectId);