using CQRS.Application.Product.Exceptions;
using CQRS.Application.Product.Queries.Get;
using CQRS.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CQRS.Application.Product.Commands.Update;

public class ProductUpdateCommandHandler : IRequestHandler<ProductUpdateCommand, ProductGetDto>
{
    private readonly AppDbContext _context;

    public ProductUpdateCommandHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ProductGetDto> Handle(ProductUpdateCommand request, CancellationToken cancellationToken)
    {
        var product = await _context.Products.SingleOrDefaultAsync(p => p.Id == request.Id, cancellationToken);
        if (product == null) throw new ProductNotFoundException(request.Id.ToString());
        product.Name = request.Name;
        product.Description = request.Description;
        product.Price = request.Price;
        _context.Products.Update(product);
        await _context.SaveChangesAsync(cancellationToken);
        return new ProductGetDto(product.Id, product.Name, product.Description, product.Price);
    }
}