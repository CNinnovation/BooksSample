using BooksViewModels.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace BooksViewModels.Services
{
    public class BooksService : IBooksService, IDisposable
    {
        private readonly IHttpHService  _httpService;
        private readonly IAddressService _addressService;
        private readonly ObservableCollection<Book> _books = new ObservableCollection<Book>();

        public BooksService(IHttpHService httpService, IAddressService addressService)
        {
            _httpService = httpService;
            _addressService = addressService;
        }

        public IEnumerable<Book> Books => _books;

        public async Task<IEnumerable<Book>> GetBooksAsync()
        {
            IEnumerable<Book> books = await _httpService.GetItemsAsync<Book>(_addressService.BooksUrl);
            _books.Clear();
            foreach (var book in books)
            {
                _books.Add(book);
            }
            return books;
        }

        public async Task<Book> AddBookAsync(Book book)
        {
            Book addedBook = await _httpService.AddItem(_addressService.BooksUrl, book);
            return addedBook;
        }

        public async Task<Book> UpdateBookAsync(Book book)
        {
            Book updatedBook = await _httpService.UpdateItem(_addressService.BooksUrl, book);
            return updatedBook;
        }
 
        public void Dispose()
        {
            _httpService?.Dispose();
        }
    }
}
