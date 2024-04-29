using Domain.DTOs;
using Domain.Filters;
using Domain.Responses;
using Infrastructure.Services.AssignmentService;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[Route("[controller]")]
[ApiController]
public class AssignmentController(IAssignmentService service)
{
    [HttpGet("get-Assignment")]
    public async Task<Response<List<GetAssigmentDto>>> GetAssignmentAsync ([FromQuery]AssigmentFilter filter)
    {
        return await service.GetAssignmentsAsync(filter);
    }
    [HttpGet("{assignmentId:int}")]
    public async Task<Response<GetAssigmentDto>> GetAssignmentByIdAsync(int assignmentId)
    {
        return await service.GetAssignmentByIdAsync(assignmentId);
    }
    
    [HttpPost("create-Assignment")]
    public async Task<Response<string>> AddAssignmentAsync(AddAssigmentDto assignmentDto)
    {
        return await service.CreateAssignmentAsync(assignmentDto);
    }
    
    [HttpPut("update-Assignment")]
    public async Task<Response<string>> UpdateAssignmentAsync(UpdateAssigmentDto assignmentDto)
    {
        return await service.UpdateAssignmentAsync(assignmentDto);
    }
    
    [HttpDelete("{AssignmentId:int}")]
    public async Task<Response<bool>> DeleteAssignmentAsync(int AssignmentId)
    {
        return await service.DeleteAssignmentAsync(AssignmentId);
    }
    
}
