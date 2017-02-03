using BooksViewModels.ViewModels;
using System;
using Windows.UI.Xaml.Data;

namespace UWPBooksClient.Utilities
{
    public class BookEditModeToIsReadonlyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language) => 
            !(((BookEditMode)value) == BookEditMode.Edit || ((BookEditMode)value) == BookEditMode.New);


        public object ConvertBack(object value, Type targetType, object parameter, string language) => throw new NotSupportedException();
    }
}
