using BooksViewModels.Models;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksViewModels.Events
{
    public class SelectedBookEvent : PubSubEvent<Book>
    {
    }
}
