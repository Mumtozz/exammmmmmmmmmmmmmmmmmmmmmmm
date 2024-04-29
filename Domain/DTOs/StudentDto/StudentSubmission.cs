using Domain.Entities;

namespace Domain.DTOs.StudentDto;

public class StudentSubmission
{
    public Student? Student { get; set; }
    public int Count { get; set; }
}


