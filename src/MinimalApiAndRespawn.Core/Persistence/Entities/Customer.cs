namespace MinimalApiAndRespawn.Core.Persistence.Entities;

public class Customer
{
    public int Id { get; set; }
    
    public required string Name { get; set; }

    public required string LastName { get; set; }
}
