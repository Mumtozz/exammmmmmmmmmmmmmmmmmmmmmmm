using System.Data.Common;
using System.Net;
using AutoMapper;
using Domain.DTOs.StudentDto;
using Domain.Entities;
using Domain.Filters;
using Domain.Responses;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.StudentService;

public class StudentService(DataContext context, IMapper mapper) : IStudentService
{
    #region GetStudentsAsync



    //1
    public async Task<PagedResponse<List<StudentSubmission>>> GetStudentCountSubmission(PaginationFilter filter)
    {
        try
        {
            var students = (from s in context.Students
                            select new StudentSubmission
                            {
                                Student = s,
                                Count = context.Submissions.Count(),
                            }).OrderByDescending(e => e.Count);
            var response = await students
            .Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize).ToListAsync();
            var totalRecord = students.Count();

            var mapped = mapper.Map<List<StudentSubmission>>(response);
            return new PagedResponse<List<StudentSubmission>>(mapped, filter.PageNumber, filter.PageSize, totalRecord);
        }
        catch (Exception ex)
        {
            return new PagedResponse<List<StudentSubmission>>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }





    //4
    public async Task<PagedResponse<List<GetStudentDto>>> GetStudentByDate(PaginationFilter filter)
    {
        try
        {
            var students = (from s in context.Students
                            join sub in context.Submissions on s.Id equals sub.StudentId
                            join a in context.Assignments on sub.AssignmentId equals a.Id
                            join c in context.Courses on a.CourseId equals c.Id
                            select new
                            {
                                Student = s,
                                Submission = sub,
                                Assignment = a,
                            }).Where(x => x.Submission.SubmissionDate < x.Assignment.DueDate).Select(x => x.Student).AsQueryable();

            var response = await students
            .Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize).ToListAsync();
            var totalRecord = students.Count();

            var mapped = mapper.Map<List<GetStudentDto>>(response);
            return new PagedResponse<List<GetStudentDto>>(mapped, filter.PageNumber, filter.PageSize, totalRecord);
        }
        catch (Exception ex)
        {
            return new PagedResponse<List<GetStudentDto>>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }


    //2
    public async Task<PagedResponse<List<StudentSubmission>>> GetStudentsSubmission(PaginationFilter filter)
    {
        try
        {
            var students = (from s in context.Students
                            select new StudentSubmission
                            {
                                Student = s,
                                Count = context.Submissions.Where(x => x.StudentId == s.Id).Count(),
                            }).Where(x => x.Count == 0).AsQueryable();
            var response = await students
            .Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize).ToListAsync();
            var totalRecord = students.Count();

            var mapped = mapper.Map<List<StudentSubmission>>(response);
            return new PagedResponse<List<StudentSubmission>>(mapped, filter.PageNumber, filter.PageSize, totalRecord);
        }
        catch (Exception ex)
        {
            return new PagedResponse<List<StudentSubmission>>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }
    public async Task<PagedResponse<List<GetStudentDto>>> GetStudentsAsync(StudentFilter filter)
    {
        try
        {
            var students = context.Students.AsQueryable();

            if (!string.IsNullOrEmpty(filter.Email))
                students = students.Where(x => x.Email.ToLower().Contains(filter.Email.ToLower()));

            var response = await students
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize).ToListAsync();
            var totalRecord = students.Count();

            var mapped = mapper.Map<List<GetStudentDto>>(response);
            return new PagedResponse<List<GetStudentDto>>(mapped, filter.PageNumber, filter.PageSize, totalRecord);

        }
        catch (Exception e)
        {
            return new PagedResponse<List<GetStudentDto>>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    #endregion

    #region GetStudentByIdAsync

    public async Task<Response<GetStudentDto>> GetStudentByIdAsync(int id)
    {
        try
        {
            var student = await context.Students.FirstOrDefaultAsync(x => x.Id == id);
            if (student == null)
                return new Response<GetStudentDto>(HttpStatusCode.BadRequest, "Student not found");
            var mapped = mapper.Map<GetStudentDto>(student);
            return new Response<GetStudentDto>(mapped);
        }
        catch (Exception e)
        {
            return new Response<GetStudentDto>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    #endregion

    #region CreateStudentAsync

    public async Task<Response<string>> CreateStudentAsync(AddStudentDto student)
    {
        try
        {
            var existingStudent = await context.Students.FirstOrDefaultAsync(x => x.Email == student.Email);
            if (existingStudent != null)
                return new Response<string>(HttpStatusCode.BadRequest, "Student already exists");
            var mapped = mapper.Map<Student>(student);

            await context.Students.AddAsync(mapped);
            await context.SaveChangesAsync();

            return new Response<string>("Successfully created a new student");
        }
        catch (Exception e)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    #endregion

    #region UpdateStudentAsync

    public async Task<Response<string>> UpdateStudentAsync(UpdateStudentDto student)
    {
        try
        {
            var mappedStudent = mapper.Map<Student>(student);
            context.Students.Update(mappedStudent);
            var update = await context.SaveChangesAsync();
            if (update == 0) return new Response<string>(HttpStatusCode.BadRequest, "Student not found");
            return new Response<string>("Student updated successfully");
        }
        catch (Exception e)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
        }
    }


    #endregion

    public async Task<Response<bool>> DeleteStudentAsync(int id)
    {
        try
        {
            var student = await context.Students.Where(x => x.Id == id).ExecuteDeleteAsync();
            if (student == 0)
                return new Response<bool>(HttpStatusCode.BadRequest, "Student not found");

            return new Response<bool>(true);
        }
        catch (Exception e)
        {
            return new Response<bool>(HttpStatusCode.InternalServerError, e.Message);
        }
    }


}