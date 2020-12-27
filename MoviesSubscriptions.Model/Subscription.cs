
using System;

namespace MoviesSubscriptions.Model
{
    public class Subscription
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public Member Member { get; set; }

        public int MovieId { get; set; }
        public Movie Movie { get; set; }
        public DateTime WatchedDate { get; set; }
        
    }
}
