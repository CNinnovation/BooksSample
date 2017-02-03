using BooksViewModels.Events;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksViewModels.ViewModels
{
    public class MainPageViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;

        private readonly SortedSet<string> inProgressList = new SortedSet<string>();

        public MainPageViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            _eventAggregator.GetEvent<InProgressEvent>().Subscribe(args =>
            {
                if (!args.Completed)
                {
                    inProgressList.Add(args.Id);
                }
                else if (args.Completed && inProgressList.Contains(args.Id))
                {
                    inProgressList.Remove(args.Id);
                }

                InProgress = inProgressList.Count > 0;
            });
        }

        private bool _inProgress = false;
        public bool InProgress
        {
            get => _inProgress;
            set => SetProperty(ref _inProgress, value);
        }
    }
}
