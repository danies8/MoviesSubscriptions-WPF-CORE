
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Windows.Input;

namespace MoviesSubscriptions.Model
{
    public class Member
    {
        public Member()
        {
            Subscriptions = new Collection<Subscription>();
         }

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(50)]
     
        public string City { get; set; }

        public ICollection<Subscription> Subscriptions { get; set; }

     
    }
}
