namespace MovieRental.Client.ViewModels
{
    public class BuyMerchViewModel
    {
        public string Country { get; private set; } = string.Empty;

        public BuyMerchViewModel(string country)
        {
            Country = country;
        }
    }
}