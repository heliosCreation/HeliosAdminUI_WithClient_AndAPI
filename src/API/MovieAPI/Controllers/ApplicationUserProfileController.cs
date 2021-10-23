using API.Application.Features.ApplicationUserProfiles.Query;
using API.Application.Features.ApplicationUserProfiles.Query.Get;
using API.Application.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MovieAPI.Controllers
{
    public class ApplicationUserProfileController : ApiController
    {
        /// <summary>
        /// Get the Application User profile.
        /// </summary>
        /// <param name="subject"></param>
        /// <returns></returns>
        [HttpGet("{subject}")]
        [ProducesResponseType(typeof(ApiResponse<ApplicationUserProfileVm>), StatusCodes.Status200OK)]
        public async Task<IActionResult> ApplicationUserProfile(string subject)
        {
            return Ok(await Mediator.Send(new GetApplicationUserProfileQuery(subject)));
        }
    }
}
