using API.Application.Features.Categories.Query.Get;
using API.Application.Features.Categories.Query.GetAll;
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

        [HttpGet]
        public async Task<IActionResult> GetAllCategory()
        {
            var res = await Mediator.Send(new GetAllCategoryQuery());
            return Ok(res);
        }
    }
}
