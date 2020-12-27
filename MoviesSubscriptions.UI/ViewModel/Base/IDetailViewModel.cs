using System.Threading.Tasks;

namespace MoviesSubscriptions.UI.ViewModel.Base
{
  public interface IDetailViewModel
  {
    Task LoadAsync(int id);
    bool HasChanges { get; }
    int Id { get; }
  }
}