using MoviesSubscriptions.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace MoviesSubscriptions.UI.Wrapper
{
    public class MovieWrapper : ModelWrapper<Movie>
    {
        public MovieWrapper(Movie model) : base(model)
        {
        }

        public int Id { get { return Model.Id; } }

        public string Name
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public string Genres
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public string ImageUrl
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public DateTime Premired
        {
            get { return GetValue<DateTime>(); }
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
