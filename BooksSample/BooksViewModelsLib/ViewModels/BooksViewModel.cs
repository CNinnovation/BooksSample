using BooksViewModels.Events;
using BooksViewModels.Models;
using BooksViewModels.Services;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BooksViewModels.ViewModels
{
    public class BooksViewModel : BindableBase
    {
        private readonly IBooksService _booksService;
        private readonly ISelectedBookService _selectedBookService;
        private readonly IDialogService _dialogService;
        private readonly IEventAggregator _eventAggregator;
        private readonly IOpenWindow _openWindow;

        public BooksViewModel(IBooksService booksService, ISelectedBookService selectedBookService, IDialogService dialogService, IEventAggregator eventAggregator, IOpenWindow openWindow)
        {
            _booksService = booksService;
            _selectedBookService = selectedBookService;
            _dialogService = dialogService;
            _eventAggregator = eventAggregator;
            _openWindow = openWindow;

            _selectedBookService.PropertyChanged += (sender, e) =>
            {
                base.OnPropertyChanged(nameof(CurrentBook));
            };

            RefreshBooksCommand = new DelegateCommand(OnGetBooks);
            NewWindowCommand = new DelegateCommand(OnNewWindow);
        }

        public DelegateCommand RefreshBooksCommand { get; }
        public DelegateCommand NewWindowCommand { get; }

        public async void OnGetBooks()
        {
            InProgressEventArgs args = new InProgressEventArgs();
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
            args.SetComplete();
            _eventAggregator.GetEvent<InProgressEvent>().Publish(args);           
        }

        public void OnNewWindow()
        {
            _openWindow.OpenWindowAsync();
        }

        public IEnumerable<Book> Books => _booksService.Books;

        public Book CurrentBook
        {
            get => _selectedBookService.Book;
            set => _selectedBookService.Book = value;
        }
    }
}
