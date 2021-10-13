using API.Application.Contracts.Identity;
using API.Application.Contracts.Persistence;
using API.Application.Exceptions;
using API.Domain.Entities;
using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace API.Application.Features.Movies.Query.Get
{
    public class GetMovieQueryHandler : IRequestHandler<GetMovieQuery, MovieVm>
    {
        private readonly IMapper _mapper;
        private readonly IMovieRepository _movieRepository;
        private readonly ILoggedInUserService _loggedInUserService;

        public GetMovieQueryHandler(
            IMapper mapper,
            IMovieRepository movieRepository,
            ILoggedInUserService loggedInUserService)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _movieRepository = movieRepository ?? throw new ArgumentNullException(nameof(movieRepository));
            _loggedInUserService = loggedInUserService ?? throw new ArgumentNullException(nameof(loggedInUserService));
        }
        public async Task<MovieVm> Handle(GetMovieQuery request, CancellationToken cancellationToken)
        {
            var movie = await _movieRepository.GetByIdAndOwnerId(request.Id, _loggedInUserService.UserId);
            if (movie != null)
            {
                var vm = _mapper.Map<MovieVm>(movie);
                return vm;
            }

            throw new NotFoundException(nameof(Movie), request.Id);
        }
    }
}
