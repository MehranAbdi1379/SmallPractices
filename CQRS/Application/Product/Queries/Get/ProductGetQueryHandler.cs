using CQRS.Application.Product.Exceptions;
using CQRS.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CQRS.Application.Product.Queries.Get;

public class ProductGetQueryHandler: IRequestHandler<ProductGetQuery, ProductGetDto>
{
    private readonly AppDbContext context;

    public ProductGetQueryHandler(AppDbContext context)
    {
        this.context = context;
    }
    public async Task<ProductGetDto> Handle(ProductGetQuery request, CancellationToken cancellationToken)
    {
        var product = await context.Products.SingleOrDefaultAsync(p => p.Id == request.Id, cancellationToken);
        if(product == null) throw new ProductNotFoundException(request.Id.ToString());
        return new ProductGetDto(product.Id, product.Name, product.Description, product.Price);
    }
}