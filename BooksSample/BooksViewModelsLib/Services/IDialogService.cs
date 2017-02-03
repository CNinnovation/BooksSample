using System.Threading.Tasks;

namespace BooksViewModels.Services
{
    public interface IDialogService
    {
        Task ShowMessageAsync(string message);
    }
}
