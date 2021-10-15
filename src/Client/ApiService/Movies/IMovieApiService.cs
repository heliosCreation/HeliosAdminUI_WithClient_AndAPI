using Movies.Client.Models;
using Movies.Client.Models.Movies;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.Client.ApiService.Movies
{
    public interface IMovieApiService
    {
        Task<IEnumerable<Movie>> GetMovies();
        Task<Movie> GetMovie(Guid? id);
        Task CreateMovie(CreateMovieModel movie);
        Task UpdateMovie(Movie movie);
        Task DeleteMovie(Guid id);

        Task<UserInfoViewModel> GetUserInfo();
    }
}
