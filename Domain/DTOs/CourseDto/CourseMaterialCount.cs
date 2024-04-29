using Domain.Entities;

namespace Domain.DTOs.CourseDto;

public class CourseMaterialCount
{
    public Course? Course { get; set; }
    public int Count { get; set; }
}
