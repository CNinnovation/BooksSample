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


    }
}
