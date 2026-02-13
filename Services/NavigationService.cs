using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace modernwinpos.Services
{
    /// <summary>
    /// Simple navigation service for page navigation
    /// </summary>
    public class NavigationService
    {
        private static Frame _mainFrame;

        public static void Initialize(Frame frame)
        {
            _mainFrame = frame;
        }

        public static void NavigateTo<T>() where T : Page
        {
            _mainFrame?.NavigateTo(typeof(T));
        }

        public static void GoBack()
        {
            if (_mainFrame?.CanGoBack == true)
            {
                _mainFrame.GoBack();
            }
        }
    }
}
