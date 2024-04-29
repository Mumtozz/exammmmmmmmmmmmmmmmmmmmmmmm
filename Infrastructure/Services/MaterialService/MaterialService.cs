using AutoMapper;
using Domain.DTOs.MaterialDto;
using Domain.Entities;
using Domain.Filters;
using Domain.Responses;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.MaterialService;

public class MaterialService(DataContext context, IMapper mapper)  : IMaterialService
{
    public async Task<Response<string>> CreateMaterialAsync(AddMaterialDto materialDto)
    {
         try
        {
            var existingStudent = await context.Materials.FirstOrDefaultAsync(x => x.Title == materialDto.Title);
            if (existingStudent != null)
                return new Response<string>(System.Net.HttpStatusCode.BadRequest, "Material already exists");
            var mapped = mapper.Map<Material>(materialDto);

            await context.Materials.AddAsync(mapped);
            await context.SaveChangesAsync();

            return new Response<string>("Successfully created a new Material");
        }
        catch (Exception e)
        {
            return new Response<string>(System.Net.HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<bool>> DeleteMaterialAsync(int id)
    {
         try
        {
            var material = await context.Materials.Where(x => x.Id == id).ExecuteDeleteAsync();
            if (material == 0)
                return new Response<bool>(System.Net.HttpStatusCode.BadRequest, "Material not found");

            return new Response<bool>(true);
        }
        catch (Exception e)
        {
            return new Response<bool>(System.Net.HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<PagedResponse<List<GetMaterialDto>>> GetMaterialsAsync(MaterialFilter filter)
    {
         try
        {
            var materials = context.Materials.AsQueryable();

            if (!string.IsNullOrEmpty(filter.Title))
                materials = materials.Where(x => x.Title.ToLower().Contains(filter.Title.ToLower()));

            var response = await materials
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize).ToListAsync();
            var totalRecord = materials.Count();

            var mapped = mapper.Map<List<GetMaterialDto>>(response);
            return new PagedResponse<List<GetMaterialDto>>(mapped, filter.PageNumber, filter.PageSize, totalRecord);

        }
        catch (Exception e)
        {
            return new PagedResponse<List<GetMaterialDto>>(System.Net.HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<GetMaterialDto>> GetMaterialByIdAsync(int id)
    {
          try
        {
            var material = await context.Materials.FirstOrDefaultAsync(x => x.Id == id);
            if (material == null)
                return new Response<GetMaterialDto>(System.Net.HttpStatusCode.BadRequest, "Material not found");
            var mapped = mapper.Map<GetMaterialDto>(material);
            return new Response<GetMaterialDto>(mapped);
        }
        catch (Exception e)
        {
            return new Response<GetMaterialDto>(System.Net.HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<string>> UpdateMaterialAsync(UpdateMaterialDto materialDto)
    {
        
        try
        {
            var mappedMaterial = mapper.Map<Material>(materialDto);
            context.Materials.Update(mappedMaterial);
            var update= await context.SaveChangesAsync();
            if(update==0)  return new Response<string>(System.Net.HttpStatusCode.BadRequest, "Material not found");
            return new Response<string>("Material updated successfully");
        }
        catch (Exception e)
        {
            return new Response<string>(System.Net.HttpStatusCode.InternalServerError, e.Message);
        }
    }

}
