using Movies.Client.Models;
using Movies.Client.Models.Movies;
using System;
using System.Threading.Tasks;

namespace Movies.Client.ApiService.Movies
{
    public interface IMovieApiService
    {
        Task<BaseResponse<Movie>> GetMovies();
        Task<BaseResponse<Movie>> GetMovie(Guid? id);
        Task<BaseResponse<Movie>> CreateMovie(CreateMovieModel movie);
        Task<BaseResponse<Movie>> UpdateMovie(UpdateMovieModel movie);
        Task<BaseResponse<Movie>> DeleteMovie(Guid id);

        Task<UserInfoViewModel> GetUserInfo();
    }
}
