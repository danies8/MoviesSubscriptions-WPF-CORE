using MoviesSubscriptions.UI.Wrapper;
using System.Text;

namespace MoviesSubscriptions.UI.Helpers
{
    public static class Helper
    {
        internal static string PermissionsBuilder(UserWrapper userWrapper)
        {
            StringBuilder sb = new StringBuilder();

            if (userWrapper.ViewSubscriptions)
            {
                sb.AppendLine(Constants.ViewSubscriptions);
            }
            if (userWrapper.CreateSubscriptions)
            {
                sb.AppendLine(Constants.CreateSubscriptions);
            }
            if (userWrapper.UpdateSubscriptions)
            {
                sb.AppendLine(Constants.UpdateSubscriptions);
            }
            if (userWrapper.DeleteSubscriptions)
            {
                sb.AppendLine(Constants.DeleteSubscriptions);
            }
            if (userWrapper.ViewMovies)
            {
                sb.AppendLine(Constants.ViewMovies);
            }
            if (userWrapper.CreateMovies)
            {
                sb.AppendLine(Constants.CreateMovies);
            }
            if (userWrapper.UpdateMovies)
            {
                sb.AppendLine(Constants.UpdateMovies);
            }
            if (userWrapper.DeleteMovies)
            {
                sb.AppendLine(Constants.DeleteMovies);
            }

            return sb.ToString();
        }

      
    }
}
