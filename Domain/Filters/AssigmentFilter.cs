namespace Domain.Filters;

public class AssigmentFilter : PaginationFilter
{
    public string Instructor { get; set; } = null!;

}
