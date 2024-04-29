using System.Net;
using Domain.DTOs.StudentDto;
using Domain.Filters;
using Domain.Responses;
using Infrastructure.Services.StudentService;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[Route("[controller]")]
[ApiController]
public class StudentController(IStudentService studentService) : ControllerBase
{
    [HttpGet("get-students")]
    public async Task<Response<List<GetStudentDto>>> GetStudentsAsync([FromQuery] StudentFilter filter)
    {
        return await studentService.GetStudentsAsync(filter);
    }
    [HttpGet("{studentId:int}")]
    public async Task<Response<GetStudentDto>> GetStudentByIdAsync(int studentId)
    {
        return await studentService.GetStudentByIdAsync(studentId);
    }

    [HttpPost("create-student")]
    public async Task<Response<string>> AddStudentAsync(AddStudentDto studentDto)
    {
        return await studentService.CreateStudentAsync(studentDto);
    }

    [HttpPut("update-student")]
    public async Task<Response<string>> UpdateStudentAsync(UpdateStudentDto studentDto)
    {
        return await studentService.UpdateStudentAsync(studentDto);
    }

    [HttpDelete("{studentId:int}")]
    public async Task<Response<bool>> DeleteStudentAsync(int studentId)
    {
        return await studentService.DeleteStudentAsync(studentId);
    }
    [HttpGet("Get-Student-By-Date")]
    public async Task<PagedResponse<List<GetStudentDto>>> GetStudentByDate(PaginationFilter filter)
    {
        return await studentService.GetStudentByDate(filter);
    }
    [HttpGet("GetStudent-Submission")]
    public async Task<PagedResponse<List<StudentSubmission>>> GetStudentsSubmission(PaginationFilter filter)
    {
        return await studentService.GetStudentsSubmission(filter);
    }
    [HttpGet("Get-Student-Count-Submission")]
    public async Task<PagedResponse<List<StudentSubmission>>> GetStudentCountSubmission(PaginationFilter filter)
    {
        return await studentService.GetStudentCountSubmission(filter);
    }



}