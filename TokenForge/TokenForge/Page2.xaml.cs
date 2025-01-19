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
using Windows.Media.SpeechRecognition;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace TokenForge
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// 
    public sealed partial class Page2 : Page
    {
        public Page2()
        {
            this.InitializeComponent();
            tokenname.PlaceholderText = GlobalVariables.TokenName;
            tokensymbol.PlaceholderText = GlobalVariables.TokenSymbol;
            tokenurl.PlaceholderText = GlobalVariables.TokenUrl;
            if (!string.IsNullOrEmpty(tokenname.Text) &&
                !string.IsNullOrEmpty(tokensymbol.Text) &&
                !string.IsNullOrEmpty(tokenurl.Text))
            {
                NextButton2.IsEnabled = true;
            }
        }
        private void TokenUrl_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Enable the button only when all three TextBoxes have some text
            if (!string.IsNullOrEmpty(tokenname.Text) &&
                !string.IsNullOrEmpty(tokensymbol.Text) &&
                !string.IsNullOrEmpty(tokenurl.Text))
            {
                NextButton2.IsEnabled = true;
            }
            else
            {
                // Disable the button if any of the TextBoxes is empty
                NextButton2.IsEnabled = false;
            }
        }
        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            GlobalVariables.TokenName = tokenname.Text;
            GlobalVariables.TokenUrl = tokenurl.Text;
            GlobalVariables.TokenSymbol = tokensymbol.Text;
            this.Frame.Navigate(typeof(Page3));
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
