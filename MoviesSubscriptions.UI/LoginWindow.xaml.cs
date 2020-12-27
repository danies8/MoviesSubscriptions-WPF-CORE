using MahApps.Metro.Controls;
using MoviesSubscriptions.UI.ViewModel;

namespace MoviesSubscriptions.UI
{
    public partial class LoginWindow : MetroWindow
    {
         public LoginWindow(LoginsViewModel viewModel)
        {
            InitializeComponent();
             DataContext = viewModel;
          }

          public LoginsViewModel ViewModel => DataContext as LoginsViewModel;

    }
}
