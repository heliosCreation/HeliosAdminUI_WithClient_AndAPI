using API.Application.Contracts.Identity;
using API.Application.Contracts.Persistence;
using API.Application.Response;
using API.Domain.Entities;
using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace API.Application.Features.Movies.Command.Create
{
    public class CreateMovieCommandHandler : IRequestHandler<CreateMovieCommand, ApiResponse<CreateMovieDto>>, IValidatable
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
        public async Task<ApiResponse<CreateMovieDto>> Handle(CreateMovieCommand request, CancellationToken cancellationToken)
        {
            var response = new ApiResponse<CreateMovieDto>();
            request.OwnerId = _loggedInUserService.UserId;
            var movieEntity = _mapper.Map<Movie>(request);
            var createdMovie = await _movieRepository.AddAsync(movieEntity);
            response.Data = _mapper.Map<CreateMovieDto>(createdMovie);
            return response;
        }
    }
}
