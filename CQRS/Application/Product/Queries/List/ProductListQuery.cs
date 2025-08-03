using MediatR;

namespace CQRS.Application.Product.Queries.List;

public record ProductListQuery: IRequest<ProductListDto>;