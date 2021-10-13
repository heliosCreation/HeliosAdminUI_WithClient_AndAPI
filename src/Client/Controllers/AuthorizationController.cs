using Microsoft.AspNetCore.Mvc;

namespace Movies.Client.Controllers
{
    public class AuthorizationController : Controller
    {
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
