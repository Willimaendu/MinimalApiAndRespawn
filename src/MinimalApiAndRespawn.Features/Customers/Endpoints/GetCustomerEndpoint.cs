using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using MinimalApiAndRespawn.Core.Persistence;
using MinimalApiAndRespawn.Features.Customers.Contracts.Requests;
using MinimalApiAndRespawn.Features.Customers.Contracts.Responses;
using MinimalApiAndRespawn.Features.Customers.Mapping;

namespace MinimalApiAndRespawn.Features.Customers.Endpoints;

[HttpGet("/api/customers/{id}"), AllowAnonymous]
public class GetCustomerEndpoint : Endpoint<GetCustomerRequest, CustomerResponse>
{
    private readonly AppDbContext _appDbContext;

    public GetCustomerEndpoint(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public override async Task HandleAsync(GetCustomerRequest request, CancellationToken cancellationToken)
    {
        var customer = await _appDbContext.Customers.FindAsync(request.Id, cancellationToken);

        if(customer == null)
        {
            await SendNotFoundAsync(cancellationToken); 
            return;
        }

        await SendOkAsync(customer.ToCustomerReponse(), cancellationToken);
    }
}
