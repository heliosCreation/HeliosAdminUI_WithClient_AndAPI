using API.Application.Contracts.Identity;
using API.Application.Contracts.Persistence;
using API.Application.Exceptions;
using API.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace API.Application.Features.Movies.Command.Delete
{
    public class DeleteMovieCommandHandler : IRequestHandler<DeleteMovieCommand>
    {
        private readonly IMovieRepository _movieRepository;
        private readonly ILoggedInUserService _loggedInUserService;

        public DeleteMovieCommandHandler(IMovieRepository movieRepository, ILoggedInUserService loggedInUserService)
        {
            _movieRepository = movieRepository ?? throw new ArgumentNullException(nameof(movieRepository));
            _loggedInUserService = loggedInUserService ?? throw new ArgumentNullException(nameof(loggedInUserService));
        }
        public async Task<Unit> Handle(DeleteMovieCommand request, CancellationToken cancellationToken)
        {
            var entity = await _movieRepository.GetByIdAndOwnerId(request.Id, _loggedInUserService.UserId);
            if (entity == null)
            {
                throw new NotFoundException(nameof(Movie), request.Id);
            }

            await _movieRepository.DeleteAsync(entity);
            return Unit.Value;

        }
    }
}
