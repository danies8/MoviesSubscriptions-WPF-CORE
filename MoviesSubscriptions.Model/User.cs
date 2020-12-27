using System;
using System.ComponentModel.DataAnnotations;

namespace MoviesSubscriptions.Model
{
    public class User
    {

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public int SessionTimeOut { get; set; }

        [Required]
        [StringLength(50)]
        [EmailAddress]
        public string UserName { get; set; }

        public bool ViewSubscriptions { get; set; }
        public bool CreateSubscriptions { get; set; }
        public bool UpdateSubscriptions { get; set; }
        public bool DeleteSubscriptions { get; set; }
        public bool ViewMovies { get; set; }
        public bool CreateMovies { get; set; }
        public bool UpdateMovies { get; set; }
        public bool DeleteMovies { get; set; }
    }
}
