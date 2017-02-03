using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksViewModels.Events
{
    public class InProgressEventArgs
    {
        public string Id { get; set; }
        public bool Completed { get; set; }
    }

    public class InProgressEvent : PubSubEvent<InProgressEventArgs>
    {
    }
}
