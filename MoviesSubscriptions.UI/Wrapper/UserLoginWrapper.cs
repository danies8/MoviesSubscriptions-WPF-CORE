using MoviesSubscriptions.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoviesSubscriptions.UI.Wrapper
{
    public class UserLoginWrapper : ModelWrapper<UserLogin>
    {
        public UserLoginWrapper(UserLogin model) : base(model)
        {
        }

        public int Id { get { return Model.Id; } }

        public string UserName
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public string Password
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

      
        protected override IEnumerable<string> ValidateProperty(string propertyName)
        {
            return null;
        }
    }
}
