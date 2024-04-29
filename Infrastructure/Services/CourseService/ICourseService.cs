using Domain.DTOs.CourseDto;
using Domain.Filters;
using Domain.Responses;

namespace Infrastructure.Services.CourseService;

public interface ICourseService
{
    Task<PagedResponse<List<GetCoursesDto>>> GetCourses(CourseFilter filter);
    Task<Response<GetCoursesDto>> GetCourseById(int id);
    Task<Response<string>> AddCourse(AddCourseDto course);
    Task<Response<string>> UpdateCourse(UpdateCourseDto course);
    Task<Response<bool>> DeleteCourse(int id);
    Task<PagedResponse<List<CourseMaterialCount>>> GetCourseWithMaterialsCount(PaginationFilter filter);
    Task<PagedResponse<List<CourseWithMaterial>>> GetCourseAndMaterailAsync(CourseFilter filter);
}