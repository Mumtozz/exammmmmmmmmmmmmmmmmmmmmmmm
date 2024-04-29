namespace Domain.DTOs.SubmissionDto;

public class AddSubmissionDto
{
    
    public DateTime SubmissionDate { get; set; }
    public string Content { get; set; } = null!;
    public int StudentId { get; set; }
    public int AssignmentId { get; set; }
}
