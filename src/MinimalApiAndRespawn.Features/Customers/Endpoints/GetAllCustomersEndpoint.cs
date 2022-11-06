using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using MinimalApiAndRespawn.Core.Persistence;

namespace MinimalApiAndRespawn.Features.Customers.Endpoints;

[HttpGet("/api/customers/"), AllowAnonymous]
public class GetAllCustomersEndpoint : Endpoint<EmptyRequest>
{
    private readonly AppDbContext _appDbContext;

    public GetAllCustomersEndpoint(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public override async Task HandleAsync(EmptyRequest request, CancellationToken cancellationToken)
    {
        await SendOkAsync(
            await _appDbContext.Customers.Select(customer => new
            {
                customer.Name,
                customer.LastName
            })
            .ToListAsync(cancellationToken), cancellationToken);
    }
}
