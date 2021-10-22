using System;

namespace Movies.Client.Models.ApplicationUserProfiles
{
    public class ApplicationUserProfile
    {
        public Guid Id { get; set; }
        public string Subject { get; set; }
        public string SubscriptionLevel { get; set; }
        public string Role { get; set; }
    }
}
