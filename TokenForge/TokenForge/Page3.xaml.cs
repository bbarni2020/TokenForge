using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.Json;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace TokenForge
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// 
    public class TokenInfo
    {
        public string name { get; set; }
        public string symbol { get; set; }
        public string description { get; set; }
        public string image { get; set; }
    }

    public class FileGenerator
    {
        public static void GenerateJsonFileInDownloads()
        {
            // Get the Downloads folder path
            string downloadsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
            string filePath = Path.Combine(downloadsPath, "metadata.json");

            // Create the JSON object
            var tokenInfo = new TokenInfo
            {
                name = $"{GlobalVariables.TokenName}",
                symbol = $"{GlobalVariables.TokenSymbol}",
                description = $"{GlobalVariables.TokenDescription}",
                image = $"{GlobalVariables.TokenUrl}"
            };

            // Serialize the object to JSON
            string jsonString = JsonSerializer.Serialize(tokenInfo, new JsonSerializerOptions { WriteIndented = true });

            // Write to the file
            File.WriteAllText(filePath, jsonString);
        }
    }
    public sealed partial class Page3 : Page
    {
        public Page3()
        {
            this.InitializeComponent();
            tokendsc.Text = GlobalVariables.TokenDescription;
            tokennum.Text = Convert.ToString(GlobalVariables.TokenCount);
            if (!string.IsNullOrEmpty(tokendsc.Text))
            {
                NextButton3.IsEnabled = true;

            }
        }
        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            // Enable the button only when all three TextBoxes have some text
            if (!string.IsNullOrEmpty(tokendsc.Text))
            {
                NextButton3.IsEnabled = true;
            } else
            {
                // Disable the button if any of the TextBoxes is empty
                NextButton3.IsEnabled = false;
            }
        }
        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            GlobalVariables.TokenCount = Convert.ToInt32(tokennum.Text);
            GlobalVariables.TokenDescription = tokendsc.Text;
            FileGenerator.GenerateJsonFileInDownloads();
            this.Frame.Navigate(typeof(Page5));
        }
        private void GoBack(object sender, RoutedEventArgs e)
        {
            if (this.Frame.CanGoBack)
            {
                this.Frame.GoBack();
            }
        }

    }
}
