namespace CQRS.Application.Product.Exceptions;

public class ProductNotFoundException : Exception
{
    public ProductNotFoundException(string productId): base($"Product not found with id of {productId}")
    {
    }
    
}