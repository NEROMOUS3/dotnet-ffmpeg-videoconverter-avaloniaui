using System;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using FFmpegAvaloniaUI.FileExplorer;
using FFmpegAvaloniaUI.PowerShell;
using FFmpegAvaloniaUI.WindowsSelector;

namespace FFmpegAvaloniaUI;

public partial class MainWindow : Avalonia.Controls.Window
{
    private IAsyncCommandExecutor _asyncCommandExecutor;
    
    public MainWindow()
    {
        _asyncCommandExecutor = new AsyncCommandPowerShellExecutor();
        InitializeComponent();
    }
    
    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
    

    private void InstallFFMpeg_OnClick(object? sender, RoutedEventArgs e)
    {
        if(_asyncCommandExecutor.IsRunning) return;
        _asyncCommandExecutor.ExecuteCommand(ShellConst.InstallFFmpeg);
    }
    
    private void UninstallFFMpeg_OnClick(object? sender, RoutedEventArgs e)
    {
        if(_asyncCommandExecutor.IsRunning) return;
        _asyncCommandExecutor.ExecuteCommand(ShellConst.InstallFFmpeg);
    }

    private void SelectFileButton_OnClick(object? sender, RoutedEventArgs e)
    {
       var file =  WinFormsDialogHelper.ShowOpenFileDialog();
       var isPathSafe = WinPathUtils.IsPathSafe(file);
       Console.WriteLine(file + " "+ isPathSafe);
       Console.WriteLine(WinPathUtils.GetDirectory(file));
    }
}