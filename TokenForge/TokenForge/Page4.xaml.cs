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
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace TokenForge
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Page4 : Page
    {
        public Page4()
        {
            this.InitializeComponent();
            name.Text = $"Token name: {GlobalVariables.TokenName}";
            symbol.Text = $"Token symbol: {GlobalVariables.TokenSymbol}";
            description.Text = $"Token description: {GlobalVariables.TokenDescription}";
            url.Text = $"Token picture url: {GlobalVariables.TokenUrl}";
            count.Text = $"Token count: {Convert.ToString(GlobalVariables.TokenCount)}";
            network.Text = $"Token network: Devnet (Mainnet is unstable){GlobalVariables.TokenNetwork}";
            username.Text = $"WSL username: {GlobalVariables.Username}";
            password.Text = $"WSL password: {GlobalVariables.Password}";
            path.Text = $"Token folder: {GlobalVariables.path}";
            NextButton3.IsEnabled = true ;
        }
        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(CMD));
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