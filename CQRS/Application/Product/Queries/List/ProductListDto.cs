using CQRS.Application.Product.Queries.Get;

namespace CQRS.Application.Product.Queries.List;

public record ProductListDto(List<ProductGetDto> Products, int TotalCount);