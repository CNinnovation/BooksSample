using BooksViewModels.Models;
using BooksViewModels.Services;
using Prism.Commands;
using Prism.Mvvm;

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

        public BookViewModel(IBooksService booksService, ISelectedBookService selectedBookService)
        {
            _booksService = booksService;
            _selectedBookService = selectedBookService;

            AddBookCommand = new DelegateCommand(OnAddBook, CanAddBook);
            EditBookCommand = new DelegateCommand(OnEditBook, CanEditBook);
            SaveBookCommand = new DelegateCommand(OnSaveBook, CanSaveBook);
            CancelBookCommand = new DelegateCommand(OnCancel, CanCancel);

            CurrentEditMode = BookEditMode.Read;
            UpdateCommandState();
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
        }

        public void OnEditBook()
        {
            CurrentEditMode = BookEditMode.Edit;
        }

        public void OnSaveBook()
        {
            // TODO: save using service
            CurrentEditMode = BookEditMode.Read;
        }

        public void OnCancel()
        {
            CurrentEditMode = BookEditMode.Read;
        }

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
