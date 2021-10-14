using API.Application.Features.Categories.Query.Get;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace MovieAPI.Controllers
{
    public class CategoryController : ApiController
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(Guid id)
        {
            return Ok(await Mediator.Send(new GetCategoryQuery(id)));
        }
    }
}
