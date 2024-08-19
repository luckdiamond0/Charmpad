using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Charpad
{
    public partial class FontSettingsWindow : Window
    {
        private MainWindow mainWindow;

        public FontSettingsWindow(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            LoadFonts();
            SetSelectedFont();
        }

        private void SetSelectedFont()
        {
            if (mainWindow.editor.FontFamily != null)
            {
                string currentFont = mainWindow.editor.FontFamily.Source;
                FontComboBox.SelectedItem = currentFont;
            }
        }


        private void LoadFonts()
        {
            foreach (var fontFamily in Fonts.SystemFontFamilies)
            {
                FontComboBox.Items.Add(fontFamily.Source);
            }
            if (FontComboBox.Items.Count > 0)
                FontComboBox.SelectedIndex = 0;
        }

        private void Apply_Click(object sender, RoutedEventArgs e)
        {
            string selectedFont = FontComboBox.SelectedItem.ToString();
            // Aplicar a fonte selecionada ao TextBox na janela principal
            mainWindow.editor.FontFamily = new FontFamily(selectedFont);
            this.Close();
        }
    }
}
