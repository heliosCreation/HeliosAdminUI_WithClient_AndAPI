using API.Application.Contracts.Identity;
using API.Application.Contracts.Persistence;
using API.Application.Exceptions;
using API.Domain.Entities;
using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace API.Application.Features.Movies.Command.Update
{
    public class UpdateMovieCommandHandler : IRequestHandler<UpdateMovieCommand>
    {
        private readonly IMapper _mapper;
        private readonly IMovieRepository _movieRepository;
        private readonly ILoggedInUserService _loggedInUserService;

        public UpdateMovieCommandHandler(
            IMapper mapper,
            IMovieRepository movieRepository,
            ILoggedInUserService loggedInUserService)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _movieRepository = movieRepository ?? throw new ArgumentNullException(nameof(movieRepository));
            _loggedInUserService = loggedInUserService ?? throw new ArgumentNullException(nameof(loggedInUserService));
        }
        public async Task<Unit> Handle(UpdateMovieCommand request, CancellationToken cancellationToken)
        {
            var entity = await _movieRepository.GetByIdAndOwnerId(request.Id, _loggedInUserService.UserId);
            if (entity == null)
            {
                throw new NotFoundException(nameof(Movie), request.Id);
            }

            _mapper.Map(request, entity, typeof(UpdateMovieCommand), typeof(Movie));
            await _movieRepository.UpdateAsync(entity);
            return Unit.Value;
        }
    }
}
