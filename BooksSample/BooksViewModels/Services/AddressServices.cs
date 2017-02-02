namespace BooksViewModels.Services
{
    public class AddressServices : IAddressServices
    {
        public string BaseUrl { get; } = "http://localhost:11724/api/";
        public string BooksUrl => BaseUrl + "Books";
    }
}
