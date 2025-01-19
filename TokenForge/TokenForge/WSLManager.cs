using System;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using TokenForge;

public class WslSession
{
    private Process _process;
    private StringBuilder _outputBuilder;

    public WslSession()
    {
        StartBash();
    }

    private void StartBash()
    {
        _process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "wsl.exe",
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            }
        };

        _outputBuilder = new StringBuilder();

        _process.OutputDataReceived += (sender, e) =>
        {
            if (e.Data != null)
            {
                _outputBuilder.AppendLine(e.Data);
            }
        };

        _process.ErrorDataReceived += (sender, e) =>
        {
            if (e.Data != null)
            {
                _outputBuilder.AppendLine(e.Data);
            }
        };

        _process.Start();
        _process.BeginOutputReadLine();
        _process.BeginErrorReadLine();
    }

    public async Task<string> SendCommandAsync(string command)
    {
        string previous_out = "null";
        if (_process == null || _process.HasExited)
        {
            throw new InvalidOperationException("The WSL session is not running.");
        }

        _outputBuilder.Clear(); // Clear previous output

        // Ensure the command ends with a newline appropriate for Unix systems
        if (!command.EndsWith("\n"))
        {
            command += "\n";
        }

        _process.StandardInput.Write(command);
        _process.StandardInput.Flush();

        while (previous_out != _outputBuilder.ToString())
        {
            previous_out = _outputBuilder.ToString();
            await Task.Delay(3500);
        }
        // Giving some time for the command to execute and capture output
        
        return _outputBuilder.ToString();
    }
}
