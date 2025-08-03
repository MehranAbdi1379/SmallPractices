using MediatR;

namespace CQRS.Application.Product.Commands.Create;

public record ProductCreateCommand(string Name, string Description, decimal Price): IRequest<Guid>;