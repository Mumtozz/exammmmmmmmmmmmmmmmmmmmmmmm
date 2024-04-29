using Domain.DTOs.FeedbackDto;
using Domain.Filters;
using Domain.Responses;
using Infrastructure.Services.FeedbackService;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

public class FeedbackController(IFeedbackService service)
{
    [HttpGet("get-Feedbacks")]
    public async Task<Response<List<GetFeedbackDto>>> GetFeedbacksAsync([FromQuery] FeedbackFilter filter)
    {
        return await service.GetFeedbacksAsync(filter);
    }
    [HttpGet("{feedbackId:int}")]
    public async Task<Response<GetFeedbackDto>> GetFeedBackByIdAsync(int feedbackId)
    {
        return await service.GetFeedbackByIdAsync(feedbackId);
    }

    [HttpPost("create-FeedBack")]
    public async Task<Response<string>> AddFeedBackAsync([FromBody] AddFeedbackDto addFeedbackDto)
    {
        return await service.CreateFeedbackAsync(addFeedbackDto);
    }

    [HttpPut("update-FeedBack")]
    public async Task<Response<string>> UpdateFeedBackAsync([FromBody] UpdateFeedbackDto updateFeedbackDto)
    {
        return await service.UpdateFeedbackAsync(updateFeedbackDto);
    }

    [HttpDelete("{feedbackId:int}")]
    public async Task<Response<bool>> DeleteFeedBackAsync(int feedbackId)
    {
        return await service.DeleteFeedbackAsync(feedbackId);
    }
    [HttpGet("Getttttttttttttttt")]
    public async Task<PagedResponse<List<GetFeedbackForAssignmentDto>>> GetFeedBacksForAssignment(FeedbackFilter filter)
    {
        return await service.GetFeedBacksForAssignment(filter);
    }
}
