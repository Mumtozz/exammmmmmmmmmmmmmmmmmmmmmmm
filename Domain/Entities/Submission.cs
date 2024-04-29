namespace Domain.Entities;

public class Submission
{
    
    public int Id { get; set; }
    public DateTime SubmissionDate { get; set; }
    public string? Content { get; set; }
    public int StudentId { get; set; }
    public int AssignmentId { get; set; }
    public Student? Student { get; set; }
    public Assignment? Assignment { get; set; }
}
