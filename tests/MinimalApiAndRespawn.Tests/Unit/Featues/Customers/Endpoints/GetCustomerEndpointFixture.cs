using FastEndpoints;
using FluentAssertions;
using MinimalApiAndRespawn.Core.Persistence.Entities;
using MinimalApiAndRespawn.Features.Customers.Contracts.Requests;
using MinimalApiAndRespawn.Features.Customers.Endpoints;
using MinimalApiAndRespawn.Tests.Unit;
using System.Net;
using Xunit;

namespace MinimalApiAndRespawn.Tests.UnitTests.Featues.Customers.Endpoints;

public class GetCustomerEndpointFixture : DatabaseFixture
{
    private static readonly Customer _customerToFind = new()
    {
        Name = "Test",
        LastName = "Test"
    };

    private readonly GetCustomerEndpoint _getCustomerEndpoint;

    public GetCustomerEndpointFixture()
    {
        SetupDatabase(dbContext =>
        {
            dbContext.Customers.Add(_customerToFind);
        });
        _getCustomerEndpoint = Factory.Create<GetCustomerEndpoint>(DbContextFactory);
    }

    [Fact]
    public async Task GetCustomerEndpoint_HandleAsync_ShouldReturn200_WhenCustomerIdExists()
    {
        // Arrange
        var getCustomerRequest = new GetCustomerRequest
        {
            Id = _customerToFind.Id,
        };

        // Act
        await _getCustomerEndpoint.HandleAsync(getCustomerRequest, default);

        // Assert
        _getCustomerEndpoint.HttpContext.Response.StatusCode.Should().Be((int)HttpStatusCode.OK);
        _getCustomerEndpoint.Response.Id.Should().Be(_customerToFind.Id);
        _getCustomerEndpoint.Response.Name.Should().Be(_customerToFind.Name);
        _getCustomerEndpoint.Response.LastName.Should().Be(_customerToFind.LastName);
    }

    [Fact]
    public async Task GetCustomerEndpoint_HandleAsync_ShouldReturn404_WhenCustomerIdNotExists()
    {
        // Arrange
        var getCustomerRequest = new GetCustomerRequest();

        // Act
        await _getCustomerEndpoint.HandleAsync(getCustomerRequest, default);

        // Assert
        _getCustomerEndpoint.HttpContext.Response.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
        _getCustomerEndpoint.Response.Id.Should().Be(0);
        _getCustomerEndpoint.Response.Name.Should().BeNull();
        _getCustomerEndpoint.Response.LastName.Should().BeNull();
    }
}
