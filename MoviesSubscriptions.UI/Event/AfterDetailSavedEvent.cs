using Prism.Events;

namespace MoviesSubscriptions.UI.Event
{
  public class AfterDetailSavedEvent : PubSubEvent<AfterDetailSavedEventArgs>
  {
  }

  public class AfterDetailSavedEventArgs
  {
    public int Id { get; set; }
  
  }
}
