namespace MinimalApiAndRespawn.Features.Customers.Contracts.Requests;

public class CreateCustomerRequest
{
    public string Name { get; init; } = default!;

    public string LastName { get; init; } = default!;
}
