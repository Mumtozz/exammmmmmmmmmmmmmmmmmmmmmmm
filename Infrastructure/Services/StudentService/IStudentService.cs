using Domain.DTOs.StudentDto;
using Domain.Filters;
using Domain.Responses;

namespace Infrastructure.Services.StudentService;

public interface IStudentService
{
    
    Task<PagedResponse<List<GetStudentDto>>> GetStudentsAsync(StudentFilter filter);
    Task<Response<GetStudentDto>> GetStudentByIdAsync(int id);
    Task<Response<string>> CreateStudentAsync(AddStudentDto student);
    Task<Response<string>> UpdateStudentAsync(UpdateStudentDto student);
    Task<Response<bool>> DeleteStudentAsync(int id);
    Task<PagedResponse<List<GetStudentDto>>> GetStudentByDate(PaginationFilter filter);
    Task<PagedResponse<List<StudentSubmission>>> GetStudentsSubmission(PaginationFilter filter);
    Task<PagedResponse<List<StudentSubmission>>> GetStudentCountSubmission(PaginationFilter filter);
    
}
