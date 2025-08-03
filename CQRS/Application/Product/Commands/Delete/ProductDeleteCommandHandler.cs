using CQRS.Application.Product.Exceptions;
using CQRS.Infrastructure.Data;
using MediatR;

namespace CQRS.Application.Product.Commands.Delete;

public class ProductDeleteCommandHandler: IRequestHandler<ProductDeleteCommand>
{
    private readonly AppDbContext _context;

    public ProductDeleteCommandHandler(AppDbContext context)
    {
        _context = context;
    }

    public Task Handle(ProductDeleteCommand request, CancellationToken cancellationToken)
    {
        var product = _context.Products.SingleOrDefault(p => p.Id == request.Id);
        if(product == null) throw new ProductNotFoundException(request.Id.ToString());
        _context.Products.Remove(product);
        return _context.SaveChangesAsync(cancellationToken);
    }
}