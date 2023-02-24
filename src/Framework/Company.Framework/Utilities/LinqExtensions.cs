using Company.Framework.Dtos;

namespace Company.Framework.Utilities;

public static class LinqExtensions
{
    public static List<T> ToPagedList<T>(this IQueryable<T> source, PaginationRequest req)
    {
        return source.Skip(req.Skip).Take(req.PerPage).ToList();
    }
}