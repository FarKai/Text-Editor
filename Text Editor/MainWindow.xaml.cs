using Microsoft.Win32;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace Text_Editor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string fileName = string.Empty;
        public string[] readText = new string[1000];
        public MainWindow()
        {
            InitializeComponent();
        }

        private void openFileBtn_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog(); 
            openFileDialog.InitialDirectory = "c:\\";
            openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog.Multiselect = false;
            openFileDialog.ShowDialog();

            fileName = openFileDialog.FileName;
            if (fileName != string.Empty)
            {
                fileNameBlock.Text = "";
                fileSpaceBox.Text = "";
                fileNameBlock.Text = fileName;
                readText = File.ReadAllLines(fileName);
                for (int i = 0; i < readText.Length; i++)
                {
                    fileSpaceBox.Text += readText[i] + '\n';
                }
            }

        }
        private void saveFileBtn_Click(object sender, RoutedEventArgs e)
        {
            if (fileName != string.Empty)
            {
                File.WriteAllText(fileName, fileSpaceBox.Text);
            }
        }
        private void deleteFileBtn_Click(object sender, RoutedEventArgs e)
        {

        }
        private void fileSpaceBox_KeyDown(object sender, System.Windows.Input.KeyboardEventArgs e)
        {
            if (e.Key = )
        }
    }
}