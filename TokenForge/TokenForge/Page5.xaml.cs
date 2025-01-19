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

using Windows.Storage.Pickers;
using Windows.Storage;
using WinRT.Interop;

namespace TokenForge
{
    public sealed partial class Page5 : Page
    {
        public Page5()
        {
            this.InitializeComponent();
        }
// For WindowNative and InitializeWithWindow

    private async void SelectFolderButton_Click(object sender, RoutedEventArgs e)
        {
            // Access the main window
            var mainWindow = (App.Current as App)?.MainWindow;

            if (mainWindow == null)
            {
                // Handle the case where the main window is not available
                InstructionText.Text = "Main window not found.";
                return;
            }

            // Create the folder picker
            var folderPicker = new FolderPicker
            {
                SuggestedStartLocation = PickerLocationId.Desktop
            };
            folderPicker.FileTypeFilter.Add("*"); // Necessary, even though we're picking folders

            // Get the HWND of the main window
            var hwnd = WindowNative.GetWindowHandle(mainWindow);

            // Associate the HWND with the folder picker
            InitializeWithWindow.Initialize(folderPicker, hwnd);

            // Show the folder picker
            StorageFolder selectedFolder = await folderPicker.PickSingleFolderAsync();
            if (selectedFolder != null)
            {
                // Folder was picked; you can now use selectedFolder
                SelectedFolderText.Text = $"Selected folder: {selectedFolder.Name}";
                GlobalVariables.path = selectedFolder.Path;
                if (!string.IsNullOrEmpty(username.Text) &&
                !string.IsNullOrEmpty(password.Text))
                {
                    NextButton3.IsEnabled = true;
                }
                else
                {
                    NextButton3.IsEnabled = false;
                }
            }
            else
            {
                // Operation was canceled
                SelectedFolderText.Text = "No folder selected.";
                NextButton3.IsEnabled = false;
            }
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedFolderText.Text != "No folder selected.")
            {
                GlobalVariables.Username = username.Text;
                GlobalVariables.Password = password.Text;
                GlobalVariables.MetadataURL = metadata.Text;
                this.Frame.Navigate(typeof(Page4));
            }
        }


        private void GoBack(object sender, RoutedEventArgs e)
        {
            if (this.Frame.CanGoBack)
            {
                this.Frame.GoBack();
            }
        }
        private void TokenUrl_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Enable the button only when all three TextBoxes have some text
            if (!string.IsNullOrEmpty(username.Text) &&
                !string.IsNullOrEmpty(password.Text) && SelectedFolderText.Text != "No folder selected.")
            {
                NextButton3.IsEnabled = true;
            }
            else
            {
                // Disable the button if any of the TextBoxes is empty
                NextButton3.IsEnabled = false;
            }
        }
    }
}

