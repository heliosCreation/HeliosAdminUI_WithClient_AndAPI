using System.Collections.Generic;

namespace Movies.Client.Models
{
    public class UserInfoViewModel
    {
        public Dictionary<string, string> UserInfoDictionnary { get; private set; } = null;

        public UserInfoViewModel(Dictionary<string, string> userInfo)
        {
            UserInfoDictionnary = userInfo;
        }
    }
}
