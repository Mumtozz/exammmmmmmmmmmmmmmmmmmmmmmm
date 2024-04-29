namespace Domain.DTOs.FeedbackDto;

public class AddFeedbackDto
{
    
    public string Text { get; set; } = null!;
    public DateTime FeedbackDate { get; set; }
    public int StudentId { get; set; }
    public int AssignmentId { get; set; }
}
