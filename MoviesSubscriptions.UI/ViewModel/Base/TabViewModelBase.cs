using System;
using System.Threading.Tasks;

namespace MoviesSubscriptions.UI.ViewModel.Base
{
  public abstract class TabViewModelBase : ViewModelBase 
    {
       public abstract string Title { get; }

       public abstract Task LoadAsync();
              
    }
}
