using Domain.DTOs.SubmissionDto;
using Domain.Filters;
using Domain.Responses;

namespace Infrastructure.Services.SubmissionService;

public interface ISubmissionService
{
    
    Task<PagedResponse<List<GetSubmissionDto>>> GetSubmissionsAsync(SubmissionFilter filter);
    Task<Response<GetSubmissionDto>> GetSubmissionByIdAsync(int id);
    Task<Response<string>> CreateSubmissionAsync(AddSubmissionDto submission);
    Task<Response<string>> UpdateSubmissionAsync(UpdateSubmissionDto submission);
    Task<Response<bool>> DeleteSubmissionAsync(int id);
}
