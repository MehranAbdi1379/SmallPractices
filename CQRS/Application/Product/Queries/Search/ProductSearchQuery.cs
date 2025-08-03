using CQRS.Application.Product.Queries.List;
using MediatR;

namespace CQRS.Application.Product.Queries.Search;

public record ProductSearchQuery(string Query) : IRequest<ProductListDto>;