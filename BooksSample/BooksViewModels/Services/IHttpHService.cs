using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BooksViewModels.Services
{
    public interface IHttpHService : IDisposable
    {
        Task<T> AddItem<T>(string url, T item);
        Task<T> GetItemById<T>(string url, int id);
        Task<IEnumerable<T>> GetItemsAsync<T>(string url);
        Task<T> UpdateItem<T>(string url, T item);
    }
}