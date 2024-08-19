using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Charpad
{
    /// <summary>
    /// Interaction logic for FindReplaceWindow.xaml
    /// </summary>
    public partial class FindReplaceWindow : Window
    {
        private readonly MainWindow _mainWindow;
        public FindReplaceWindow(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
        }

        private void FindNext_Click(object sender, RoutedEventArgs e)
        {
            string searchText = findTextBox.Text;
            if (!string.IsNullOrEmpty(searchText))
            {
                _mainWindow.FindNext(searchText);
            }
        }

        private void ReplaceAll_Click(object sender, RoutedEventArgs e)
        {
            string searchText = findReplaceTextBox.Text;
            string replaceText = replaceTextBox.Text;
            if (!string.IsNullOrEmpty(searchText))
            {
                _mainWindow.ReplaceAll(searchText, replaceText);
            }
        }

        private void FindTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            findPlaceholder.Visibility = Visibility.Collapsed;
        }

        private void FindTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(findTextBox.Text))
            {
                findPlaceholder.Visibility = Visibility.Visible;
            }
        }

        private void FindReplaceTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            replaceFindPlaceholder.Visibility = Visibility.Collapsed;
        }

        private void FindReplaceTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(findReplaceTextBox.Text))
            {
                replaceFindPlaceholder.Visibility = Visibility.Visible;
            }
        }

        private void ReplaceTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            replacePlaceholder.Visibility = Visibility.Collapsed;
        }

        private void ReplaceTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(replaceTextBox.Text))
            {
                replacePlaceholder.Visibility = Visibility.Visible;
            }
        }
    }
}
