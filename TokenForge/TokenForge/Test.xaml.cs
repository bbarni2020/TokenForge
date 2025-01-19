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
using System.Threading.Tasks;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace TokenForge
{

    public sealed partial class Test : Page
    {
        private WslSession _wslSession;

        public Test()
        {
            this.InitializeComponent();
            _wslSession = new WslSession();
        }

        private async void ExecuteCommandButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string command = CommandTextBox.Text;
                if (!string.IsNullOrEmpty(command))
                {
                    string output = await _wslSession.SendCommandAsync(command);
                    OutputTextBox.Text = output;
                }
            }
            catch (Exception ex)
            {
                OutputTextBox.Text = $"Error: {ex.Message}";
            }
        }
    }

}
