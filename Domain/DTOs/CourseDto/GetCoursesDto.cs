namespace Domain.DTOs.CourseDto;

public class GetCoursesDto
{
    
    public int Id { get; set; }
    public string CourseName { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Instructor { get; set; } = null!;
    public int Credits { get; set; }
}
