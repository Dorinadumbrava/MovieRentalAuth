using System;

namespace MovieRental.API.Entities
{
    public class Movie
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string FileName { get; set; }

        public string OwnerId { get; set; }

        public string Review { get; set; }

        public void UpdateReview(string review)
        {
            if (!string.IsNullOrWhiteSpace(review))
            {
                Review = review;
            }
        }
    }
}