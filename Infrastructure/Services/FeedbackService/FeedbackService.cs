using AutoMapper;
using Domain.DTOs.FeedbackDto;
using Domain.Entities;
using Domain.Filters;
using Domain.Responses;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.FeedbackService;

public class FeedbackService(DataContext context,IMapper mapper) : IFeedbackService
{
    public async Task<Response<string>> CreateFeedbackAsync(AddFeedbackDto feedbackDto)
    {
        try
        {
            var existingFeed = await context.Feedbacks.FirstOrDefaultAsync(x => x.Text == feedbackDto.Text);
            if (existingFeed != null)
                return new Response<string>(System.Net.HttpStatusCode.BadRequest, "FeedBack already exists");
            var mapped = mapper.Map<Feedback>(feedbackDto);
            
            await context.Feedbacks.AddAsync(mapped);
            await context.SaveChangesAsync();

            return new Response<string>("Successfully created a new FeedBack");
        }
        catch (Exception e)
        {
            return new Response<string>(System.Net.HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<bool>> DeleteFeedbackAsync(int id)
    {
          try
        {
            var feed = await context.Feedbacks.Where(x => x.Id == id).ExecuteDeleteAsync();
            if (feed == 0)
                return new Response<bool>(System.Net.HttpStatusCode.BadRequest, "Feedback not found");

            return new Response<bool>(true);
        }
        catch (Exception e)
        {
            return new Response<bool>(System.Net.HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<GetFeedbackDto>> GetFeedbackByIdAsync(int id)
    {
             try
        {
            var feed = await context.Feedbacks.FirstOrDefaultAsync(x => x.Id == id);
            if (feed == null)
                return new Response<GetFeedbackDto>(System.Net.HttpStatusCode.BadRequest, "Feedback not found");
            var mapped = mapper.Map<GetFeedbackDto>(feed);
            return new Response<GetFeedbackDto>(mapped);
        }
        catch (Exception e)
        {
            return new Response<GetFeedbackDto>(System.Net.HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<PagedResponse<List<GetFeedbackDto>>> GetFeedbacksAsync(FeedbackFilter filter)
    {
          try
        {
            var feed = context.Feedbacks.AsQueryable();

            if (!string.IsNullOrEmpty(filter.Text))
                feed = feed.Where(x => x.Text.ToLower().Contains(filter.Text.ToLower()));
                
            var response = await feed
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize).ToListAsync();
            var totalRecord = feed.Count();

            var mapped = mapper.Map<List<GetFeedbackDto>>(response);
            return new PagedResponse<List<GetFeedbackDto>>(mapped, filter.PageNumber, filter.PageSize, totalRecord);

        }
        catch (Exception e)
        {
            return new PagedResponse<List<GetFeedbackDto>>(System.Net.HttpStatusCode.InternalServerError, e.Message);
        }
    }


    




    




    public async Task<Response<string>> UpdateFeedbackAsync(UpdateFeedbackDto feedbackDto)
    {
          try
        {
            var mappedFeedback = mapper.Map<Material>(feedbackDto);
            context.Materials.Update(mappedFeedback);
            var update= await context.SaveChangesAsync();
            if(update==0)  return new Response<string>(System.Net.HttpStatusCode.BadRequest, "Feedback not found");
            return new Response<string>("Feedback updated successfully");
        }
        catch (Exception e)
        {
            return new Response<string>(System.Net.HttpStatusCode.InternalServerError, e.Message);
        }
    }


      public async Task<PagedResponse<List<GetFeedbackForAssignmentDto>>> GetFeedBacksForAssignment(FeedbackFilter filter)
 {
     try
     {
         string zadanie = "AssignmentName";
         var feedbacks = await (from f in context.Feedbacks
                                join a in context.Assignments on f.AssignmentId equals a.Id
                                where a.Description == zadanie
                                select new GetFeedbackForAssignmentDto {
                                    AssignmentTitle = a.Description!,
                                    Feedback = f.Text!
                                }).ToListAsync(); 
         var response=feedbacks
             .Skip((filter.PageNumber-1)*filter.PageSize)
             .Take(filter.PageSize).ToList();
         var totalRecord= feedbacks.Count();
         return new PagedResponse<List<GetFeedbackForAssignmentDto>>(response, filter.PageNumber, filter.PageSize, totalRecord);

             
     }
     catch (Exception e)
     {
         return new PagedResponse<List<GetFeedbackForAssignmentDto>>(System.Net.HttpStatusCode.InternalServerError, e.Message);
         
     }


 }

}




