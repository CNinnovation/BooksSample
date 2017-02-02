using BooksViewModels.Models;
using BooksViewModels.Services;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace BooksViewModels.ViewModels
{
    public class BooksViewModel : BindableBase
    {
        private readonly IBooksService _booksService;
        private readonly ISelectedBookService _selectedBookService;

        public BooksViewModel(IBooksService booksService, ISelectedBookService selectedBookService)
        {
            _booksService = booksService;
            _selectedBookService = selectedBookService;

            _selectedBookService.PropertyChanged += (sender, e) =>
            {
                base.OnPropertyChanged(nameof(CurrentBook));
            };

            RefreshBooksCommand = new DelegateCommand(OnGetBooksAsync);
        }

        public DelegateCommand RefreshBooksCommand { get; }

        private async void OnGetBooksAsync()
        {
            try
            {
                await _booksService.GetBooksAsync();
            }
            catch (Exception ex)
            {

            }
        }

        public IEnumerable<Book> Books => _booksService.Books;

        public Book CurrentBook
        {
            get => _selectedBookService.Book;
            set => _selectedBookService.Book = value;
        }


    }
}
