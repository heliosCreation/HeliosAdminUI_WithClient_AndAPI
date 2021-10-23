using API.Application.Features.Categories.Query;
using API.Application.Features.Categories.Query.Get;
using API.Application.Features.Categories.Query.GetAll;
using API.Application.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace MovieAPI.Controllers
{
    public class CategoryController : ApiController
    {
        /// <summary>
        /// Get a given category by id.
        /// </summary>
        /// <param name="id"></param>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponse<CategoryVm>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<CategoryVm>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCategory(Guid id)
        {
            return Ok(await Mediator.Send(new GetCategoryQuery(id)));
        }

        /// <summary>
        /// Get all the categories stored.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<CategoryVm>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllCategory()
        {
            var res = await Mediator.Send(new GetAllCategoryQuery());
            return Ok(res);
        }
    }
}
