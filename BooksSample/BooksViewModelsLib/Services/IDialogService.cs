using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksViewModels.Services
{
    public interface IDialogService
    {
        Task ShowMessageAsync(string message);
    }
}
