using Domain.DTOs.MaterialDto;
using Domain.Filters;
using Domain.Responses;
using Infrastructure.Services.MaterialService;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[Route("[controller]")]
[ApiController]
public class MaterialController(IMaterialService service)
{
    [HttpGet("get-Materials")]
    public async Task<Response<List<GetMaterialDto>>> GetMaterialsAsync([FromQuery] MaterialFilter filter)
    {
        return await service.GetMaterialsAsync(filter);
    }
    [HttpGet("{MaterialId:int}")]
    public async Task<Response<GetMaterialDto>> GetMaterialByIdAsync(int MaterialId)
    {
        return await service.GetMaterialByIdAsync(MaterialId);
    }

    [HttpPost("create-Material")]
    public async Task<Response<string>> AddMaterialAsync(AddMaterialDto MaterialDto)
    {
        return await service.CreateMaterialAsync(MaterialDto);
    }

    [HttpPut("update-Material")]
    public async Task<Response<string>> UpdateMaterialAsync(UpdateMaterialDto MaterialDto)
    {
        return await service.UpdateMaterialAsync(MaterialDto);
    }

    [HttpDelete("{MaterialId:int}")]
    public async Task<Response<bool>> DeleteMaterialAsync(int MaterialId)
    {
        return await service.DeleteMaterialAsync(MaterialId);
    }
}
