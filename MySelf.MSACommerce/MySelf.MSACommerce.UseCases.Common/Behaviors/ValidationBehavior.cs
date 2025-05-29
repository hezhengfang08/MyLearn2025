

using FluentValidation;
using MediatR;
using System.ComponentModel.DataAnnotations;
using ValidationException= MySelf.MSACommerce.UseCases.Common.Exceptions.ValidationException;
namespace MySelf.MSACommerce.UseCases.Common.Behaviors
{
    public class ValidationBehavior<TRequest, TReponse>(IEnumerable<IValidator<TRequest>> validators)
        : IPipelineBehavior<TRequest, TReponse> where TRequest : notnull
    {
        public async Task<TReponse> Handle(TRequest request, RequestHandlerDelegate<TReponse> next, CancellationToken cancellationToken)
        {
            if (validators.Any())
            {
                var context = new ValidationContext<TRequest> (request);
                var validationResults = await Task.WhenAll(
                    validators.Select(validator => validator.ValidateAsync(context, cancellationToken)));
                var failures = validationResults.Where(result => result.Errors.Count() != 0)
                    .SelectMany(result => result.Errors).ToList();
                if (failures.Any()) {
                    throw new ValidationException(failures);
                }
            }
            return await next();
        }
    }
}
