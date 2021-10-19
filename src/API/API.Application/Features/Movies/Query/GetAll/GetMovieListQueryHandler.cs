using API.Application.Contracts.Identity;
using API.Application.Contracts.Persistence;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace API.Application.Features.Movies.Query.GetAll
{
    public class GetMovieListQueryHandler : IRequestHandler<GetMovieListQuery, List<MovieVm>>
    {
        private readonly ILoggedInUserService _loggedInUserService;
        private readonly IMapper _mapper;
        private readonly IMovieRepository _movieRepository;

        public GetMovieListQueryHandler(ILoggedInUserService loggedInUserService,IMapper mapper, IMovieRepository movieRepository)
        {
            _loggedInUserService = loggedInUserService ?? throw new ArgumentNullException(nameof(loggedInUserService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _movieRepository = movieRepository ?? throw new ArgumentNullException(nameof(movieRepository));
        }
        public async Task<List<MovieVm>> Handle(GetMovieListQuery request, CancellationToken cancellationToken)
        {
            //var entitiesBis = await _movieRepository.GetAllTest();
            var entities = await _movieRepository.GetByOwnerId(_loggedInUserService.UserId);
            var result = _mapper.Map<List<MovieVm>>(entities);

            return result;
        }
    }
}
