using BooksViewModels.Models;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksViewModels.Services
{
    public class SelectedBookService : BindableBase, ISelectedBookService
    {
        private Book _book;

        public Book Book
        {
            get => _book;
            set { SetProperty(ref _book, value); }
        }

    }
}
