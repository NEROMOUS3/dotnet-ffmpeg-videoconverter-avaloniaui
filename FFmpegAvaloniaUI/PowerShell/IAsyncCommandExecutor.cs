using System.Threading;
using System.Threading.Tasks;

namespace FFmpegAvaloniaUI.PowerShell;

public interface IAsyncCommandExecutor
{
    public Task ExecuteCommand(string command, CancellationToken cancellationToken = default);
    public bool IsRunning { get; }
}