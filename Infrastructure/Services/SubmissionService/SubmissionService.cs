using AutoMapper;
using Domain.DTOs.SubmissionDto;
using Domain.Entities;
using Domain.Filters;
using Domain.Responses;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.SubmissionService;

public class SubmissionService(DataContext context, IMapper mapper) : ISubmissionService
{
    public async Task<Response<string>> CreateSubmissionAsync(AddSubmissionDto submission)
    {
        try
        {
            var existingSubmission = await context.Submissions.FirstOrDefaultAsync(x => x.Content == submission.Content);
            if (existingSubmission != null)
                return new Response<string>(System.Net.HttpStatusCode.BadRequest, "Submission already exists");
            var mapped = mapper.Map<Submission>(submission);

            await context.Submissions.AddAsync(mapped);
            await context.SaveChangesAsync();

            return new Response<string>("Successfully created a new Submission");
        }
        catch (Exception e)
        {
            return new Response<string>(System.Net.HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<bool>> DeleteSubmissionAsync(int id)
    {
        try
        {
            var sub = await context.Submissions.Where(x => x.Id == id).ExecuteDeleteAsync();
            if (sub == 0)
                return new Response<bool>(System.Net.HttpStatusCode.BadRequest, "Submission not found");

            return new Response<bool>(true);
        }
        catch (Exception e)
        {
            return new Response<bool>(System.Net.HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<GetSubmissionDto>> GetSubmissionByIdAsync(int id)
    {
          try
        {
            var sub = await context.Submissions.FirstOrDefaultAsync(x => x.Id == id);
            if (sub == null)
                return new Response<GetSubmissionDto>(System.Net.HttpStatusCode.BadRequest, "Submission not found");
            var mapped = mapper.Map<GetSubmissionDto>(sub);
            return new Response<GetSubmissionDto>(mapped);
        }
        catch (Exception e)
        {
            return new Response<GetSubmissionDto>(System.Net.HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<PagedResponse<List<GetSubmissionDto>>> GetSubmissionsAsync(SubmissionFilter filter)
    {
           try
        {
            var sub = context.Submissions.AsQueryable();

            if (!string.IsNullOrEmpty(filter.Content))
                sub = sub.Where(x => x.Content.ToLower().Contains(filter.Content.ToLower()));

            var response = await sub
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize).ToListAsync();
            var totalRecord = sub.Count();

            var mapped = mapper.Map<List<GetSubmissionDto>>(response);
            return new PagedResponse<List<GetSubmissionDto>>(mapped, filter.PageNumber, filter.PageSize, totalRecord);

        }
        catch (Exception e)
        {
            return new PagedResponse<List<GetSubmissionDto>>(System.Net.HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<string>> UpdateSubmissionAsync(UpdateSubmissionDto submission)
    {
         try
        {
            var mappedSubmission = mapper.Map<Submission>(submission);
            context.Submissions.Update(mappedSubmission);
            var update= await context.SaveChangesAsync();
            if(update==0)  return new Response<string>(System.Net.HttpStatusCode.BadRequest, "Submission not found");
            return new Response<string>("Submission updated successfully");
        }
        catch (Exception e)
        {
            return new Response<string>(System.Net.HttpStatusCode.InternalServerError, e.Message);
        }
    }

}
