using BooksViewModels.Models;
using BooksViewModels.Services;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace BooksViewModels.ViewModels
{
    public class BooksViewModel
    {
        private readonly IBooksService _booksService;
        public BooksViewModel(IBooksService booksService)
        {
            _booksService = booksService;

            RefreshBooksCommand = new DelegateCommand(OnGetBooksAsync);
            AddBookCommand = new DelegateCommand(OnGetBooksAsync);
        }

        public DelegateCommand RefreshBooksCommand { get; }
        public DelegateCommand AddBookCommand { get; }

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


    }
}
