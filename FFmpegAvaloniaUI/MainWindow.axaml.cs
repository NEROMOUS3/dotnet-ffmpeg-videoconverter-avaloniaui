using Avalonia.Controls;
using Avalonia.Interactivity;
using FFmpegAvaloniaUI.PowerShell;

namespace FFmpegAvaloniaUI;

public partial class MainWindow : Window
{
    private IAsyncCommandExecutor _asyncCommandExecutor;
    
    public MainWindow()
    {
        _asyncCommandExecutor = new AsyncCommandPowerShellExecutor();
        InitializeComponent();
    }

    private void PShellCall_OnClick(object? sender, RoutedEventArgs e)
    {
        if(_asyncCommandExecutor.IsRunning) return;
        _asyncCommandExecutor.ExecuteCommand(ShellConst.GetNetTCPConnection);
    }

    private void FFmpegInstall_OnClick(object? sender, RoutedEventArgs e)
    {
        if(_asyncCommandExecutor.IsRunning) return;
        _asyncCommandExecutor.ExecuteCommand(ShellConst.InstallFFmpeg);
    }
}