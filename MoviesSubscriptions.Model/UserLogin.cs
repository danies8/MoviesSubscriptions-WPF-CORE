using System.ComponentModel.DataAnnotations;

namespace MoviesSubscriptions.Model
{
    public class UserLogin
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [EmailAddress]
        public string UserName { get; set; }

        [Required]
        [StringLength(50)]
        public string Password { get; set; }

        public bool IsAdmin { get; set; }
    }
}
