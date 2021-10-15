using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Movies.Client.ApiService.Categories;
using Movies.Client.ApiService.Movies;
using Movies.Client.Models;
using Movies.Client.Models.Movies;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Client.Controllers
{
    [Authorize]
    public class MoviesController : Controller
    {
        private readonly IMovieApiService _movieApiService;
        private readonly ICategoryApiService _categoryApiService;

        public MoviesController(
            IMovieApiService movieApiService,
            ICategoryApiService categoryApiService)
        {
            _movieApiService = movieApiService ?? throw new ArgumentNullException(nameof(movieApiService));
            _categoryApiService = categoryApiService ?? throw new ArgumentNullException(nameof(categoryApiService));
        }

        [Authorize(Roles = "admin , IsAdmin")]
        public IActionResult AdminOnly()
        {
            var userInfoDictionnary = new Dictionary<string, string>();
            foreach (var claim in User.Claims)
            {
                if (userInfoDictionnary.ContainsKey(claim.Type))
                {
                    userInfoDictionnary[claim.Type] = $"{userInfoDictionnary[claim.Type]} , {claim.Value}";
                }
                else
                {
                    userInfoDictionnary.Add(claim.Type, claim.Value);
                }

            }
            return View(new UserInfoViewModel(userInfoDictionnary));
        }

        public async Task<IActionResult> Index()
        {
            await WriteOutIdentityInformation();
            var movies = await _movieApiService.GetMovies();

            return View(movies);
        }

        // GET: Movies/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _movieApiService.GetMovie(id);
            return View(movie);
        }

        // GET: Movies/Create
        public async Task<IActionResult> Create()
        {
            var vm = new CreateMovieModel();
            vm.Categories = await _categoryApiService.ListCategory();
            return View(vm);
        }

        // POST: Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateMovieModel movie)
        {
            if (ModelState.IsValid)
            {
                await _movieApiService.CreateMovie(movie);
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _movieApiService.GetMovie(id);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Movie movie)
        {
            if (id != movie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _movieApiService.UpdateMovie(movie);
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _movieApiService.GetMovie(id);
            return View(movie);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _movieApiService.DeleteMovie(id);
            return RedirectToAction(nameof(Index));
        }

        //[HttpGet]
        //[Authorize(Policy = "CanOrderMovie")]
        //public IActionResult OrderMovie()
        //{
        //    var address = HttpContext.User?.Claims.FirstOrDefault(c => c.Type == "address")?.Value;
        //    return View(new OrderMovie(address));
        //}

        public async Task WriteOutIdentityInformation()
        {
            //Get the saved identity token
            var identityToken = await HttpContext.GetTokenAsync(OpenIdConnectParameterNames.IdToken);

            //Write it out
            Debug.WriteLine($"Identity token: {identityToken}");

            //Write Claims
            foreach (var claim in User.Claims)
            {
                Debug.WriteLine($"Claim Type: {claim.Type}  -  Claim Value: {claim.Value}");
            }
        }

    }
}
