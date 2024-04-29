using Domain.DTOs.CourseDto;
using Domain.Filters;
using Domain.Responses;
using Infrastructure.Services.CourseService;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("[controller]")]
[ApiController]

public class CourseController(ICourseService courseService) : ControllerBase
{
    [HttpGet("get-courses")]
    public async Task<Response<List<GetCoursesDto>>> GetCoursesAsync([FromQuery] CourseFilter filter)
    {
        return await courseService.GetCourses(filter);
    }

    [HttpGet("{courseId:int}")]
    public async Task<Response<GetCoursesDto>> GetCourseByIdAsync(int courseId)
    {
        return await courseService.GetCourseById(courseId);
    }

    [HttpPost("add-course")]
    public async Task<Response<string>> AddCourseAsync(AddCourseDto courseDto)
    {
        return await courseService.AddCourse(courseDto);
    }

    [HttpPut("update-course")]
    public async Task<Response<string>> UpdateCourseAsync(UpdateCourseDto courseDto)
    {
        return await courseService.UpdateCourse(courseDto);
    }

    [HttpDelete("{courseId:int}")]
    public async Task<Response<bool>> DeleteCourseAsync(int id)
    {
        return await courseService.DeleteCourse(id);
    }
    [HttpGet("Get-Student-and-Materials-Count")]
    public async Task<PagedResponse<List<CourseMaterialCount>>> GetCourseWithMaterialsCount(PaginationFilter filter)
    {
        return await courseService.GetCourseWithMaterialsCount(filter);
    }
    [HttpGet("GEtttttt")]
    public async Task<PagedResponse<List<CourseWithMaterial>>> GetCourseAndMaterailAsync(CourseFilter filter)
    {
        return await courseService.GetCourseAndMaterailAsync(filter);
    }
    
}