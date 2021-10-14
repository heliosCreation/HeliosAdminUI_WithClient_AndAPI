using API.Application.Contracts.Identity;
using API.Application.Contracts.Persistence;
using API.Domain.Entities;
using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace API.Application.Features.Movies.Command.Create
{
    public class CreateMovieCommandHandler : IRequestHandler<CreateMovieCommand, CreateMovieCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly ILoggedInUserService _loggedInUserService;
        private readonly IMovieRepository _movieRepository;

        public CreateMovieCommandHandler(
            IMapper mapper,
            ILoggedInUserService loggedInUserService,
            IMovieRepository movieRepository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _loggedInUserService = loggedInUserService ?? throw new ArgumentNullException(nameof(loggedInUserService));
            _movieRepository = movieRepository ?? throw new ArgumentNullException(nameof(movieRepository));
        }
        public async Task<CreateMovieCommandResponse> Handle(CreateMovieCommand request, CancellationToken cancellationToken)
        {
            request.OwnerId = _loggedInUserService.UserId;
            var movieEntity = _mapper.Map<Movie>(request);
            var createdMovie = await _movieRepository.AddAsync(movieEntity);
            var createdMovieDto = _mapper.Map<CreateMovieDto>(createdMovie);
            return new CreateMovieCommandResponse { Createdmovie = createdMovieDto };
        }
    }
}
