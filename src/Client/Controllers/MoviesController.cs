using AutoMapper;
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
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Movies.Client.Controllers
{
    [Authorize]
    public class MoviesController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IMovieApiService _movieApiService;
        private readonly ICategoryApiService _categoryApiService;

        public MoviesController(
            IMapper mapper,
            IMovieApiService movieApiService,
            ICategoryApiService categoryApiService)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
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
            var result = await _movieApiService.GetMovies();
            if (!result.Succeeded)
            {
                return RedirectToAction("ErrorHandling", "Home", new { code = result.StatusCode });
            }
            return View(result.DataList);
        }
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await _movieApiService.GetMovie(id);
            if (!result.Succeeded)
            {
                return RedirectToAction("ErrorHandling", "Home", new { code = result.StatusCode });

            }
            return View(result.Data);
        }

        public async Task<IActionResult> Create()
        {
            var vm = new CreateMovieModel();
            vm.Categories = await _categoryApiService.ListCategory();
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateMovieModel movie)
        {
            if (ModelState.IsValid)
            {
                 var result = await _movieApiService.CreateMovie(movie);
                if (!result.Succeeded)
                {
                    if (result.StatusCode == 401)
                    {
                        return RedirectToAction("ErrorHandling", "Home", new { code = result.StatusCode });
                    }
                    foreach (var error in result.ErrorMessages)
                    {
                        ModelState.AddModelError(string.Empty, error); ;
                    }
                    movie.Categories = await _categoryApiService.ListCategory();
                    return View(movie);
                }
            }
            return RedirectToAction(nameof(Index));

        }

        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(Guid? id, bool isSuccess = false)
        {
            ViewBag.isSuccess = isSuccess;
            if (id == null)
            {
                return NotFound();
            }

            var result = await _movieApiService.GetMovie(id);
            if (result.Data == null)
            {
                return NotFound();
            }
            var vm = _mapper.Map<UpdateMovieModel>(result.Data);
            vm.Categories = await _categoryApiService.ListCategory();

            return View(vm);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, UpdateMovieModel movie)
        {
            if (id != movie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var result = await _movieApiService.UpdateMovie(movie);
                if (!result.Succeeded)
                {
                    if (result.StatusCode == 401)
                    {
                        return RedirectToAction("ErrorHandling", "Home", new { code = result.StatusCode });
                    }
                    foreach (var error in result.ErrorMessages)
                    {
                        ModelState.AddModelError(string.Empty, error); ;
                    }
                    movie.Categories = await _categoryApiService.ListCategory();
                    return View(movie);
                }
                return RedirectToAction(nameof(Edit), new { id = movie.Id, isSuccess = true });
            }
            return View(movie);
        }

        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result =  await _movieApiService.GetMovie(id);
            if (!result.Succeeded)
            {
                return RedirectToAction("ErrorHandling", "Home", new { code = result.StatusCode });
            }
            return View(result.Data);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var result = await _movieApiService.DeleteMovie(id);
            if (!result.Succeeded)
            {
                return RedirectToAction("ErrorHandling", "Home", new { code = result.StatusCode });
            }
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
