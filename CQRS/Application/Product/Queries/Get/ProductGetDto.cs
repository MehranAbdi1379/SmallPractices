namespace CQRS.Application.Product.Queries.Get;

public record ProductGetDto(Guid Id, string Name, string Description, decimal Price);