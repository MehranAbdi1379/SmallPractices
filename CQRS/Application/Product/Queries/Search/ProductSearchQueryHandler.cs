using CQRS.Application.Product.Queries.Get;
using CQRS.Application.Product.Queries.List;
using CQRS.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CQRS.Application.Product.Queries.Search;

public class ProductSearchQueryHandler(AppDbContext context) : IRequestHandler<ProductSearchQuery, ProductListDto>
{
    public async Task<ProductListDto> Handle(ProductSearchQuery request, CancellationToken cancellationToken)
    {
        var products = await context.Products.AsNoTracking()
            .Where(p => p.Name.ToLower().Contains(request.Query.ToLower()) ||
                        p.Description.ToLower().Contains(request.Query.ToLower()))
            .ToListAsync(cancellationToken);
        var productDtos = products.Select(p => new ProductGetDto(p.Id, p.Name, p.Description, p.Price)).ToList();
        return new ProductListDto(productDtos, products.Count);
    }
}