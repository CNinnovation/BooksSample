using BooksViewModels.Services;
using System.Threading.Tasks;
using System.Windows;

namespace WPFBooksClient.WPFServices
{
    public class WPFDialogService : IDialogService
    {
        public Task ShowMessageAsync(string message) =>
            Task.Run(() => MessageBox.Show(message));

    }
}
