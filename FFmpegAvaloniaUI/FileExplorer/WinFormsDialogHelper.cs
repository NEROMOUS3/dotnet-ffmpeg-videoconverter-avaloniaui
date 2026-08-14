using System;
using System.Windows.Forms;

namespace FFmpegAvaloniaUI.FileExplorer;

public static class WinFormsDialogHelper
{
    [STAThread]
    public static string ShowOpenFileDialog(string filter = "Все файлы (*.*)|*.*")
    {
        using (OpenFileDialog dialog = new OpenFileDialog())
        {
            dialog.Filter = filter;
            dialog.RestoreDirectory = true;
            
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                return dialog.FileName;
            }
            return null;
        }
    }
    
    [STAThread]
    public static string ShowFolderBrowserDialog(string description = "Select output folder")
    {
        using (FolderBrowserDialog dialog = new FolderBrowserDialog())
        {
            dialog.Description = description;
            dialog.ShowNewFolderButton = true;
            
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                return dialog.SelectedPath;
            }
            return null;
        }
    }
}