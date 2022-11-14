using FastEndpoints;
using FluentValidation;
using MinimalApiAndRespawn.Features.Customers.Contracts.Requests;

namespace MinimalApiAndRespawn.Features.Customers.Validators
{
    public class CreateCustomerRequestValidator : Validator<CreateCustomerRequest>
    {
        public CreateCustomerRequestValidator()
        {
            RuleFor(createCustomerRequest => createCustomerRequest.Name)
                .NotEmpty()
                .WithMessage("Name is required!");

            RuleFor(createCustomerRequest => createCustomerRequest.LastName)
                .NotEmpty()
                .WithMessage("LastName is required!");
        }
    }
}
