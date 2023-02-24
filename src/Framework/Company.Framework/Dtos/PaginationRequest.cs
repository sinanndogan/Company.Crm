namespace Company.Framework.Dtos;

public class PaginationRequest
{
    public int Page { get; set; } = 1;
    public int PerPage { get; set; } = 10;
    public int Skip => (Page - 1) * PerPage;
}
