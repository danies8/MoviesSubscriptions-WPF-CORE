using MoviesSubscriptions.Model;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace MoviesSubscriptions.UI.Wrapper
{
    public class SubscriptionWrapper : ModelWrapper<Subscription>
    {
        public SubscriptionWrapper(Subscription model) : base(model)
        {
        }

        public int MemberId
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }

        public Member Member
        {
            get { return GetValue<Member>(); }
            set { SetValue(value); }
        }

        public int MovieId
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }
        public Movie Movie
        {
            get { return GetValue<Movie>(); }
            set { SetValue(value); }
        }

        public DateTime WatchedDate
        {
            get { return GetValue<DateTime>(); }
            set { SetValue(value); }
        }
   
        protected override IEnumerable<string> ValidateProperty(string propertyName)
        {
            return null;
        }
    }
}
