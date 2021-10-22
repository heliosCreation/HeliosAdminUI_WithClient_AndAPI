﻿using API.Application.Response;
using FluentValidation;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace API.Application.Behaviour
{
    public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
            where TResponse : ApiResponse<TResponse>, new()
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);

                var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
                var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();


                if (failures.Count != 0)
                {

                    return new TResponse
                    {
                        Succeeded = false,
                        StatusCode = (int)HttpStatusCode.BadRequest,
                        ErrorMessages = failures.Select(f => f.ErrorMessage).ToList()
                    };
                }
            }
            return await next();

        }
    }
}
