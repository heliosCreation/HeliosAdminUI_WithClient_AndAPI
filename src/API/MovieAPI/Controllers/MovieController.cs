using API.Application.Features.Movies.Query.Get;
using API.Application.Features.Movies.Query.GetAll;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace MovieAPI.Controllers
{
    public class MovieController : ApiController
    {
        [HttpGet("Get")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await Mediator.Send(new GetMovieQuery(id)));
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await Mediator.Send(new GetMovieListQuery()));
        }
    }
}
