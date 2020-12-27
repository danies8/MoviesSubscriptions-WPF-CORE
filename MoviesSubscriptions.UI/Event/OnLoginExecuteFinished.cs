using Prism.Events;

namespace MoviesSubscriptions.UI.Event
{
    public class AfterLoginExecuteEvent : PubSubEvent<AfterLoginExecuteEventArgs>
    {
    }

    public class AfterLoginExecuteEventArgs
    {
        public bool IsUserExists { get; set; }
    }
}
