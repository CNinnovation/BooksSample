using BooksViewModels.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksViewModels.Services
{
    public class BooksSampleService : IBooksService, IDisposable
    {
        private readonly ObservableCollection<Book> _books = new ObservableCollection<Book>()
        {
            new Book { Id = 1, Title = "Professional C# 6 and .NET Core 1.0", Publisher = "Wrox Press", Isbn = "1234"},
            new Book { Id = 2, Title = "Enterprise Services with the .NET Framework", Publisher = "Addison Wesley", Isbn = "5678"},
        };

        public BooksSampleService()
        {
        }

        public IEnumerable<Book> Books => _books;

        public Task<IEnumerable<Book>> GetBooksAsync()
        {
            return Task.FromResult<IEnumerable<Book>>(_books);
        }

        public Book PrepareAddBook()
        {
            Book newBook = Book.New;
            _books.Add(newBook);
            return newBook;
        }

        public void CancelAddBook() => RemoveNullBooks();

        private void RemoveNullBooks()
        {
            var booksToRemove = _books.Where(b => b.Id == 0).ToArray();
            for (int i = 0; i < booksToRemove.Length; i++)
            {
                _books.Remove(booksToRemove[i]);
            }
        }

        public Task<Book> AddBookAsync(Book book)
        {
            RemoveNullBooks();
            book.Id = _books.Max(b => b.Id) + 1;
            _books.Add(book);
            return Task.FromResult(book);
        }

        public Task UpdateBookAsync(Book book)
        {
            return Task.FromResult<object>(null);
        }

        public void Dispose()
        {
            
        }
    }
}
