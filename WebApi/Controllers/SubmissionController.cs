using Domain.DTOs.SubmissionDto;
using Domain.Filters;
using Domain.Responses;
using Infrastructure.Services.SubmissionService;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[Route("[controller]")]
[ApiController]
public class SubmissionController(ISubmissionService service)
{
    [HttpGet("get-Submissions")]
    public async Task<Response<List<GetSubmissionDto>>> GetSubmissionsAsync([FromQuery]SubmissionFilter filter)
    {
        return await service.GetSubmissionsAsync(filter);
    }

    [HttpGet("{submissionId:int}")]
    public async Task<Response<GetSubmissionDto>> GetCourseByIdAsync(int submissionId)
    {
        return await service.GetSubmissionByIdAsync(submissionId);
    }

    [HttpPost("add-course")]
    public async Task<Response<string>> AddCourseAsync(AddSubmissionDto addSubmissionDto)
    {
        return await service.CreateSubmissionAsync(addSubmissionDto);
    }

    [HttpPut("update-Submission")]
    public async Task<Response<string>> UpdateCourseAsync(UpdateSubmissionDto  updateSubmissionDto)
    {
        return await service.UpdateSubmissionAsync(updateSubmissionDto);
    }

    [HttpDelete("{SubmissionId:int}")]
    public async Task<Response<bool>> DeleteCourseAsync(int submissionId)
    {
        return await service.DeleteSubmissionAsync(submissionId);
    }

}
