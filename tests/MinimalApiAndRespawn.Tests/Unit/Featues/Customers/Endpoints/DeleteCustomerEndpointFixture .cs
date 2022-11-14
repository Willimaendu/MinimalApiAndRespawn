using FastEndpoints;
using FluentAssertions;
using MinimalApiAndRespawn.Core.Persistence.Entities;
using MinimalApiAndRespawn.Features.Customers.Contracts.Requests;
using MinimalApiAndRespawn.Features.Customers.Endpoints;
using MinimalApiAndRespawn.Tests.Unit;
using System.Net;
using Xunit;

namespace MinimalApiAndRespawn.Tests.UnitTests.Featues.Customers.Endpoints;

public class DeleteCustomerEndpointFixture : DatabaseFixture
{
    private static readonly Customer _customerToFind = new()
    {
        Name = "Test",
        LastName = "Test"
    };

    private readonly DeleteCustomerEndpoint _deleteCustomerEndpoint;

    public DeleteCustomerEndpointFixture()
    {
        SetupDatabase(dbContext =>
        {
            dbContext.Customers.Add(_customerToFind);
        });
        _deleteCustomerEndpoint = Factory.Create<DeleteCustomerEndpoint>(DbContextFactory);
    }

    [Fact]
    public async Task DeleteCustomerEndpoint_HandleAsync_ShouldReturn200_WhenCustomerIdExists()
    {
        // Arrange
        var deleteCustomerRequest = new DeleteCustomerRequest
        {
            Id = _customerToFind.Id,
        };

        // Act
        await _deleteCustomerEndpoint.HandleAsync(deleteCustomerRequest, default);

        // Assert
        _deleteCustomerEndpoint.HttpContext.Response.StatusCode.Should().Be((int)HttpStatusCode.OK);
    }

    [Fact]
    public async Task DeleteCustomerEndpoint_HandleAsync_ShouldReturn400_WhenCustomerIdNotExists()
    {
        // Arrange
        var deleteCustomerRequest = new DeleteCustomerRequest();

        // Act
        await _deleteCustomerEndpoint.HandleAsync(deleteCustomerRequest, default);

        // Assert
        _deleteCustomerEndpoint.HttpContext.Response.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
    }
}
