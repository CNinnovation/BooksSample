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
        public InProgressEventArgs()
        {

        }
        public string Id { get; } = Guid.NewGuid().ToString();

        private bool _isComplete = false;
        public bool SetComplete() => _isComplete = true;
        public bool IsCompleted => _isComplete;
    }

    public class InProgressEvent : PubSubEvent<InProgressEventArgs>
    {
    }
}
