using System.IO;

namespace FFmpegAvaloniaUI.WindowsSelector;

public static class WinPathUtils
{
    public static bool IsPathSafe(string filePath)
    {
        if(string.IsNullOrWhiteSpace(filePath)) return false;
        char[] invalidChars = Path.GetInvalidPathChars();
        if (filePath.IndexOfAny(invalidChars) >= 0)
            return false;
        
        string fullPath = Path.GetFullPath(filePath);
        
        if(!DirectoryExists(fullPath)) return false;
        return File.Exists(fullPath);
    }
    
    public static string GetDirectory(string path)
    {
        if(string.IsNullOrWhiteSpace(path)) return string.Empty;
        var directoryPath = Path.GetDirectoryName(path);
        if(directoryPath == null) return string.Empty;
        return directoryPath;
    }
    
    public static bool DirectoryExists(string path)
    {
        if(string.IsNullOrWhiteSpace(path)) return true;
        return Directory.Exists(GetDirectory(path));
    }
}