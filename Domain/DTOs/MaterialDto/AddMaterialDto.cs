namespace Domain.DTOs.MaterialDto;

public class AddMaterialDto
{
    
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string ContentUrl { get; set; } = null!;
    public int CourseId { get; set; }
}
