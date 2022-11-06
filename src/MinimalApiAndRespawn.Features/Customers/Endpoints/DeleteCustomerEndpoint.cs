using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using MinimalApiAndRespawn.Core.Persistence;
using MinimalApiAndRespawn.Features.Customers.Contracts.Requests;

namespace MinimalApiAndRespawn.Features.Customers.Endpoints;

[HttpDelete("/api/customers/{id}"), AllowAnonymous]
public class DeleteCustomerEndpoint : Endpoint<DeleteCustomerRequest>
{
    private readonly AppDbContext _appDbContext;

	public DeleteCustomerEndpoint(AppDbContext appDbContext)
	{
		_appDbContext = appDbContext;
	}

    public override async Task HandleAsync(DeleteCustomerRequest request, CancellationToken cancellationToken)
    {
        var customerToDelete = await _appDbContext.Customers.SingleAsync(customer => customer.Id == request.Id, cancellationToken);
        _appDbContext.Customers.Remove(customerToDelete);

        if (await _appDbContext.SaveChangesAsync(cancellationToken) > 0)
        {
            await SendOkAsync(cancellationToken);
        }
        else
        {
            await SendErrorsAsync(cancellation: cancellationToken);
        }
    }
}
