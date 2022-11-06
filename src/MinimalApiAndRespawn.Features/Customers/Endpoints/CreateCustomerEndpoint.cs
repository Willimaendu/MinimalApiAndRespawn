using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using MinimalApiAndRespawn.Core.Persistence;
using MinimalApiAndRespawn.Core.Persistence.Entities;
using MinimalApiAndRespawn.Features.Customers.Contracts.Requests;
using MinimalApiAndRespawn.Features.Customers.Contracts.Responses;
using MinimalApiAndRespawn.Features.Customers.Mapping;

namespace MinimalApiAndRespawn.Features.Customers.Endpoints;

[HttpPost("api/customers/create"), AllowAnonymous]
public class CreateCustomerEndpoint : Endpoint<CreateCustomerRequest, CustomerResponse>
{
    private readonly AppDbContext _appDbContext;

    public CreateCustomerEndpoint(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public override async Task HandleAsync(CreateCustomerRequest request, CancellationToken cancellationToken)
    {
        var customer = new Customer
        {
            LastName = request.LastName,
            Name = request.Name
        };

        _appDbContext.Customers.Add(customer);

        var customerResponse = customer.ToCustomerReponse();

        if (await _appDbContext.SaveChangesAsync(cancellationToken) > 0)
        {
            await SendCreatedAtAsync<GetCustomerEndpoint>(new { customer.Id }, customerResponse, generateAbsoluteUrl: true, cancellation: cancellationToken);
        }
        else
        {
            await SendErrorsAsync(cancellation: cancellationToken);
        }
    }
}
