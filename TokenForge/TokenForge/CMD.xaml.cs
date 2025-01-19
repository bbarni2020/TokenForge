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


// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace TokenForge
{
    public sealed partial class CMD : Page
    {
        private Process? cmdProcess;
        private string commandOutputBuffer = string.Empty;
        private WslSession wslSession;

        public CMD()
        {
            this.InitializeComponent();
            wslSession = new WslSession();
        }

        public static string ConvertWindowsPathToWslPath(string windowsPath)
        {
            if (string.IsNullOrWhiteSpace(windowsPath))
                throw new ArgumentException("Path cannot be null or empty.", nameof(windowsPath));

            // Check if the path is rooted (i.e., starts with a drive letter)
            if (!Path.IsPathRooted(windowsPath))
                throw new ArgumentException("The provided path is not a rooted Windows path.", nameof(windowsPath));

            // Extract the drive letter
            char driveLetter = char.ToLower(windowsPath[0]);

            // Ensure the path starts with a drive letter followed by ':'
            if (windowsPath.Length < 2 || windowsPath[1] != ':')
                throw new ArgumentException("The provided path does not have a valid drive letter.", nameof(windowsPath));

            // Remove the drive letter and colon from the path
            string pathWithoutDrive = windowsPath.Substring(2);

            // Replace backslashes with forward slashes
            string unixPath = pathWithoutDrive.Replace('\\', '/').TrimStart('/');

            // Construct the WSL path
            string wslPath = $"/mnt/{driveLetter}/{unixPath}";

            return wslPath;
        }

        public static void WriteToFile()
        {
            // Define the content to be written to the file
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

            // Combine the path and file name to create the full file path
            string directoryPath = Path.Combine(GlobalVariables.path, GlobalVariables.TokenName);
            string filePath = Path.Combine(directoryPath, "Dockerfile");
            GlobalVariables.TokenPath = directoryPath;
            try
            {
                // Write the lines to the file
                File.WriteAllLines(filePath, lines);
                Console.WriteLine($"File successfully written to {filePath}");
            }
            catch (Exception ex)
            {
                // Handle any exceptions that may occur
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
                // Handle any exceptions that occur during command execution
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
