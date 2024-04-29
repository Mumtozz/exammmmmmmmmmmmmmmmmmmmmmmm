namespace Domain.DTOs.SubmissionDto;

public class UpdateSubmissionDto
{
    
    public int Id { get; set; }
    public DateTime SubmissionDate { get; set; }
    public string Content { get; set; } = null!;
    public int StudentId { get; set; }
    public int AssignmentId { get; set; }
}
