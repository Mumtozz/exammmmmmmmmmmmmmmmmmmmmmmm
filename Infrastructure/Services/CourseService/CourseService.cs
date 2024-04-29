using System.Net;
using AutoMapper;
using Domain.DTOs.CourseDto;
using Domain.Entities;
using Domain.Filters;
using Domain.Responses;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.CourseService;

public class CourseService(DataContext context, IMapper mapper) : ICourseService
{
    public async Task<PagedResponse<List<GetCoursesDto>>> GetCourses(CourseFilter filter)
    {
        try
        {
            var courses = context.Courses.AsQueryable();

            if (!string.IsNullOrEmpty(filter.CourseName))
                courses = courses.Where(x => x.CourseName.ToLower().Contains(filter.CourseName.ToLower()));
            var response = await courses
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize).ToListAsync();
            var totalRecord = courses.Count();
            var mapped = mapper.Map<List<GetCoursesDto>>(response);
            return new PagedResponse<List<GetCoursesDto>>(mapped, filter.PageNumber, filter.PageSize, totalRecord);
        }
        catch (Exception e)
        {
            return new PagedResponse<List<GetCoursesDto>>(HttpStatusCode.InternalServerError, e.Message);
        }
    }



    public async Task<Response<GetCoursesDto>> GetCourseById(int id)
    {
        try
        {
            var course = await context.Courses.FirstOrDefaultAsync(x => x.Id == id);
            if (course == null) return new Response<GetCoursesDto>(HttpStatusCode.BadRequest, "Not found");
            var mapped = mapper.Map<GetCoursesDto>(course);
            return new Response<GetCoursesDto>(mapped);
        }
        catch (Exception e)
        {
            return new Response<GetCoursesDto>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<string>> AddCourse(AddCourseDto course)
    {
        try
        {
            var mapped = mapper.Map<Course>(course);
            await context.Courses.AddAsync(mapped);
            var save = await context.SaveChangesAsync();
            if (save > 0) return new Response<string>("Successfully");
            return new Response<string>(HttpStatusCode.BadRequest, "Failed");
        }
        catch (Exception e)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<string>> UpdateCourse(UpdateCourseDto course)
    {
        try
        {
            var mapped = mapper.Map<Course>(course);
            context.Courses.Update(mapped);
            var save = await context.SaveChangesAsync();
            if (save > 0) return new Response<string>("Successfully");
            return new Response<string>(HttpStatusCode.BadRequest, "Failed");
        }
        catch (Exception e)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<bool>> DeleteCourse(int id)
    {
        try
        {

            var course = await context.Courses.Where(x => x.Id == id).ExecuteDeleteAsync();
            if (course > 0) return new Response<bool>(true);
            return new Response<bool>(HttpStatusCode.BadRequest, "Not found");

        }
        catch (Exception e)
        {
            return new Response<bool>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    //3
    public async Task<PagedResponse<List<CourseMaterialCount>>> GetCourseWithMaterialsCount(PaginationFilter filter)
    {
        try
        {
            var course = (from c in context.Courses
                          select new CourseMaterialCount
                          {
                              Course = c,
                              Count = context.Materials.Count(),
                          });
            var response = await course
            .Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize).ToListAsync();
            var totalRecord = course.Count();

            var mapped = mapper.Map<List<CourseMaterialCount>>(response);
            return new PagedResponse<List<CourseMaterialCount>>(mapped, filter.PageNumber, filter.PageSize, totalRecord);
        }
        catch (Exception ex)
        {
            return new PagedResponse<List<CourseMaterialCount>>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }
    public async Task<PagedResponse<List<CourseWithMaterial>>> GetCourseAndMaterailAsync(CourseFilter filter)
    {
        try
        {
            var courses = await (from c in context.Courses
                                 join m in context.Materials on c.Id equals m.CourseId
                                 select new CourseWithMaterial
                                 {
                                     CourseName = c.CourseName,
                                     MaterialTitle = m.Title,

                                 }).ToListAsync();
            var response = courses
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize).ToList();
            var totalRecord = courses.Count();
            return new PagedResponse<List<CourseWithMaterial>>(response, filter.PageNumber, filter.PageSize, totalRecord);
        }
        catch (Exception e)
        {

            return new PagedResponse<List<CourseWithMaterial>>(HttpStatusCode.InternalServerError, e.Message);
        }
    }
}