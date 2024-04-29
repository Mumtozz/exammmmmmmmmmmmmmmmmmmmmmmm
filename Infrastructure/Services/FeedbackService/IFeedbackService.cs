using Domain.DTOs.FeedbackDto;
using Domain.Filters;
using Domain.Responses;

namespace Infrastructure.Services.FeedbackService;

public interface IFeedbackService
{
    
    Task<PagedResponse<List<GetFeedbackDto>>> GetFeedbacksAsync(FeedbackFilter filter);
    Task<Response<GetFeedbackDto>> GetFeedbackByIdAsync(int id);
    Task<Response<string>> CreateFeedbackAsync(AddFeedbackDto feedbackDto);
    Task<Response<string>> UpdateFeedbackAsync(UpdateFeedbackDto feedbackDto);
    Task<Response<bool>> DeleteFeedbackAsync(int id);
    Task<PagedResponse<List<GetFeedbackForAssignmentDto>>> GetFeedBacksForAssignment(FeedbackFilter filter);
}
