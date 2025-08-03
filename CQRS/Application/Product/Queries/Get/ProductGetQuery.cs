using MediatR;

namespace CQRS.Application.Product.Queries.Get;

public record ProductGetQuery(Guid Id) : IRequest<ProductGetDto>;