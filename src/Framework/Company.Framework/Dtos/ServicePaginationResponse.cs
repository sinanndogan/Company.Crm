namespace Company.Framework.Dtos;

public class ServicePaginationResponse<TKey> : ServiceResponse<TKey> where TKey : class
{
    public ServicePaginationResponse(TKey data, int total, PaginationRequest req)
    {
        IsSuccess = true;
        Data = data;
        Meta = new(req.Page, total, req.PerPage);
    }

    public PaginationMeta Meta { get; set; }
}

public class PaginationMeta
{
    public PaginationMeta(int page, int total, int perPage)
    {
        CurrentPage = page;
        Total = total;
        PerPage = perPage;

        var pageCount = (double)Total / PerPage;
        LastPage = (int)Math.Ceiling(pageCount);
    }

    public int Total { get; set; }
    public int PerPage { get; set; } = 10;
    public int CurrentPage { get; set; } = 1;
    public int LastPage { get; set; }

    public int OffsetFrom => (CurrentPage - 1) * PerPage + 1;
    public int OffsetTo => Math.Min(CurrentPage * PerPage, Total);
}
