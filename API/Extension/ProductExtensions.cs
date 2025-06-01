using API.Entities;

namespace API.Extension;

public static class ProductExtensions
{
    public static IQueryable<Product> Sort(this IQueryable<Product> query, string? orderBy)
    {
        query = orderBy switch
        {
            "price" => query.OrderBy(p => p.Price),
            "priceDesc" => query.OrderByDescending(p => p.Price),
            _ => query.OrderBy(p => p.Name)
        };
        return query;
    }
    public static IQueryable<Product> Search(this IQueryable<Product> query, string? searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm)) return query;

        var lowerSearchTerm = searchTerm.Trim().ToLower();
        return query.Where(p => p.Name.ToLower().Contains(lowerSearchTerm));
    }
    public static IQueryable<Product> Filter(this IQueryable<Product> query, string brands, string types)
    {
        List<string> brandList = [];
        List<string> typeList = [];

        if (!string.IsNullOrEmpty(brands))
        {
            brandList.AddRange(brands.ToLower().Split(",").ToList());
        }

        if (!string.IsNullOrEmpty(types))
        {
            typeList.AddRange(types.ToLower().Split(",").ToList());
        }

        query = query.Where(p => brandList.Count == 0 || brandList.Contains(p.Brand.ToLower()));

        query = query.Where(p => typeList.Count == 0 || typeList.Contains(p.Type.ToLower()));

        return query;
    }
}
