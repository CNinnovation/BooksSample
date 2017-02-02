using BooksViewModels.Models;
using System.ComponentModel;

namespace BooksViewModels.Services
{
    public interface ISelectedBookService : INotifyPropertyChanged
    {
        Book Book { get; set; }
    }
}