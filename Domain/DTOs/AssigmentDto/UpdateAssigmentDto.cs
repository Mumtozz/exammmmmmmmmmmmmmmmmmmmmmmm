namespace Domain.DTOs;

public class UpdateAssigmentDto
{
    
    public int Id { get; set; }
    public string Description { get; set; } = null!;
    public string Instructor { get; set; } = null!;
    public DateTime DueDate { get; set; }
    public int CourseId { get; set; }
}
