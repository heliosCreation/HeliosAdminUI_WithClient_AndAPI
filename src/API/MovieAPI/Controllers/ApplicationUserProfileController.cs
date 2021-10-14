using API.Application.Features.ApplicationUserProfiles.Query.Get;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MovieAPI.Controllers
{
    public class ApplicationUserProfileController : ApiController
    {

        //[Authorize(Policy = "SubjectMustMatchUser")]
        [HttpGet("{subject}")]
        public async Task<IActionResult> ApplicationUserProfile(string subject)
        {
            return Ok(await Mediator.Send(new GetApplicationUserProfileQuery(subject)));
        }
    }
}
