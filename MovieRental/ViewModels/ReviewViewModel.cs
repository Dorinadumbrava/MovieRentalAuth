using System;
using System.ComponentModel.DataAnnotations;

namespace MovieRental.Client.ViewModels
{
    public class ReviewViewModel
    {
        [Required]
        public string ReviewText { get; set; }
        [Required]
        public Guid Id { get; set; }

        public string MovieName { get; set; }
    }
}