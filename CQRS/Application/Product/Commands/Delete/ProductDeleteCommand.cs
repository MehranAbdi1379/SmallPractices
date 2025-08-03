using MediatR;

namespace CQRS.Application.Product.Commands.Delete;

public record ProductDeleteCommand(Guid Id): IRequest;