using System.IO;

namespace FFmpegAvaloniaUI.WindowsSelector;

public static class WinPathUtils
{
    public static bool IsPathSafe(string filePath)
    {
        char[] invalidChars = Path.GetInvalidPathChars();
        if (filePath.IndexOfAny(invalidChars) >= 0)
            return false;
        
        string fullPath = Path.GetFullPath(filePath);
        
        if (!File.Exists(fullPath))
            return false;
    
        return true;
    }
    
    public static string? GetDirectory(string filePath)
    {
        return IsPathSafe(filePath) ? Path.GetDirectoryName(filePath) : string.Empty;
    }
}