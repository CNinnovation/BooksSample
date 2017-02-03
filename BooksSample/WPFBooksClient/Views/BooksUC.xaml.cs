using BooksViewModels.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Extensions.DependencyInjection;

namespace WPFBooksClient.Views
{
    /// <summary>
    /// Interaction logic for BooksUC.xaml
    /// </summary>
    public partial class BooksUC : UserControl
    {
        public BooksUC()
        {
            ViewModel = (Application.Current as App).Container.GetService<BooksViewModel>();
            InitializeComponent();
            this.DataContext = this;
        }

        public BooksViewModel ViewModel { get; }
    }
}
