using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Windowing;
using Windows.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using PInvoke; // Add PInvoke package to the project

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace TokenForge
{
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();

            // Get the AppWindow for the current window
            var appWindow = GetAppWindowForCurrentWindow();

            // Customize the title bar
            if (AppWindowTitleBar.IsCustomizationSupported())
            {
                var titleBar = appWindow.TitleBar;

                // Set the title bar colors to match the page's theme
                titleBar.BackgroundColor = GetApplicationPageBackgroundColor(); // Adjust this to match your page background
                titleBar.ForegroundColor = GetApplicationPageBackgroundColor(); // Text color
                titleBar.ButtonBackgroundColor = GetApplicationPageBackgroundColor();
                titleBar.ButtonForegroundColor = Colors.White;
                titleBar.ButtonHoverBackgroundColor = Colors.White; // Hover effect
                titleBar.ButtonHoverForegroundColor = GetApplicationPageBackgroundColor();
                titleBar.ButtonPressedBackgroundColor = Colors.White; // Pressed effect
                titleBar.ButtonPressedForegroundColor = GetApplicationPageBackgroundColor();
            }


            // Set the app icon
            SetAppIcon("icon.ico");
        }

        private AppWindow GetAppWindowForCurrentWindow()
        {
            var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(this);
            var windowId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(hWnd);
            return AppWindow.GetFromWindowId(windowId);
        }

        public Color GetApplicationPageBackgroundColor()
        {
            // Retrieve the resource brush from Application resources
            var brush = (SolidColorBrush)Application.Current.Resources["ApplicationPageBackgroundThemeBrush"];

            // Extract the Color property from the SolidColorBrush
            return brush.Color;
        }

        private void SetAppIcon(string iconPath)
        {
            var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(this);
            IntPtr hIcon = User32.LoadImage(IntPtr.Zero, iconPath,
            User32.ImageType.IMAGE_ICON, 0, 0, User32.LoadImageFlags.LR_LOADFROMFILE);

            if (hIcon != IntPtr.Zero)
            {
                // Set small and large icons for the app window
                User32.SendMessage(hWnd, User32.WindowMessage.WM_SETICON, new IntPtr(0), hIcon); // Small icon
                User32.SendMessage(hWnd, User32.WindowMessage.WM_SETICON, new IntPtr(1), hIcon); // Large icon
            }
        }
    }
}
