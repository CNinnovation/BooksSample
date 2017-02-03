using System.Collections.Generic;
using System.Threading.Tasks;
using BooksViewModels.Models;

namespace BooksViewModels.Services
{
    public interface IBooksService
    {
        IEnumerable<Book> Books { get; }

        Task<IEnumerable<Book>> GetBooksAsync();
        Book PrepareAddBook();
        Task<Book> AddBookAsync(Book book);
        Task UpdateBookAsync(Book book);
    }
}