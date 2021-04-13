namespace MovieRental.Client.ViewModels
{
    public class RentMovieViewModel
    {
        public string Address { get; private set; } = string.Empty;

        public RentMovieViewModel(string address)
        {
            Address = address;
        }
    }
}