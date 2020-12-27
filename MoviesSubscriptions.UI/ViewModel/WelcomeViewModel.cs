using MoviesSubscriptions.UI.Helpers;
using MoviesSubscriptions.UI.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MoviesSubscriptions.UI.ViewModel
{
    public class WelcomeViewModel : TabViewModelBase
    {
        public WelcomeViewModel()
        {

        }

        public override string Title => Constants.Welcome;

        public override Task LoadAsync()
        {
            return Task.Delay(100);
        }
    }
}
