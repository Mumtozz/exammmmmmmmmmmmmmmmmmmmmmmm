namespace Domain.DTOs.StudentDto;

public class AddStudentDto
{
    
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public DateTime EnrollmentDate { get; set; }
}
