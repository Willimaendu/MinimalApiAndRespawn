using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using MinimalApiAndRespawn.Core.Persistence;
using MinimalApiAndRespawn.Core.Persistence.Entities;
using MinimalApiAndRespawn.Features.Customers.Contracts.Requests;
using MinimalApiAndRespawn.Features.Customers.Contracts.Responses;
using MinimalApiAndRespawn.Features.Customers.Mapping;

namespace MinimalApiAndRespawn.Features.Customers.Endpoints;

[HttpPost("api/customers/create"), AllowAnonymous]
public class CreateCustomerEndpoint : Endpoint<CreateCustomerRequest, CustomerResponse>
{
    private readonly IDbContextFactory<AppDbContext> _dbContextFactory;

    public CreateCustomerEndpoint(IDbContextFactory<AppDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public override async Task HandleAsync(CreateCustomerRequest request, CancellationToken cancellationToken = default)
    {
        var customer = new Customer
        {
            LastName = request.LastName,
            Name = request.Name
        };

        await using var dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
        dbContext.Customers.Add(customer);
        await dbContext.SaveChangesAsync(cancellationToken);
        await SendCreatedAtAsync<GetCustomerEndpoint>(
            new { customer.Id },
            customer.ToCustomerReponse(),
            generateAbsoluteUrl: true,
            cancellation: cancellationToken);

    }
}
