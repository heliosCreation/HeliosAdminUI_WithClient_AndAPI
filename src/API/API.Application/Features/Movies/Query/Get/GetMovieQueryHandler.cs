using API.Application.Contracts.Identity;
using API.Application.Contracts.Persistence;
using API.Application.Exceptions;
using API.Application.Response;
using API.Domain.Entities;
using AutoMapper;
using MediatR;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace API.Application.Features.Movies.Query.Get
{
    public class GetMovieQueryHandler : IRequestHandler<GetMovieQuery, ApiResponse<MovieVm>>
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
        public async Task<ApiResponse<MovieVm>> Handle(GetMovieQuery request, CancellationToken cancellationToken)
        {
            var response = new ApiResponse<MovieVm>();
            var movie = await _movieRepository.GetByIdAndOwnerId(request.Id, _loggedInUserService.UserId);
            if (movie != null)
            {
                var vm = _mapper.Map<MovieVm>(movie);
                response.Data = vm;
                return response;
            }

            response.Succeeded = false;
            response.StatusCode = (int)HttpStatusCode.NotFound;
            return response;
        }
    }
}
