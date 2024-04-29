namespace Domain.DTOs.FeedbackDto;

public class UpdateFeedbackDto
{
    
    public int Id { get; set; }
    public string Text { get; set; } = null!;
    public DateTime FeedbackDate { get; set; }
    public int StudentId { get; set; }
    public int AssignmentId { get; set; }
}
