using BooksViewModels.Services;
using BooksViewModels.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WPFBooksClient.WPFServices;

namespace WPFBooksClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            RegisterServices();
        }
        private void RegisterServices()
        {
            var services = new ServiceCollection();
            services.AddTransient<BooksViewModel>();
            services.AddTransient<BookViewModel>();
            services.AddTransient<MainPageViewModel>();
            services.AddSingleton<IBooksService, BooksService>();
            services.AddSingleton<IAddressService, AddressService>();
            services.AddSingleton<IHttpHService, HttpHService>();
            services.AddSingleton<ISelectedBookService, SelectedBookService>();
            services.AddSingleton<IDialogService, WPFDialogService>();
            services.AddSingleton<IEventAggregator, EventAggregator>();
            Container = services.BuildServiceProvider();
        }

        public IServiceProvider Container { get; private set; }
    }
}
