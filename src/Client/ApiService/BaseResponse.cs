﻿using System.Collections.Generic;

namespace Movies.Client.ApiService
{
    public class BaseResponse<T> where T : class
    {
        public bool Succeeded { get; set; }

        public int StatusCode { get; set; }

        public T Data { get; set; }

        public IEnumerable<T> DataList { get; set; }
    }
}
