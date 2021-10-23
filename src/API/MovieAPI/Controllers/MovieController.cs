using API.Application.Features.Movies.Command.Create;
using API.Application.Features.Movies.Command.Delete;
using API.Application.Features.Movies.Command.Update;
using API.Application.Features.Movies.Query;
using API.Application.Features.Movies.Query.Get;
using API.Application.Features.Movies.Query.GetAll;
using API.Application.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace MovieAPI.Controllers
{
    public class MovieController : ApiController
    {
        /// <summary>
        /// Get movie by specified id. The movie properties will be check against the userId.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponse<MovieVm>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await Mediator.Send(new GetMovieQuery(id)));
        }


        /// <summary>
        /// Get the List of movie owned by the user sending the request.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<MovieVm>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<CreateMovieDto>), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAll()
        {
            var res = await Mediator.Send(new GetMovieListQuery());
            return Ok(res);
        }


        /// <summary>
        /// Creates a movie associated with the user.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<CreateMovieDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<CreateMovieDto>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<CreateMovieDto>), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Post([FromBody] CreateMovieCommand command)
        {
            var res = await Mediator.Send(command);
            return Ok(res);
        }

        /// <summary>
        /// Update a movie associated with the user.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ApiResponse<CreateMovieDto>), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiResponse<CreateMovieDto>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<CreateMovieDto>), StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> Update(Guid id, UpdateMovieCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            await Mediator.Send(command);

            return NoContent();
        }


        /// <summary>
        /// Delete a movie associated with the user.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiResponse<CreateMovieDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<CreateMovieDto>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<CreateMovieDto>), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await Mediator.Send(new DeleteMovieCommand(id)));
        }
    }
}
