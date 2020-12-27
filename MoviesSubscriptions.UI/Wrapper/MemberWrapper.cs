using MoviesSubscriptions.Model;
using MoviesSubscriptions.UI.ViewModell;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace MoviesSubscriptions.UI.Wrapper
{
    public class MemberWrapper : ModelWrapper<Member>
    {
        public MemberWrapper(Member model) : base(model)
        {
        }

        public int Id { get { return Model.Id; } }

        public string Name
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public string Email
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public string City
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public ICollection<Subscription> Subscriptions
        {
            get { return GetValue<ICollection<Subscription>>(); }
            set { SetValue(value); }
        }

        protected override IEnumerable<string> ValidateProperty(string propertyName)
        {
            return null;
        }
    }
}
