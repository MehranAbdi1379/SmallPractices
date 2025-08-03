using CQRS.Application.Product.Queries.Get;
using CQRS.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CQRS.Application.Product.Queries.List;

public class ProductListQueryHandler: IRequestHandler<ProductListQuery, ProductListDto>
{
    private readonly AppDbContext _context;

    public ProductListQueryHandler(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<ProductListDto> Handle(ProductListQuery request, CancellationToken cancellationToken)
    {
        var products = await _context.Products.AsNoTracking().Select(p => new ProductGetDto(p.Id, p.Name, p.Description, p.Price)).ToListAsync(cancellationToken);
        return new ProductListDto(products, products.Count);
    }
}