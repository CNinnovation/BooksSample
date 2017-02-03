using BooksViewModels.Events;
using BooksViewModels.Models;
using BooksViewModels.Services;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;

namespace BooksViewModels.ViewModels
{
    public enum BookEditMode
    {
        Read,
        Edit,
        New
    }

    public class BookViewModel : BindableBase
    {
        private readonly IBooksService _booksService;
        private readonly ISelectedBookService _selectedBookService;
        private readonly IDialogService _dialogService;
        private readonly IEventAggregator _eventAggregator;

        public BookViewModel(IBooksService booksService, ISelectedBookService selectedBookService, IDialogService dialogService, IEventAggregator eventAggregator)
        {
            _booksService = booksService;
            _selectedBookService = selectedBookService;
            _dialogService = dialogService;
            _eventAggregator = eventAggregator;

            AddBookCommand = new DelegateCommand(OnAddBook, CanAddBook);
            EditBookCommand = new DelegateCommand(OnEditBook, CanEditBook);
            SaveBookCommand = new DelegateCommand(OnSaveBook, CanSaveBook);
            CancelBookCommand = new DelegateCommand(OnCancel, CanCancel);

            CurrentEditMode = BookEditMode.Read;
            UpdateCommandState();

            _selectedBookService.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == "Book") OnPropertyChanged("Book");  // fire property change on view-model Book if referenced Book fires change
            };
        }

        public DelegateCommand AddBookCommand { get; }
        public DelegateCommand EditBookCommand { get; }
        public DelegateCommand SaveBookCommand { get; }
        public DelegateCommand CancelBookCommand { get; }

        private BookEditMode _currentEditMode;

        public BookEditMode CurrentEditMode
        {
            get => _currentEditMode;
            set
            {
                if (SetProperty(ref _currentEditMode, value))
                {
                    UpdateCommandState();
                }
            }
        }

        public Book Book => _selectedBookService.Book;

        public void OnAddBook()
        {
            CurrentEditMode = BookEditMode.New;
            Book newBook = _booksService.PrepareAddBook();
            _selectedBookService.Book = newBook;
        }

        public void OnEditBook() => CurrentEditMode = BookEditMode.Edit;


        public async void OnSaveBook()
        {
            InProgressEventArgs args = new InProgressEventArgs();
            _eventAggregator.GetEvent<InProgressEvent>().Publish(args);
            try
            {
                switch (CurrentEditMode)
                {
                    case BookEditMode.Edit:
                        await _booksService.UpdateBookAsync(_selectedBookService.Book);
                        break;
                    case BookEditMode.New:
                        Book newBook = await _booksService.AddBookAsync(_selectedBookService.Book);
                        await _booksService.GetBooksAsync();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                await _dialogService.ShowMessageAsync(ex.Message);
            }

            args.SetComplete();
            _eventAggregator.GetEvent<InProgressEvent>().Publish(args);
            CurrentEditMode = BookEditMode.Read;
        }

        public void OnCancel() => CurrentEditMode = BookEditMode.Read;

        public bool CanEditBook() => _currentEditMode == BookEditMode.Read;

        public bool CanAddBook() => _currentEditMode == BookEditMode.Read;

        public bool CanSaveBook() => _currentEditMode == BookEditMode.Edit || _currentEditMode == BookEditMode.New;

        public bool CanCancel() => _currentEditMode == BookEditMode.Edit || _currentEditMode == BookEditMode.New;

        private void UpdateCommandState()
        {
            AddBookCommand?.RaiseCanExecuteChanged();
            EditBookCommand?.RaiseCanExecuteChanged();
            SaveBookCommand?.RaiseCanExecuteChanged();
            CancelBookCommand?.RaiseCanExecuteChanged();
        }
    }
}
