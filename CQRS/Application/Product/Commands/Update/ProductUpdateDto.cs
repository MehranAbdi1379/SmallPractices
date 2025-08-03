namespace CQRS.Application.Product.Commands.Update;

public record ProductUpdateDto(Guid Id, string Name, string Description, decimal Price);