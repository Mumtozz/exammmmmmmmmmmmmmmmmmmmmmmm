namespace Domain.DTOs.CourseDto;

public class AddCourseDto
{
    
    public string CourseName { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Instructor { get; set; } = null!;
    public int Credits { get; set; }
}
