using Domain.DTOs;
using Domain.Filters;
using Domain.Responses;

namespace Infrastructure.Services.AssignmentService;

public interface IAssignmentService
{
    
    Task<PagedResponse<List<GetAssigmentDto>>> GetAssignmentsAsync(AssigmentFilter filter);
    Task<Response<GetAssigmentDto>> GetAssignmentByIdAsync(int id);
    Task<Response<string>> CreateAssignmentAsync(AddAssigmentDto assigmentDto);
    Task<Response<string>> UpdateAssignmentAsync(UpdateAssigmentDto assigmentDto);
    Task<Response<bool>> DeleteAssignmentAsync(int id);
}
