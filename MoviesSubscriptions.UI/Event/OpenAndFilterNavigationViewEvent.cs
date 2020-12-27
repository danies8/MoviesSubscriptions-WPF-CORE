using Prism.Events;

namespace MoviesSubscriptions.UI.Event
{
  public class OpenAndFilterNavigationViewEvent : PubSubEvent<OpenAndFilterNavigationViewEventArgs>
  {
  }
  public class OpenAndFilterNavigationViewEventArgs
    {
    public string ViewModelName { get; set; }
    public string SelectedItem { get; set; }
  }
}
