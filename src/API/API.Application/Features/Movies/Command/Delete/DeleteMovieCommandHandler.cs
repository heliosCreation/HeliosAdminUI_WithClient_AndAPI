using API.Application.Contracts.Identity;
using API.Application.Contracts.Persistence;
using API.Application.Response;
using MediatR;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace API.Application.Features.Movies.Command.Delete
{
    public class DeleteMovieCommandHandler : IRequestHandler<DeleteMovieCommand, ApiResponse<object>>
    {
        private readonly IMovieRepository _movieRepository;
        private readonly ILoggedInUserService _loggedInUserService;

        public DeleteMovieCommandHandler(IMovieRepository movieRepository, ILoggedInUserService loggedInUserService)
        {
            _movieRepository = movieRepository ?? throw new ArgumentNullException(nameof(movieRepository));
            _loggedInUserService = loggedInUserService ?? throw new ArgumentNullException(nameof(loggedInUserService));
        }
        public async Task<ApiResponse<object>> Handle(DeleteMovieCommand request, CancellationToken cancellationToken)
        {
            var response = new ApiResponse<object>();
            var entity = await _movieRepository.GetByIdAndOwnerId(request.Id, _loggedInUserService.UserId);
            if (entity == null)
            {
                response.Succeeded = false;
                response.StatusCode = (int)HttpStatusCode.NotFound;
            }

            await _movieRepository.DeleteAsync(entity);
            return response;

        }
    }
}
