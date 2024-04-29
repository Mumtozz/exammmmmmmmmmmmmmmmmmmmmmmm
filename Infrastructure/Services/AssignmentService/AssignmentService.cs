using AutoMapper;
using Domain.DTOs;
using Domain.Entities;
using Domain.Filters;
using Domain.Responses;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.AssignmentService;

public class AssignmentService(DataContext context,IMapper mapper) : IAssignmentService
{
    public async Task<Response<string>> CreateAssignmentAsync(AddAssigmentDto assigmentDto)
    {
           try
        {
            var existingAssignment = await context.Assignments.FirstOrDefaultAsync(x => x.Instructor == assigmentDto.Instructor);
            if (existingAssignment != null)
                return new Response<string>(System.Net.HttpStatusCode.BadRequest, "Assignment already exists");
            var mapped = mapper.Map<Assignment>(assigmentDto);

            await context.Assignments.AddAsync(mapped);
            await context.SaveChangesAsync();

            return new Response<string>("Successfully created a new Assignment");
        }
        catch (Exception e)
        {
            return new Response<string>(System.Net.HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<bool>> DeleteAssignmentAsync(int id)
    {
         try
        {
            var assign = await context.Assignments.Where(x => x.Id == id).ExecuteDeleteAsync();
            if (assign == 0)
                return new Response<bool>(System.Net.HttpStatusCode.BadRequest, "Assignment not found");

            return new Response<bool>(true);
        }
        catch (Exception e)
        {
            return new Response<bool>(System.Net.HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<GetAssigmentDto>> GetAssignmentByIdAsync(int id)
    {
          try
        {
            var assign = await context.Assignments.FirstOrDefaultAsync(x => x.Id == id);
            if (assign == null)
                return new Response<GetAssigmentDto>(System.Net.HttpStatusCode.BadRequest, "Assignment not found");
            var mapped = mapper.Map<GetAssigmentDto>(assign);
            return new Response<GetAssigmentDto>(mapped);
        }
        catch (Exception e)
        {
            return new Response<GetAssigmentDto>(System.Net.HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<PagedResponse<List<GetAssigmentDto>>> GetAssignmentsAsync(AssigmentFilter filter)
    {
          try
        {
            var assign = context.Assignments.AsQueryable();

            if (!string.IsNullOrEmpty(filter.Instructor))
                assign = assign.Where(x => x.Instructor.ToLower().Contains(filter.Instructor.ToLower()));

            var response = await assign
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize).ToListAsync();
            var totalRecord = assign.Count();

            var mapped = mapper.Map<List<GetAssigmentDto>>(response);
            return new PagedResponse<List<GetAssigmentDto>>(mapped, filter.PageNumber, filter.PageSize, totalRecord);

        }
        catch (Exception e)
        {
            return new PagedResponse<List<GetAssigmentDto>>(System.Net.HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<string>> UpdateAssignmentAsync(UpdateAssigmentDto assigmentDto)
    {
         try
        {
            var mappedAssignment = mapper.Map<Assignment>(assigmentDto);
            context.Assignments.Update(mappedAssignment);
            var update= await context.SaveChangesAsync();
            if(update==0)  return new Response<string>(System.Net.HttpStatusCode.BadRequest, "Assignment not found");
            return new Response<string>("Assignment updated successfully");
        }
        catch (Exception e)
        {
            return new Response<string>(System.Net.HttpStatusCode.InternalServerError, e.Message);
        }
    }

}
