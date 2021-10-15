using API.Application.Contracts.Identity;
using API.Application.Features.Movies.Query.Get;
using API.Application.Features.Movies.Query.GetAll;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace MovieAPI.Controllers
{
    public class MovieController : ApiController
    {
        private readonly ILoggedInUserService _loggedInUserService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MovieController(ILoggedInUserService loggedInUserService, IHttpContextAccessor httpContextAccessor)
        {
            _loggedInUserService = loggedInUserService ?? throw new ArgumentNullException(nameof(loggedInUserService));
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var sub = _loggedInUserService.UserId;
            return Ok(await Mediator.Send(new GetMovieQuery(id)));
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await Mediator.Send(new GetMovieListQuery()));
        }
    }
}
