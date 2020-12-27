using Prism.Events;

namespace MoviesSubscriptions.UI.Event
{
  public class OpenNavigationViewEvent : PubSubEvent<OpenNavigationViewEventArgs>
  {
  }
  public class OpenNavigationViewEventArgs
    {
    public int Id { get; set; }
    public string ViewModelName { get; set; }
    public string SelectedItem { get; set; }
  }
}
