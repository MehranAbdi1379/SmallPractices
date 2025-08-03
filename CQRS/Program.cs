using System.Reflection;
using System.Text.Json;
using CQRS.Application.Product.Commands.Create;
using CQRS.Application.Product.Commands.Delete;
using CQRS.Application.Product.Commands.Update;
using CQRS.Application.Product.Queries.Get;
using CQRS.Application.Product.Queries.List;
using CQRS.Application.Product.Queries.Search;
using CQRS.Infrastructure.Data;
using MediatR;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        context.Response.StatusCode = 500;
        context.Response.ContentType = "application/json";

        var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();

        if (exceptionHandlerPathFeature?.Error != null)
        {
            var errorMessage = exceptionHandlerPathFeature.Error.Message;

            await context.Response.WriteAsync(
                JsonSerializer.Serialize(new { message = errorMessage }));
        }
    });
});


app.MapGet("/products/{id:guid}", async (Guid id, ISender mediatr, CancellationToken cancellationToken) =>
{
    var product = await mediatr.Send(new ProductGetQuery(id));
    return Results.Ok(product);
});

app.MapGet("/products", async (ISender mediatr, CancellationToken cancellationToken) =>
{
    var products = await mediatr.Send(new ProductListQuery(), cancellationToken);
    return Results.Ok(products);
});

app.MapPost("/products", async (ProductCreateCommand command, ISender mediatr, CancellationToken cancellationToken) =>
{
    var productId = await mediatr.Send(command, cancellationToken);
    return Results.Created($"/products/{productId}", new { id = productId });
});

app.MapDelete("/products/{id:guid}", async (Guid id, ISender mediatr, CancellationToken cancellationToken) =>
{
    await mediatr.Send(new ProductDeleteCommand(id), cancellationToken);
    return Results.NoContent();
});

app.MapPut("/products",
    async (ProductUpdateCommand command, ISender mediatr, CancellationToken cancellationToken) =>
    {
        var product = await mediatr.Send(command, cancellationToken);
        return Results.Ok(product);
    });

app.MapGet("/products/search/{query}",
    async (string query, ISender mediatr, CancellationToken cancellationToken) =>
    {
        var products = await mediatr.Send(new ProductSearchQuery(query), cancellationToken);
        return Results.Ok(products);
    });

app.Run();