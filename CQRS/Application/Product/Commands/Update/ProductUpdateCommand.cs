using CQRS.Application.Product.Queries.Get;
using MediatR;

namespace CQRS.Application.Product.Commands.Update;

public record ProductUpdateCommand(Guid Id, string Name, string Description, decimal Price) : IRequest<ProductGetDto>;