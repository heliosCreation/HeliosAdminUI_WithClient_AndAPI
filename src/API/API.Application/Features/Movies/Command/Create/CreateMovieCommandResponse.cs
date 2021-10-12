using API.Application.Response;

namespace API.Application.Features.Movies.Command.Create
{
    public class CreateMovieCommandResponse : BaseResponse
    {
        public CreateMovieCommandResponse() : base()
        {

        }

        public CreateMovieDto Createdmovie { get; set; }
    }
}
