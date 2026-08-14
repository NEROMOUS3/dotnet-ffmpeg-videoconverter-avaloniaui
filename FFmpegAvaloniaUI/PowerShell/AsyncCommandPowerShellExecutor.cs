using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace FFmpegAvaloniaUI.PowerShell;

public class AsyncCommandPowerShellExecutor: IAsyncCommandExecutor, IDisposable
{
    private Process _process;
    private bool _isRunning = false;

    public bool IsRunning => _isRunning;

    public async Task ExecuteCommand(string command, CancellationToken cancellationToken = default)
    {
        if (_isRunning) return;
        _isRunning = true;

        try
        {
            using Process process = new Process();
            process.StartInfo.FileName = ShellConst.PowerShellExecutor;
            var encodedCommand = Convert.ToBase64String(System.Text.Encoding.Unicode.GetBytes(command));
            process.StartInfo.Arguments = $"{ShellConst.NoExit} {ShellConst.EncodedCommand} {encodedCommand}";
            process.StartInfo.UseShellExecute = true;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                
            process.StartInfo.RedirectStandardOutput = false;
            process.StartInfo.RedirectStandardError = false;
            process.StartInfo.RedirectStandardInput = false;

            process.Start();
                
            await process.WaitForExitAsync(cancellationToken);
        }
        finally
        {
            _isRunning = false;
        }
    }
    
    public void Dispose()
    {
        _process?.Dispose();
    }
}