namespace MinimalApiAndRespawn.Features.Customers.Contracts.Responses;

public class CustomerResponse
{
    public int Id { get; init; } = default!;

    public string Name { get; init; } = default!;

    public string LastName { get; init; } = default!;
}
