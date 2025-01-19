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
using PInvoke;

namespace TokenForge
{
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
            var appWindow = GetAppWindowForCurrentWindow();
            if (AppWindowTitleBar.IsCustomizationSupported())
            {
                var titleBar = appWindow.TitleBar;
                titleBar.BackgroundColor = GetApplicationPageBackgroundColor();
                titleBar.ForegroundColor = GetApplicationPageBackgroundColor();
                titleBar.ButtonBackgroundColor = GetApplicationPageBackgroundColor();
                titleBar.ButtonForegroundColor = Colors.White;
                titleBar.ButtonHoverBackgroundColor = Colors.White;
                titleBar.ButtonHoverForegroundColor = GetApplicationPageBackgroundColor();
                titleBar.ButtonPressedBackgroundColor = Colors.White;
                titleBar.ButtonPressedForegroundColor = GetApplicationPageBackgroundColor();
            }
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
            var brush = (SolidColorBrush)Application.Current.Resources["ApplicationPageBackgroundThemeBrush"];
            return brush.Color;
        }

        private void SetAppIcon(string iconPath)
        {
            var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(this);
            IntPtr hIcon = User32.LoadImage(IntPtr.Zero, iconPath,
            User32.ImageType.IMAGE_ICON, 0, 0, User32.LoadImageFlags.LR_LOADFROMFILE);

            if (hIcon != IntPtr.Zero)
            {
                User32.SendMessage(hWnd, User32.WindowMessage.WM_SETICON, new IntPtr(0), hIcon);
                User32.SendMessage(hWnd, User32.WindowMessage.WM_SETICON, new IntPtr(1), hIcon);
            }
        }
    }
}
