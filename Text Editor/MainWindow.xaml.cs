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
using System.Web;
using System.Net;
using System.Net.Http;
using System;

using Newtonsoft.Json;
using static System.Net.Mime.MediaTypeNames;

namespace Text_Editor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string fileName = string.Empty;
        public string[] readText = new string[1000];
        static readonly HttpClient client = new HttpClient();

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
        private async void translateFileBtn_Click(object sender, RoutedEventArgs e)
        {
            if (fileName != string.Empty)
            {
                var translatedText = await translateAsync(fileSpaceBox.Text);
                fileSpaceBox.Text = translatedText;
            }
        }
        private void fileSpaceBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var keyIndex = fileSpaceBox.CaretIndex;
                fileSpaceBox.Text = fileSpaceBox.Text.Insert(keyIndex, "\n");
                fileSpaceBox.CaretIndex = keyIndex + 1;
            }
        }
        public async Task<String> translateAsync(String input)
        {
            var fromLanguage = "EN";
            var toLanguage = "RU";
            string url = $"http://api.mymemory.translated.net/get?q={Uri.EscapeDataString(input)}&langpair={fromLanguage}|{toLanguage}";

            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            string responseJson = await response.Content.ReadAsStringAsync();
            var translationResult = JsonConvert.DeserializeObject<TranslationResponse>(responseJson);

            if (translationResult.ResponseStatus == 200)
            {
                return translationResult.TranslatedText;
            }

            return string.Empty;
        }

        public class TranslationResponse
        {
            [JsonProperty("responseStatus")]
            public int ResponseStatus { get; set; }

            [JsonProperty("responseData")]
            public TranslationData ResponseData { get; set; }

            public string TranslatedText => ResponseData?.TranslatedText;

        }

        public class TranslationData
        {
            [JsonProperty("translatedText")]
            public string TranslatedText { get; set; }

        }
    }
}