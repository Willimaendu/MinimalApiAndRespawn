using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using MinimalApiAndRespawn.Core.Persistence;
using MinimalApiAndRespawn.Features.Customers.Contracts.Requests;

namespace MinimalApiAndRespawn.Features.Customers.Endpoints;

[HttpDelete("/api/customers/{id}"), AllowAnonymous]
public class DeleteCustomerEndpoint : Endpoint<DeleteCustomerRequest, EmptyResponse>
{
    private readonly IDbContextFactory<AppDbContext> _dbContextFactory;

    public DeleteCustomerEndpoint(IDbContextFactory<AppDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public override async Task HandleAsync(DeleteCustomerRequest request, CancellationToken cancellationToken = default)
    {
        await using var dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
        var customerToDelete = await dbContext.Customers.SingleOrDefaultAsync(customer => customer.Id == request.Id, cancellationToken);
        if(customerToDelete != null)
        {
            dbContext.Customers.Remove(customerToDelete);
            if (await dbContext.SaveChangesAsync(cancellationToken) > 0)
            {
                await SendOkAsync(cancellationToken);
                return;
            }
        }
        await SendErrorsAsync(cancellation: cancellationToken);
    }
}
