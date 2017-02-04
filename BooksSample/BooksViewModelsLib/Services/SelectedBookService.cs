using BooksViewModels.Models;
using Prism.Mvvm;

namespace BooksViewModels.Services
{
    public class SelectedBookService : BindableBase, ISelectedBookService
    {
        private Book _book = Book.Empty;

        public Book Book
        {
            get => _book;
            set => SetProperty(ref _book, value);
        }
    }
}
