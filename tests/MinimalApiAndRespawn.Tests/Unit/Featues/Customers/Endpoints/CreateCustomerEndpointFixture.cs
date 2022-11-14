using FastEndpoints;
using FluentAssertions;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using MinimalApiAndRespawn.Core.Persistence;
using MinimalApiAndRespawn.Features.Customers.Contracts.Requests;
using MinimalApiAndRespawn.Features.Customers.Endpoints;
using MinimalApiAndRespawn.Tests.Unit;
using NSubstitute;
using System.Net;
using Xunit;

namespace MinimalApiAndRespawn.Tests.UnitTests.Featues.Customers.Endpoints;

public class CreateCustomerEndpointFixture : DatabaseFixture
{
    private CreateCustomerEndpoint _createCustomerEndpoint;

    public CreateCustomerEndpointFixture()
    {
        _createCustomerEndpoint = Factory.Create<CreateCustomerEndpoint>(context =>
        {
            var services = new ServiceCollection();
            services.AddSingleton(Substitute.For<LinkGenerator>());
            context.RequestServices = services.BuildServiceProvider();
        },
        DbContextFactory);
    }

    [Fact]
    public async Task CreateCustomerEndpointFixture_HandleAsync_ShouldReturn201()
    {
        // Arrange
        var createCustomerRequest = new CreateCustomerRequest
        {
            Name = "Test",
            LastName = "Test",
        };
        await using AppDbContext dbContext = await DbContextFactory.CreateDbContextAsync();

        // Act
        await _createCustomerEndpoint.HandleAsync(createCustomerRequest);

        // Assert
        _createCustomerEndpoint.HttpContext.Response.StatusCode.Should().Be((int)HttpStatusCode.Created);
        dbContext.Customers.Should().HaveCount(1);
    }
}