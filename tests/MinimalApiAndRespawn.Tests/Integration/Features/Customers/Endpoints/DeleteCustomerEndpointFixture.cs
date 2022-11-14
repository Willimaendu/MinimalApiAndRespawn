//using FluentAssertions;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.DependencyInjection;
//using MinimalApiAndRespawn.Core.Persistence;
//using MinimalApiAndRespawn.Core.Persistence.Entities;
//using MinimalApiAndRespawn.Features.Customers.Contracts.Requests;
//using MinimalApiAndRespawn.Features.Customers.Contracts.Responses;
//using System.Net;
//using System.Net.Http.Json;
//using Xunit;

//namespace MinimalApiAndRespawn.Tests.Integration.Features.Customers.Endpoints
//{
//    [Collection("TestCollection")]
//    public class DeleteCustomerEndpointFixture : IAsyncLifetime
//    {
//        private readonly HttpClient _httpClient;

//        private readonly Func<Task> _resetDatabase;

//        private readonly IServiceProvider _services;

//        public DeleteCustomerEndpointFixture(ApiFactory apiFactory)
//        {
//            _httpClient = apiFactory.HttpClient;
//            _resetDatabase = apiFactory.ResetDatabaseAsync;
//            _services = apiFactory.Services;
//        }

//        [Fact]
//        public async Task Delete_ShouldDeleteCustomer_WhenInputDataIsValid()
//        {
//            // Arrange
//            var dbContextFactory = _services.GetRequiredService<IDbContextFactory<AppDbContext>>();
//            var appDbContext = await dbContextFactory.CreateDbContextAsync();
//            var customerToDelete = new Customer
//            {
//                LastName = "Delete",
//                Name = "Delete"
//            };
//            appDbContext.Customers.Add(customerToDelete);
//            await appDbContext.SaveChangesAsync();

//            var deleteCustomerRequest = new DeleteCustomerRequest { Id = customerToDelete.Id };

//            // Act
//            _httpClient.DeleteAsJsonAsync(_httpClient, deleteCustomerRequest);
//            var response = await _httpClient.DeleteFromJsonAsync("api/customers/create",  deleteCustomerRequest);

//            // Assert
//            var customerResponse = await response.Content.ReadFromJsonAsync<CustomerResponse>();
//            customerResponse.Should().BeEquivalentTo(createCustomerRequest);
//            response.StatusCode.Should().Be(HttpStatusCode.Created);
//            response.Headers.Location!.ToString().Should()
//                .Be($"http://localhost/api/customers/{customerResponse!.Id}");
//        }

//        [Fact]
//        public async Task Create_ShouldReturnBadRequest_WhenInputIsNotValid()
//        {
//            // Arrange
//            var createCustomerRequest = new CreateCustomerRequest
//            {
//            };

//            var dbContextFactory = _services.GetRequiredService<IDbContextFactory<AppDbContext>>();
//            var appDbContext = await dbContextFactory.CreateDbContextAsync();

//            // Act
//            var response = await _httpClient.PostAsJsonAsync("api/customers/create", createCustomerRequest);

//            // Assert
//            var responseContent = await response.Content.ReadAsStringAsync();
//            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
//            responseContent.Should().Contain("Name is required!");
//            responseContent.Should().Contain("LastName is required!");
//            appDbContext.Customers.Should().HaveCount(0);
//        }


//        public Task DisposeAsync() => _resetDatabase();

//        public Task InitializeAsync() => Task.CompletedTask;
//    }
//}
