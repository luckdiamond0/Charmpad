using Microsoft.Win32;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.IO;
using System;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Windows.Media.Animation;
using System.Runtime.InteropServices;
using Charpad;

namespace Charpad
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string currentFilePath;
        public static readonly RoutedUICommand FindCommand = new RoutedUICommand("Find", "Find", typeof(MainWindow));
        public static readonly RoutedUICommand ReplaceCommand = new RoutedUICommand("Replace", "Replace", typeof(MainWindow));

        [DllImport("user32.dll")]
        private static extern int GetSystemMetrics(int nIndex);

        private const int SM_CXSCREEN = 0;
        private const int SM_CYSCREEN = 1;

        int screenWidth = GetSystemMetrics(SM_CXSCREEN);
        int screenHeight = GetSystemMetrics(SM_CYSCREEN);

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            editor.Width = screenWidth;
            editor.Height = screenHeight;

            if (App.CommandLineArgs.Length > 0)
            {
                string filePath = App.CommandLineArgs[0];
                if (File.Exists(filePath))
                {
                    currentFilePath = filePath;
                    editor.Text = File.ReadAllText(currentFilePath);
                }
                else
                {
                    MessageBox.Show("not found " + filePath);
                }
            }
        }

        private void LoadFonts(object sender, RoutedEventArgs e)
        {
            FontSettingsWindow fontSettingsWindow = new FontSettingsWindow(this);
            fontSettingsWindow.ShowDialog();
        }


        private void Editor_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            // Check if Ctrl key is pressed
            if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
            {
                // Adjust font size
                double newFontSize = editor.FontSize + (e.Delta > 0 ? 2 : -2); // Increase or decrease font size
                editor.FontSize = Math.Max(newFontSize, 1); // Prevent font size from becoming too small

                // Prevent the default scroll behavior
                e.Handled = true;
            }
            else
            {
                // Allow default scroll behavior if no modifier keys are pressed
                e.Handled = false;
            }
        }





        private void NewFile_Click(object sender, RoutedEventArgs e)
        {
            editor.Clear();
            currentFilePath = null;
        }


        private void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                currentFilePath = openFileDialog.FileName;
                editor.Text = File.ReadAllText(currentFilePath);
            }
        }

        private void SaveFile_Click(object sender, RoutedEventArgs e)
        {
            if (currentFilePath == null)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*"
                };

                if (saveFileDialog.ShowDialog() == true)
                {
                    currentFilePath = saveFileDialog.FileName;
                }
                else
                {
                    return;
                }
            }

            File.WriteAllText(currentFilePath, editor.Text);
        }

        private void SaveAsFile_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                currentFilePath = saveFileDialog.FileName;
                File.WriteAllText(currentFilePath, editor.Text);
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Find_Click(object sender, RoutedEventArgs e)
        {
            FindReplaceWindow findReplaceWindow = new FindReplaceWindow(this);
            findReplaceWindow.ShowDialog();
        }

        private void Replace_Click(object sender, RoutedEventArgs e)
        {
            FindReplaceWindow findReplaceWindow = new FindReplaceWindow(this);
            findReplaceWindow.ShowDialog();
        }

        public void FindNext(string searchText)
        {
            if (string.IsNullOrEmpty(searchText)) return;

            int startIndex = editor.SelectionStart + editor.SelectionLength;
            int index = editor.Text.IndexOf(searchText, startIndex, StringComparison.OrdinalIgnoreCase);

            if (index >= 0)
            {
                editor.Focus();
                editor.Select(index, searchText.Length);
                editor.ScrollToHome();
                editor.ScrollToVerticalOffset(editor.VerticalOffset + editor.FontSize);
                editor.SelectionBrush = Brushes.Blue;
            }
            else
            {
                MessageBox.Show("Text not found.");
            }
        }

        public void ReplaceAll(string searchText, string replaceText)
        {
            if (!string.IsNullOrEmpty(searchText))
            {
                editor.Text = editor.Text.Replace(searchText, replaceText);
            }
        }
    }
}