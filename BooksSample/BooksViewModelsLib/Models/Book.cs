using Prism.Mvvm;

namespace BooksViewModels.Models
{
    public class Book : BindableBase
    {
        public int Id { get; set; }
        private string _title;

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        private string _publisher;

        public string Publisher
        {
            get => _publisher;
            set => SetProperty(ref _publisher, value);
        }

        private string _isbn;

        public string Isbn
        {
            get => _isbn;
            set => SetProperty(ref _isbn, value);
        }

        public override string ToString() => Title;

        public static Book Empty => new Book();
        public static Book New => new Book { Id = 0, Title = "enter title", Publisher = "enter publisher" };
    }
}
