using BooksViewModels.Events;
using BooksViewModels.Models;
using BooksViewModels.Services;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace BooksViewModels.ViewModels
{
    public class BooksViewModel : BindableBase
    {
        private readonly IBooksService _booksService;
        private readonly ISelectedBookService _selectedBookService;
        private readonly IDialogService _dialogService;
        private readonly IEventAggregator _eventAggregator;

        public BooksViewModel(IBooksService booksService, ISelectedBookService selectedBookService, IDialogService dialogService, IEventAggregator eventAggregator)
        {
            _booksService = booksService;
            _selectedBookService = selectedBookService;
            _dialogService = dialogService;
            _eventAggregator = eventAggregator;

            _selectedBookService.PropertyChanged += (sender, e) =>
            {
                base.OnPropertyChanged(nameof(CurrentBook));
            };

            RefreshBooksCommand = new DelegateCommand(OnGetBooksAsync);
        }

        public DelegateCommand RefreshBooksCommand { get; }

        private async void OnGetBooksAsync()
        {
            InProgressEventArgs args = new InProgressEventArgs { Id = Guid.NewGuid().ToString(), Completed = false };
            try
            {
                _eventAggregator.GetEvent<InProgressEvent>().Publish(args);
                await _booksService.GetBooksAsync();
                await Task.Delay(4000); // delay to show the progress for a longer time
            }
            catch (Exception ex)
            {
                await _dialogService.ShowMessageAsync(ex.Message);
            }
            args.Completed = true;
            _eventAggregator.GetEvent<InProgressEvent>().Publish(args);
           
        }

        public IEnumerable<Book> Books => _booksService.Books;

        public Book CurrentBook
        {
            get => _selectedBookService.Book;
            set => _selectedBookService.Book = value;
        }
    }
}
