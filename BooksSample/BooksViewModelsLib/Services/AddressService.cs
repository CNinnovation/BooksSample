namespace BooksViewModels.Services
{
    public class AddressService : IAddressService
    {
        public string BaseUrl { get; } = "http://localhost:18013/api/";
        public string BooksUrl => BaseUrl + "Books";
    }
}
