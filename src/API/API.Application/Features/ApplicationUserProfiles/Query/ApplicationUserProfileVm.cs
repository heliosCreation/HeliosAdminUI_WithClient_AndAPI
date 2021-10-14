using System;

namespace API.Application.Features.ApplicationUserProfiles.Query
{
    public class ApplicationUserProfileVm
    {
        public Guid Id { get; set; }
        public string Subject { get; set; }
        public string SubscriptionLevel { get; set; }
        public string Role { get; set; }
    }
}
