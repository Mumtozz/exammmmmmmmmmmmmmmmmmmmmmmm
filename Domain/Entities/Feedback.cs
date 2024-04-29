namespace Domain.Entities;

public class Feedback
{
    
    public int Id { get; set; }
    public string? Text { get; set; } 
    public DateTime FeedbackDate { get; set; }
    public int StudentId { get; set; }
    public int AssignmentId { get; set; }

    public Student? Student { get; set; }
    public Assignment? Assignment { get; set; }
}
