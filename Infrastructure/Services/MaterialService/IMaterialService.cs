using Domain.DTOs.MaterialDto;
using Domain.Filters;
using Domain.Responses;

namespace Infrastructure.Services.MaterialService;

public interface IMaterialService
{
    
    Task<PagedResponse<List<GetMaterialDto>>> GetMaterialsAsync(MaterialFilter filter);
    Task<Response<GetMaterialDto>> GetMaterialByIdAsync(int id);
    Task<Response<string>> CreateMaterialAsync(AddMaterialDto materialDto);
    Task<Response<string>> UpdateMaterialAsync(UpdateMaterialDto materialDto);
    Task<Response<bool>> DeleteMaterialAsync(int id);
}
