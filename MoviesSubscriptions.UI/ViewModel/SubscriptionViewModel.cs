using System.Windows.Input;
using System;
using MoviesSubscriptions.Model;

namespace MoviesSubscriptions.UI.ViewModell
{
    public class SubscriptionViewModel
    {
        public int Id { get; set; }
        public Movie Movie { get; set; }
        public Member Member { get; set; }

        public DateTime WatchedDate { get; set; }
        public ICommand SelectedMovieDetailsCommand { get; set; }

        public ICommand SelectedMemberDetailsCommand { get; set; }
    }
}
