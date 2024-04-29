namespace Domain.DTOs;

public class AddAssigmentDto
{
    
    public string Description { get; set; } = null!;
    public string Instructor { get; set; } = null!;
    public DateTime DueDate { get; set; }
    public int CourseId { get; set; }
}
