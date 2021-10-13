﻿using MediatR;
using System.Collections.Generic;

namespace API.Application.Features.Movies.Query.GetAll
{
    public class GetMovieListQuery : IRequest<List<MovieVm>>
    {
    }
}
