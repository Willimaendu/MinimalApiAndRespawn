using MinimalApiAndRespawn.Core.Persistence.Entities;
using MinimalApiAndRespawn.Features.Customers.Contracts.Responses;

namespace MinimalApiAndRespawn.Features.Customers.Mapping;

public static class DomainToApiContractMapper
{
    public static CustomerResponse ToCustomerReponse(this Customer customer)
    {
        return new CustomerResponse
        {
            Id = customer.Id,
            Name = customer.Name,
            LastName = customer.LastName
        };
    }
}
