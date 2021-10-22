using API.Application.Response;
using System.Collections.Generic;

namespace API.Application.Features.Movies.Query.GetAll
{
    public class GetAllMovieQueryResponse
    {
        public List<MovieVm> DataList { get; set; }
    }
}
