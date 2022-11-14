using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using MinimalApiAndRespawn.Core.Persistence;
using MinimalApiAndRespawn.Features.Customers.Contracts.Requests;
using MinimalApiAndRespawn.Features.Customers.Contracts.Responses;
using MinimalApiAndRespawn.Features.Customers.Mapping;

namespace MinimalApiAndRespawn.Features.Customers.Endpoints;

[HttpGet("/api/customers/{id}"), AllowAnonymous]
public class GetCustomerEndpoint : Endpoint<GetCustomerRequest, CustomerResponse>
{
    private readonly IDbContextFactory<AppDbContext> _dbContextFactory;

    public GetCustomerEndpoint(IDbContextFactory<AppDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public override async Task HandleAsync(GetCustomerRequest request, CancellationToken cancellationToken = default)
    {
        await using var dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
        var customer = await dbContext.Customers.FindAsync(new object[] { request.Id }, cancellationToken: cancellationToken);

        if(customer == null)
        {
            await SendNotFoundAsync(cancellationToken); 
            return;
        }

        await SendOkAsync(customer.ToCustomerReponse(), cancellationToken);
    }
}
