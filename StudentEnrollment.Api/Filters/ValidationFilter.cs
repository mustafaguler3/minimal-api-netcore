using FluentValidation;
using StudentEnrollment.Api.Dtos;
using StudentEnrollment.Api.Validators;

namespace StudentEnrollment.Api.Filters
{
    public class ValidationFilter<T> : IEndpointFilter where T : class
    {
        private readonly IValidator<T> _validator;

        public ValidationFilter(IValidator<T> validator)
        {
            _validator = validator;
        }

        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            //runs before endpoint code
            var objectToValidate = context.GetArgument<T>(0);//parametrade 0. yerde olduğu için 
            var validationResults = await _validator.ValidateAsync(objectToValidate);

            if (!validationResults.IsValid)
            {
                return Results.BadRequest(validationResults.ToDictionary());
            }
            var results = await next(context);
            //do something after endpoint code
            return results;
        }
    }
}
