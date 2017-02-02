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
        private readonly IAddressServices _addressService;
        private readonly ObservableCollection<Book> _books = new ObservableCollection<Book>();

        public BooksService(IHttpHService httpService, IAddressServices addressService)
        {
            _httpService = httpService;
            _addressService = addressService;
        }

        public async Task<IEnumerable<Book>> GetBooksAsync()
        {
            IEnumerable<Book> books = null;
            if (_books == null)
            {
                books = await _httpService.GetItemsAsync<Book>(_addressService.BooksUrl);
            }
            _books.Clear();
            foreach (var book in books)
            {
                _books.Add(book);
            }
            return books;
        }

        public void Dispose()
        {
            _httpService?.Dispose();
        }

        public IEnumerable<Book> Books => _books;
    }
}
