
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace MoviesSubscriptions.Model
{
    public class Movie
    {
        public Movie()
        {
            Subscriptions = new Collection<Subscription>();
        }
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Genres { get; set; }

        [Required]
        [StringLength(100)]
        [Url]
        public string ImageUrl { get; set; }

        [Required]
        public DateTime Premired { get; set; }

        public ICollection<Subscription> Subscriptions { get; set; }
   
    }
}
