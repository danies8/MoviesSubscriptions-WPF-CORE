using MoviesSubscriptions.Model;
using MoviesSubscriptions.UI.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoviesSubscriptions.UI.Wrapper
{
    public class UserWrapper : ModelWrapper<User>
    {
        public UserWrapper(User model) : base(model)
        {
        }

        public int Id { get { return Model.Id; } }

        public string FirstName
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public string LastName
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public DateTime CreatedDate
        {
            get { return GetValue<DateTime>(); }
            set { SetValue(value); }
        }

        public int SessionTimeOut
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }

                
        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
            set { SetValue(value); }
        }

        public string UserName
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
        

        public string Permissions
        {
            get
            {
                return Helpers.Helper.PermissionsBuilder(this);
            }
            set { SetValue(value); }
        }

        public bool ViewSubscriptions
        {
            get { return GetValue<bool>(); }
            set { SetValue(value); }
        }

        public bool CreateSubscriptions
        {
            get { return GetValue<bool>(); }
            set
            {
                SetValue(value);
                if (CreateSubscriptions)
                {
                    ViewSubscriptions = true;
                }
            }
        }

        public bool UpdateSubscriptions
        {
            get { return GetValue<bool>(); }
            set
            {
                SetValue(value);
                if (UpdateSubscriptions)
                {
                    ViewSubscriptions = true;
                }
            }
        }
        public bool DeleteSubscriptions
        {
            get { return GetValue<bool>(); }
            set
            {
                SetValue(value);
                if (DeleteSubscriptions)
                {
                    ViewSubscriptions = true;
                }
            }
        }

        public bool ViewMovies
        {
            get { return GetValue<bool>(); }
            set { SetValue(value); }
        }

        public bool CreateMovies
        {
            get { return GetValue<bool>(); }
            set
            {
                SetValue(value);
                if (CreateMovies)
                {
                    ViewMovies = true;
                }
            }
        }

        public bool UpdateMovies
        {
            get { return GetValue<bool>(); }
            set
            {
                SetValue(value);
                if (UpdateMovies)
                {
                    ViewMovies = true;
                }
            }
        }
        public bool DeleteMovies
        {
            get { return GetValue<bool>(); }
            set
            {
                SetValue(value);
                if (DeleteMovies)
                {
                    ViewMovies = true;
                }
            }
        }
        protected override IEnumerable<string> ValidateProperty(string propertyName)
        {
           return null;
        }
    }

  

}
