using System;
using Avalonia.Controls;
using FFmpegAvaloniaUI.WindowsSelector;

namespace FFmpegAvaloniaUI;

public class ViewVariableProvider
{
    private const int DEFAULT_COMPLETION_LEVEL = 20; 
    private const string DEFAULT_RESULT_FILE_NAME = "Output"; 
    
    private TextBox _outputFileNameTextBox;
    private CheckBox _useGpuCheckBox;
    private CheckBox _fastStartCheckBox;
    private CheckBox _multipassCheckBox;
    private ComboBox _videoCodecComboBox;
    private ComboBox _resultFileFormat;
    private ComboBox _audioCodecComboBox;
    private ComboBox _audioBitrateComboBox;
    private ComboBox _presetComboBox;
    private NumericUpDown _compressionLevel;
    
    public bool UseGpu => GetComboBoxValue(_useGpuCheckBox, true);
    public bool FastStart => GetComboBoxValue(_fastStartCheckBox, false);
    public bool MultiPass => GetComboBoxValue(_multipassCheckBox, false);
    
    public string ResultFileName => GetResultFileName(_outputFileNameTextBox);
    
    public int VideoCodecOption => GetSelectorValue(_videoCodecComboBox);
    public int ResultFormatOption => GetSelectorValue(_resultFileFormat);
    public int AudioCodecOption => GetSelectorValue(_audioCodecComboBox);
    public int AudioBitrateOption => GetSelectorValue(_audioBitrateComboBox);
    public int CompressionLevelOption =>Convert.ToInt32(_compressionLevel.Value);
    public int PresetOption => GetSelectorValue(_presetComboBox);
    
    public ViewVariableProvider(Avalonia.Controls.Window mainWindow)
    {
       
        _outputFileNameTextBox = mainWindow.FindControl<TextBox>(ViewComponentsNames.ResultNameTextBox);
        
        _useGpuCheckBox =  mainWindow.FindControl<CheckBox>(ViewComponentsNames.UseGpuCheckBox);
        _fastStartCheckBox =  mainWindow.FindControl<CheckBox>(ViewComponentsNames.FastStartCheckBox);
        _multipassCheckBox =  mainWindow.FindControl<CheckBox>(ViewComponentsNames.MultipassCheckBox);

        _videoCodecComboBox = mainWindow.FindControl<ComboBox>(ViewComponentsNames.VideoCodecSelector);
        _audioCodecComboBox = mainWindow.FindControl<ComboBox>(ViewComponentsNames.AudioCodecSelector);
        _resultFileFormat = mainWindow.FindControl<ComboBox>(ViewComponentsNames.ResultFormatSelector);
        _audioBitrateComboBox = mainWindow.FindControl<ComboBox>(ViewComponentsNames.AudioBitrateSelector);

        _compressionLevel = mainWindow.FindControl<NumericUpDown>(ViewComponentsNames.CompressionLevelSelector);
        _presetComboBox = mainWindow.FindControl<ComboBox>(ViewComponentsNames.PresetSelector);
    }

    private int GetSelectorValue(ComboBox? comboBox)
    {
        if(comboBox == null) return 0;
        return (int)comboBox.SelectedIndex;
    }
    
    private bool GetComboBoxValue(CheckBox? checkBox, bool defaultValue)
    {
        if (checkBox == null)
        {
            return defaultValue;
        }
        return (bool)checkBox.IsChecked!;
    }

    private string GetResultFileName(TextBox ? textBox)
    {
        if (textBox == null) return DEFAULT_RESULT_FILE_NAME;
        if(string.IsNullOrWhiteSpace(textBox.Text))  return DEFAULT_RESULT_FILE_NAME;
        return textBox.Text;
    }
}