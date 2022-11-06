using EntityFrameworkCore.Testing.NSubstitute;
using FastEndpoints;
using FluentAssertions;
using MinimalApiAndRespawn.Core.Persistence;
using MinimalApiAndRespawn.Core.Persistence.Entities;
using MinimalApiAndRespawn.Features.Customers.Contracts.Requests;
using MinimalApiAndRespawn.Features.Customers.Endpoints;
using NSubstitute;
using Xunit;

namespace MinimalApiAndRespawn.Tests.UnitTests.Featues.Customers.Endpoints;

public class CreateCustomerEndpointFixture
{
    [Fact]
    public async Task CreateCustomerEndpoint_HandleAsync_ShouldBeSuccessful()
    {
        // Arrange
        var db = Create.MockedDbContextFor<AppDbContext>();
        var createCustomerEndpoint = Factory.Create<CreateCustomerEndpoint>(db);
        var createCustomerRequest = new CreateCustomerRequest
        {
            Name = "Test",
            LastName = "Test"
        };

        // Act
        await createCustomerEndpoint.HandleAsync(createCustomerRequest, default);

        // Assert
        createCustomerEndpoint.Response.Should().NotBeNull();
        db.Customers.Received(1).Add(Arg.Any<Customer>());
        await db.Received(1).SaveChangesAsync();
        //db.Customers.Received().Add(Arg.Any<Customer>());
        //db.Customers.Add.Should()
    }
}
