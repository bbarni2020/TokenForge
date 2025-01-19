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
using Windows.Storage.Pickers;
using Windows.Storage;
using WinRT.Interop;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using System.Diagnostics;
using System.Net;
using static System.Net.Mime.MediaTypeNames;
using System.Globalization;
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
using Windows.Storage.Pickers;
using Windows.Storage;
using WinRT.Interop;
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



namespace TokenForge
{
    public sealed partial class BlankPage1 : Page
    {
        public BlankPage1()
        {
            // Initialize the components of the page
            this.InitializeComponent();
        }


        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            // Check if a network is selected
            if (NetworkComboBox.SelectedIndex > -1)
            {
                // Navigate to Page2 if a network is selected
                this.Frame.Navigate(typeof(Page2));
            }
            else
            {
                // Show error message if no network is selected
                ErrorText.Visibility = Visibility.Visible;
            }
        }
    }



    public sealed partial class Page2 : Page
    {


        public Page2()
        {
            // Initialize the components of the page
            this.InitializeComponent();

            // Set placeholder texts for the token details
            tokenname.PlaceholderText = GlobalVariables.TokenName;
            tokensymbol.PlaceholderText = GlobalVariables.TokenSymbol;
            tokenurl.PlaceholderText = GlobalVariables.TokenUrl;

            // Enable the Next button if all token details are filled
            if (!string.IsNullOrEmpty(tokenname.Text) &&
                !string.IsNullOrEmpty(tokensymbol.Text) &&
                !string.IsNullOrEmpty(tokenurl.Text))
            {
                NextButton2.IsEnabled = true;
            }
        }



        private void TokenUrl_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Enable the Next button only when all three TextBoxes have some text
            if (!string.IsNullOrEmpty(tokenname.Text) &&
                !string.IsNullOrEmpty(tokensymbol.Text) &&
                !string.IsNullOrEmpty(tokenurl.Text))
            {
                NextButton2.IsEnabled = true;
            }
            else
            {
                // Disable the Next button if any of the TextBoxes is empty
                NextButton2.IsEnabled = false;
            }
        }



        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            // Save the token details to global variables
            GlobalVariables.TokenName = tokenname.Text;
            GlobalVariables.TokenUrl = tokenurl.Text;
            GlobalVariables.TokenSymbol = tokensymbol.Text;

            // Navigate to Page3
            this.Frame.Navigate(typeof(Page3));
        }

        private void GoBack(object sender, RoutedEventArgs e)
        {
            // Navigate back if possible
            if (this.Frame.CanGoBack)
            {
                this.Frame.GoBack();
            }
        }
    }



    public class TokenInfo
    {
        // Properties for token information
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

            // Create the JSON object with token information
            var tokenInfo = new TokenInfo
            {
                name = $"{GlobalVariables.TokenName}",
                symbol = $"{GlobalVariables.TokenSymbol}",
                description = $"{GlobalVariables.TokenDescription}",
                image = $"{GlobalVariables.TokenUrl}"
            };

            // Serialize the object to JSON
            string jsonString = JsonSerializer.Serialize(tokenInfo, new JsonSerializerOptions { WriteIndented = true });

            // Write the JSON string to the file
            File.WriteAllText(filePath, jsonString);
        }
    }



    public sealed partial class Page3 : Page
    {
        public Page3()
        {
            // Initialize the components of the page
            this.InitializeComponent();

            // Set the token description and count
            tokendsc.Text = GlobalVariables.TokenDescription;
            tokennum.Text = Convert.ToString(GlobalVariables.TokenCount);

            // Enable the Next button if the token description is filled
            if (!string.IsNullOrEmpty(tokendsc.Text))
            {
                NextButton3.IsEnabled = true;
            }
        }



        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            // Enable the Next button only when the token description is filled
            if (!string.IsNullOrEmpty(tokendsc.Text))
            {
                NextButton3.IsEnabled = true;
            }
            else
            {
                // Disable the Next button if the token description is empty
                NextButton3.IsEnabled = false;
            }
        }



        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            // Save the token count and description to global variables
            GlobalVariables.TokenCount = Convert.ToInt32(tokennum.Text);
            GlobalVariables.TokenDescription = tokendsc.Text;

            // Generate the JSON file in the Downloads folder
            FileGenerator.GenerateJsonFileInDownloads();

            // Navigate to Page5
            this.Frame.Navigate(typeof(Page5));
        }



        private void GoBack(object sender, RoutedEventArgs e)
        {
            // Navigate back if possible
            if (this.Frame.CanGoBack)
            {
                this.Frame.GoBack();
            }
        }
    }





    public sealed partial class Page4 : Page
    {
        public Page4()
        {
            // Initialize the components of the page
            this.InitializeComponent();

            // Set the token details
            name.Text = $"Token name: {GlobalVariables.TokenName}";
            symbol.Text = $"Token symbol: {GlobalVariables.TokenSymbol}";
            description.Text = $"Token description: {GlobalVariables.TokenDescription}";
            url.Text = $"Token picture url: {GlobalVariables.TokenUrl}";
            count.Text = $"Token count: {Convert.ToString(GlobalVariables.TokenCount)}";
            network.Text = $"Token network: Devnet (Mainnet is unstable){GlobalVariables.TokenNetwork}";
            username.Text = $"WSL username: {GlobalVariables.Username}";
            password.Text = $"WSL password: {GlobalVariables.Password}";
            path.Text = $"Token folder: {GlobalVariables.path}";

            // Enable the Next button
            NextButton3.IsEnabled = true;
        }



        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to CMD page
            this.Frame.Navigate(typeof(CMD));
        }



        private void GoBack(object sender, RoutedEventArgs e)
        {
            // Navigate back if possible
            if (this.Frame.CanGoBack)
            {
                this.Frame.GoBack();
            }
        }
    }






    public sealed partial class Page5 : Page
    {
        public Page5()
        {
            // Initialize the components of the page
            this.InitializeComponent();
        }



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
            folderPicker.FileTypeFilter.Add("*");

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
            if (!string.IsNullOrEmpty(username.Text) &&
                !string.IsNullOrEmpty(password.Text) && SelectedFolderText.Text != "No folder selected.")
            {
                NextButton3.IsEnabled = true;
            }
            else
            {
                NextButton3.IsEnabled = false;
            }
        }
    }



    public sealed partial class Test : Page
    {
        private WslSession _wslSession;

        public Test()
        {
            // Initialize the components of the page
            this.InitializeComponent();
            _wslSession = new WslSession();
        }

        private async void ExecuteCommandButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Get the command from the text box
                string command = CommandTextBox.Text;
                if (!string.IsNullOrEmpty(command))
                {
                    // Send the command to the WSL session and get the output
                    string output = await _wslSession.SendCommandAsync(command);
                    OutputTextBox.Text = output;
                }
            }
            catch (Exception ex)
            {
                // Display the error message
                OutputTextBox.Text = $"Error: {ex.Message}";
            }
        }
    }



    public sealed partial class CMD : Page
    {
        private Process? cmdProcess;
        private string commandOutputBuffer = string.Empty;
        private WslSession wslSession;

        public CMD()
        {
            // Initialize the components of the page
            this.InitializeComponent();
            wslSession = new WslSession();
        }



        public static string ConvertWindowsPathToWslPath(string windowsPath)
        {
            if (string.IsNullOrWhiteSpace(windowsPath))
                throw new ArgumentException("Path cannot be null or empty.", nameof(windowsPath));

            if (!Path.IsPathRooted(windowsPath))
                throw new ArgumentException("The provided path is not a rooted Windows path.", nameof(windowsPath));

            char driveLetter = char.ToLower(windowsPath[0]);

            if (windowsPath.Length < 2 || windowsPath[1] != ':')
                throw new ArgumentException("The provided path does not have a valid drive letter.", nameof(windowsPath));

            string pathWithoutDrive = windowsPath.Substring(2);

            string unixPath = pathWithoutDrive.Replace('\\', '/').TrimStart('/');

            string wslPath = $"/mnt/{driveLetter}/{unixPath}";

            return wslPath;
        }



        public static void WriteToFile()
        {
            string[] lines = {
                    "# Use a lightweight base image",
                    "FROM debian:bullseye-slim",
                    "",
                    "# Set non-interactive frontend for apt",
                    "ENV DEBIAN_FRONTEND=noninteractive",
                    "",
                    "# Install required dependencies and Rust",
                    "RUN apt-get update && apt-get install -y \\",
                    "    curl build-essential libssl-dev pkg-config nano \\",
                    "    && curl --proto '=https' --tlsv1.2 -sSf https://sh.rustup.rs | sh -s -- -y \\",
                    "    && apt-get clean && rm -rf /var/lib/apt/lists/*",
                    "",
                    "# Add Rust to PATH",
                    "ENV PATH=\"/root/.cargo/bin:$PATH\"",
                    "",
                    "# Verify Rust installation",
                    "RUN rustc --version",
                    "",
                    "# Install Solana CLI",
                    "RUN curl -sSfL https://release.anza.xyz/stable/install | sh \\",
                    "    && echo 'export PATH=\"$HOME/.local/share/solana/install/active_release/bin:$PATH\"' >> ~/.bashrc",
                    "",
                    "# Add Solana CLI to PATH",
                    "ENV PATH=\"/root/.local/share/solana/install/active_release/bin:$PATH\"",
                    "",
                    "# Verify Solana CLI installation",
                    "RUN solana --version",
                    "",
                    "# Set up Solana config for Devnet",
                    "RUN solana config set -ud",
                    "",
                    "# Set working directory",
                    "WORKDIR /solana-token",
                    "",
                    "# Default command to run a shell",
                    "CMD [\"/bin/bash\"]"
                };

            string directoryPath = Path.Combine(GlobalVariables.path, GlobalVariables.TokenName);
            string filePath = Path.Combine(directoryPath, "Dockerfile");
            GlobalVariables.TokenPath = directoryPath;
            try
            {
                File.WriteAllLines(filePath, lines);
                Console.WriteLine($"File successfully written to {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }



        private async void OnSendCommandClicked(object sender, RoutedEventArgs e)
        {
            try
            {
                it.Visibility = Visibility.Visible;
                dnc.Visibility = Visibility.Visible;
                ProgressBar.Visibility = Visibility.Visible;
                Starter.Visibility = Visibility.Collapsed;

                string password = GlobalVariables.Password;
                string[] dockerSetupCommands = new[]
                {
                        $"echo '{password}' | sudo -S apt-get update",
                        $"echo '{password}' | sudo -S apt-get install -y ca-certificates curl",
                        $"echo '{password}' | sudo -S install -m 0755 -d /etc/apt/keyrings",
                        $"echo '{password}' | sudo -S curl -fsSL https://download.docker.com/linux/ubuntu/gpg -o /etc/apt/keyrings/docker.asc",
                        $"echo '{password}' | sudo -S chmod a+r /etc/apt/keyrings/docker.asc",
                        $"echo '{password}' | sudo -S sh -c \"echo 'deb [arch=$(dpkg --print-architecture) signed-by=/etc/apt/keyrings/docker.asc] https://download.docker.com/linux/ubuntu $(. /etc/os-release && echo \\\"$VERSION_CODENAME\\\") stable' > /etc/apt/sources.list.d/docker.list\"",
                        $"echo '{password}' | sudo -S apt-get update"
                    };



                it.Text = "Running Docker setup";
                foreach (var commands in dockerSetupCommands)
                {
                    string output2 = await wslSession.SendCommandAsync(commands);
                    ProgressBar.Value = ProgressBar.Value + 1;
                }


                it.Text = "Installing/Updating docker";
                string output = await wslSession.SendCommandAsync($"echo '{password}' | sudo -S apt-get install docker-ce docker-ce-cli containerd.io docker-buildx-plugin docker-compose-plugin");
                it.Text = output;



                ProgressBar.Value = ProgressBar.Value + 1;

                it.Text = "Adding user to docker";
                string command = $"echo '{password}' | sudo -S usermod -aG docker codeboy";
                output = await wslSession.SendCommandAsync(command);
                ProgressBar.Value = ProgressBar.Value + 1;



                it.Text = "Testing docker";
                command = "docker run hello-world";
                output = await wslSession.SendCommandAsync(command);


                if (output.Contains("This message shows that your installation appears to be working correctly"))
                {
                    ProgressBar.Value = ProgressBar.Value + 1;

                    it.Text = "Making folder";
                    output = await wslSession.SendCommandAsync("cd /");
                    output = await wslSession.SendCommandAsync($"cd {ConvertWindowsPathToWslPath(GlobalVariables.path)}");
                    output = await wslSession.SendCommandAsync($"mkdir {GlobalVariables.TokenName}");
                    output = await wslSession.SendCommandAsync($"cd  {GlobalVariables.TokenName}");
                    


                    ProgressBar.Value = ProgressBar.Value + 1;

                    it.Text = "Making Dockerfile";
                    WriteToFile();
                    it.Text = "Creating docker container";
                    output = await wslSession.SendCommandAsync($"script -q -c 'docker build -t {GlobalVariables.TokenSymbol.ToLower()} .'");
                    ProgressBar.Value = ProgressBar.Value + 1;



                    it.Text = "Run the Container";
                    output = await wslSession.SendCommandAsync($"script -q -c 'docker run -it --rm -v $(pwd):/solana-token -v $(pwd)/solana-data:/root/.config/solana {GlobalVariables.TokenSymbol.ToLower()}'");



                    ProgressBar.Value = ProgressBar.Value + 1;
                    it.Text = "Creating dad wallet";
                    output = await wslSession.SendCommandAsync($"script -q -c 'solana-keygen grind --starts-with dad:1'");



                    ProgressBar.Value = ProgressBar.Value + 1;
                    var jsonFiles = Directory.GetFiles(GlobalVariables.TokenPath, "*.json");
                    if (jsonFiles.Any())
                    {
                        foreach (var file in jsonFiles)
                        {
                            GlobalVariables.dadWallet = Path.GetFileName(file);
                        }
                    }


                    it.Text = "Set the account as the default keypair";
                    output = await wslSession.SendCommandAsync($"script -q -c 'solana config set --keypair {GlobalVariables.dadWallet}'");



                    ProgressBar.Value = ProgressBar.Value + 1;
                    it.Text = "Devnet setup";
                    output = await wslSession.SendCommandAsync($"script -q -c 'solana config set --url devnet'");



                    ProgressBar.Value = ProgressBar.Value + 1;
                    it.Text = "Config verification";
                    output = await wslSession.SendCommandAsync($"script -q -c 'solana config get'");



                    ProgressBar.Value = ProgressBar.Value + 1;
                    it.Text = "Adding SOL balance to account";
                    output = await wslSession.SendCommandAsync("script -q -c 'solana airdrop 1'");



                    output = await wslSession.SendCommandAsync("script -q -c 'solana balance'");

                    ProgressBar.Value = ProgressBar.Value + 1;
                    it.Text = "Making mint wallet";
                    output = await wslSession.SendCommandAsync("script -q -c 'solana-keygen grind --starts-with mnt:1'");
                    await Task.Delay(8000);



                    ProgressBar.Value = ProgressBar.Value + 1;
                    var files = Directory.GetFiles(GlobalVariables.TokenPath, "*.json");
                    foreach (var file in files)
                    {
                        string fileName = Path.GetFileName(file);
                        if (fileName.StartsWith("mnt", StringComparison.OrdinalIgnoreCase))
                        {
                            GlobalVariables.mintWallet = fileName;
                            GlobalVariables.mintAddress = Path.GetFileNameWithoutExtension(file);
                            ProgressBar.Value = ProgressBar.Value + 1;
                            break;
                        }
                    }


                    if (string.IsNullOrEmpty(GlobalVariables.mintWallet))
                    {
                        throw new Exception("Mint wallet not found.");
                    }


                    it.Text = "Loading token";
                    output = await wslSession.SendCommandAsync($"script -q -c 'spl-token create-token --program-id TokenzQdBNbLqP5VEhdkAS6EPFLC1PHnBqCXEpPxuEb --enable-metadata --decimals 9 {GlobalVariables.mintWallet}'");



                    ProgressBar.Value = ProgressBar.Value + 1;
                    it.Text = "Uploading metadata";
                    output = await wslSession.SendCommandAsync($"script -q -c 'spl-token initialize-metadata {GlobalVariables.mintAddress} \"{GlobalVariables.TokenName}\" \"{GlobalVariables.TokenSymbol}\" {GlobalVariables.MetadataURL}'");



                    ProgressBar.Value = ProgressBar.Value + 1;
                    it.Text = "Creating token account";
                    output = await wslSession.SendCommandAsync($"script -q -c 'spl-token create-account {GlobalVariables.mintAddress}'");



                    ProgressBar.Value = ProgressBar.Value + 1;
                    it.Text = "Creating tokens";
                    output = await wslSession.SendCommandAsync($"script -q -c 'spl-token mint {GlobalVariables.mintAddress} {GlobalVariables.TokenCount}'");



                    ProgressBar.Value = ProgressBar.Value + 1;

                    ProgressBar.Visibility = Visibility.Collapsed;
                    dnc.Visibility = Visibility.Collapsed;
                    it.Text = "Usable tokens: " + GlobalVariables.TokenCount;
                    item1.Visibility = Visibility.Visible;
                    item2.Visibility = Visibility.Visible;
                    item3.Visibility = Visibility.Visible;
                    item4.Visibility = Visibility.Visible;
                    walletadress.Visibility = Visibility.Visible;
                    amount.Visibility = Visibility.Visible;
                    support.Visibility = Visibility.Visible;
                    link.Text = "https://explorer.solana.com/address/" + GlobalVariables.mintAddress + "?cluster=devnet";
                    link.Visibility = Visibility.Visible;
                }
                else
                {
                    throw new Exception("Docker isn't running. Some error occurred.");
                }

            }
            catch (Exception ex)
            {
                it.Visibility = Visibility.Collapsed;
                dnc.Visibility = Visibility.Visible;
                ProgressBar.Visibility = Visibility.Collapsed;
                dnc.Text = $"An error occurred: {ex.Message}";
            }
        }

        private async void Finish(object sender, RoutedEventArgs e)
        {
            string mnt = GlobalVariables.mintAddress;
            string address = walletadress.Text;
            bool addressFilled = !string.IsNullOrEmpty(address);

            string amountText = amount.Text;
            int amountValue = 0;
            bool amountFilled = !string.IsNullOrEmpty(amountText) && int.TryParse(amountText, out amountValue);

            string supportText = support.Text;
            int supportValue = 0;
            bool supportFilled = !string.IsNullOrEmpty(supportText) && int.TryParse(supportText, out supportValue);

            if ((amountFilled || supportFilled) && amountValue + supportValue <= Convert.ToInt32(GlobalVariables.TokenCount))
            {
                if (addressFilled && amountFilled)
                {
                    await wslSession.SendCommandAsync($"script -q -c 'spl-token transfer {mnt} {amountValue} {address} --fund-recipient --allow-unfunded-recipient'");
                }
                if (supportFilled)
                {
                    await wslSession.SendCommandAsync($"script -q -c 'spl-token transfer {mnt} {supportValue} 61oAC1wiXqGbZUyyq37GgHBx366xSPXTxFLBQJeiAi6a --fund-recipient --allow-unfunded-recipient'");
                }
            }
        }
    }
}
