﻿using API.Application.Contracts.Identity;
using API.Application.Features.Movies.Command.Create;
using API.Application.Features.Movies.Command.Delete;
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

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await Mediator.Send(new GetMovieQuery(id)));
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await Mediator.Send(new GetMovieListQuery()));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateMovieCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(new DeleteMovieCommand(id));
        }
    }
}
