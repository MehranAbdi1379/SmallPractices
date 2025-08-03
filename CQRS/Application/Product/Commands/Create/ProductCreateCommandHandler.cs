using CQRS.Infrastructure.Data;
using MediatR;

namespace CQRS.Application.Product.Commands.Create;

public class ProductCreateCommandHandler: IRequestHandler<ProductCreateCommand, Guid>
{
    private readonly AppDbContext _context;

    public ProductCreateCommandHandler(AppDbContext context)
    {
        _context = context;
    }
    public async Task<Guid> Handle(ProductCreateCommand request, CancellationToken cancellationToken)
    {
        var product = new Domain.Entities.Product(request.Name, request.Description, request.Price);
        await _context.Products.AddAsync(product, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return product.Id;
    }
}