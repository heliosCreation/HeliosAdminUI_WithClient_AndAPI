using System;

namespace API.Application.Contracts.Identity
{
    public interface ILoggedInUserService
    {
        public Guid UserId { get;}
    }
}
