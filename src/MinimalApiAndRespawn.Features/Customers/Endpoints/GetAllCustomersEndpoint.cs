using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using MinimalApiAndRespawn.Core.Persistence;
using MinimalApiAndRespawn.Features.Customers.Contracts.Responses;
using MinimalApiAndRespawn.Features.Customers.Mapping;

namespace MinimalApiAndRespawn.Features.Customers.Endpoints;

[HttpGet("/api/customers/"), AllowAnonymous]
public class GetAllCustomersEndpoint : Endpoint<EmptyRequest, ICollection<CustomerResponse>>
{
    private readonly IDbContextFactory<AppDbContext> _dbContextFactory;

    public GetAllCustomersEndpoint(IDbContextFactory<AppDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public override async Task HandleAsync(EmptyRequest request, CancellationToken cancellationToken = default)
    {
        await using var dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
        await SendOkAsync(
            await dbContext.Customers.Select(customer => customer.ToCustomerReponse())
            .ToListAsync(cancellationToken), cancellationToken);
    }
}
