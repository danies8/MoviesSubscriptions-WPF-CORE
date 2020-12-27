using MoviesSubscriptions.Model;

namespace MoviesSubscriptions.UI.Helpers
{
    public class LoginUserInfo
    {
        private static LoginUserInfo _instance;

        private LoginUserInfo()
        {

        }

        public static LoginUserInfo Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new LoginUserInfo();
                }
                return _instance;
            }
        }

        public int LoginId { get; internal set; }
        public  User User { get; set; }
        public bool IsAdmin { get; internal set; }
        public UserLogin Login { get; internal set; }
        public static  bool CreateSubscriptions { get; set; }
        public static bool UpdateSubscriptions { get; set; }
        public static bool DeleteSubscriptions { get; set; }
        public static bool CreateMovies { get; set; }
        public static bool UpdateMovies { get; set; }
        public static bool DeleteMovies { get; set; }
    }
}
