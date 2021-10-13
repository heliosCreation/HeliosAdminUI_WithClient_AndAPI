using System;

namespace Movies.Client.Models
{
    public class ApplicationUserProfileModel
    {
        public Guid Id { get; set; }
        public string Subject { get; set; }
        public string SubscriptionLevel { get; set; }
        public string Role { get; set; }
    }
}
