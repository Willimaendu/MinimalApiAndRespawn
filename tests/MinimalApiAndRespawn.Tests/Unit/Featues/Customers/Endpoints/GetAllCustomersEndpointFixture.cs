using FastEndpoints;
using FluentAssertions;
using MinimalApiAndRespawn.Features.Customers.Endpoints;
using MinimalApiAndRespawn.Tests.Unit;
using System.Net;
using Xunit;

namespace MinimalApiAndRespawn.Tests.UnitTests.Featues.Customers.Endpoints;

public class GetAllCustomersEndpointFixture : DatabaseFixture
{
    private readonly GetAllCustomersEndpoint _getAllCustomerEndpoint;

    public GetAllCustomersEndpointFixture()
    {
        SetupDatabase(dbContext =>
        {
            dbContext.Customers.AddRange(
                new()
                {
                    Name = "Test",
                    LastName = "Test"
                },
                new()
                {
                    Name = "Test",
                    LastName = "Test"
                });
        });
        _getAllCustomerEndpoint = Factory.Create<GetAllCustomersEndpoint>(DbContextFactory);
    }

    [Fact]
    public async Task GetAllCustomersEndpoint_HandleAsync_ShouldReturn200_WhenCustomersExist()
    {
        // Arrange
        var getAllCustomersRequest = new EmptyRequest();

        // Act
        await _getAllCustomerEndpoint.HandleAsync(getAllCustomersRequest, default);

        // Assert
        _getAllCustomerEndpoint.HttpContext.Response.StatusCode.Should().Be((int)HttpStatusCode.OK);
        _getAllCustomerEndpoint.Response.Count.Should().Be(2);
    }
}
