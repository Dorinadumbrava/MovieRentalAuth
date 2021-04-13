using System;
using System.Collections.Generic;
using System.Text;

namespace MovieRental.Models
{
    public class Movie
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string FileName { get; set; }
        public string Review { get; set; }
    }
}
