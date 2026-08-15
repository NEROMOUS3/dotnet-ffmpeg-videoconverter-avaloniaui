using System;
using System.IO;
using System.Windows.Forms;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using FFmpegAvaloniaUI.FFmpeg;
using FFmpegAvaloniaUI.FileExplorer;
using FFmpegAvaloniaUI.PowerShell;
using FFmpegAvaloniaUI.WindowsSelector;
using Label = Avalonia.Controls.Label;

namespace FFmpegAvaloniaUI;

public partial class MainWindow : Avalonia.Controls.Window
{
    private IAsyncCommandExecutor _asyncCommandExecutor;
    private ViewVariableProvider _viewVariableProvider;
    private Label? _sourceFileLabel;
    private Label? _outputPathLabel;
    private string _sourcePath;
    private string _outputPath;
    
    public MainWindow()
    {
        _asyncCommandExecutor = new AsyncCommandPowerShellExecutor();
        InitializeComponent();
        _viewVariableProvider = new ViewVariableProvider(this);
        InitializeDefaultView();
    }

    private void InitializeDefaultView()
    {
        _sourceFileLabel = this.FindControl<Label>(ViewComponentsNames.SourcePathLabel);
        _outputPathLabel = this.FindControl<Label>(ViewComponentsNames.OutputPathLabel);
        UpdateLabelText(_sourceFileLabel, $"{StringConst.SOURCE_FILE} {StringConst.NONE}");
        UpdateLabelText(_outputPathLabel, $"{StringConst.OUTPUT_PATH} {StringConst.NONE}");
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
        _asyncCommandExecutor.ExecuteCommand(ShellConst.UninstallFFmpeg);
    }

    private void SelectSourceFile(object? sender, RoutedEventArgs e)
    {
        var filePath = WinFormsDialogHelper.ShowOpenFileDialog();
        if (WinPathUtils.IsPathSafe(filePath))
        {
            _sourcePath = filePath;
            _outputPath = WinPathUtils.GetDirectory(filePath);
            UpdateLabelText(_sourceFileLabel, $"{StringConst.SOURCE_FILE} {_sourcePath}");
            UpdateLabelText(_outputPathLabel, $"{StringConst.OUTPUT_PATH} {_outputPath}");
        }
        else
        {
            UpdateLabelText(_sourceFileLabel, $"{StringConst.SOURCE_FILE} {StringConst.NONE}");
        }
    }
    
    private void SelectOutputDirectory(object? sender, RoutedEventArgs e)
    {
        var outputDirectoryPath = WinFormsDialogHelper.ShowFolderBrowserDialog();
        if (WinPathUtils.DirectoryExists(outputDirectoryPath))
        {
            _outputPath = outputDirectoryPath;
        }
        else
        {
            if(!string.IsNullOrEmpty(_sourcePath))
                _outputPath = WinPathUtils.GetDirectory(_sourcePath);
            else
                _outputPath = StringConst.NONE;
           
        }
        UpdateLabelText(_outputPathLabel, $"{StringConst.OUTPUT_PATH} {_outputPath}");
    }

    private void RunVideoConverter(object? sender, RoutedEventArgs e)
    {
        if(_asyncCommandExecutor.IsRunning) return;

        if (string.IsNullOrEmpty(_sourcePath))
        {
            MessageBox.Show(StringConst.SOURCE_PATH_EXCEPTION, StringConst.ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        if (string.IsNullOrEmpty(_outputPath))
        {
            MessageBox.Show(StringConst.RESULT_PATH_EXCEPTION, StringConst.ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }
        
        try
        {
            var setup = new FFmpegRunSetup();
            setup.VideoCodec = FFmpegOptionSetup.VideoCodecSetup[_viewVariableProvider.VideoCodecOption];
            setup.AudioCodec = FFmpegOptionSetup.AudioCodecSetup[_viewVariableProvider.AudioCodecOption];
            setup.AudioBitrate = FFmpegOptionSetup.AudioBitrateSetup[_viewVariableProvider.AudioBitrateOption];
            setup.Preset = FFmpegOptionSetup.PresetSetup[_viewVariableProvider.PresetOption];
            setup.UseGPU = _viewVariableProvider.UseGpu;
            setup.FastStart = _viewVariableProvider.FastStart;
            setup.Multipass = _viewVariableProvider.MultiPass;
            setup.CompressionLevel = _viewVariableProvider.CompressionLevelOption;
            var outputFilePatn = Path.Combine(_outputPath, _viewVariableProvider.ResultFileName);
            outputFilePatn +=FFmpegOptionSetup.OutputFormatSetup[_viewVariableProvider.ResultFormatOption];

            var commandBuilder = new FFmpegRunCommandBuilder();
            var command = commandBuilder.CreateCommand(_sourcePath,  outputFilePatn,setup);
            Console.WriteLine(command);

            _asyncCommandExecutor.ExecuteCommand(command);
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            MessageBox.Show(exception.ToString(), StringConst.ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
    
    private void UpdateLabelText(Label? label, string text)
    {
        if(label == null) return;
        label.Content = text;
    }
}