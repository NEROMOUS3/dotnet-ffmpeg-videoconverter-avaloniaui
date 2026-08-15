using System.Text;
using FFmpegAvaloniaUI.PowerShell;

namespace FFmpegAvaloniaUI.FFmpeg;

public class FFmpegRunCommandBuilder
{
    public string CreateCommand(string sourceName, string outputName, FFmpegRunSetup setup)
    {
        var resultCommand = new StringBuilder();
        resultCommand.Append(ShellConst.FFmpeg);
        resultCommand.Append(" -i " + sourceName);
        resultCommand.Append(" -c:v " + setup.VideoCodec);
        resultCommand.Append(" -preset " + setup.Preset);
        if (setup.UseGPU)
            resultCommand.Append(" -cq " + setup.CompressionLevel);
        else
            resultCommand.Append(" -crf " + setup.CompressionLevel);
        resultCommand.Append(" -c:a " + setup.AudioCodec);
        resultCommand.Append(" -b:a " + setup.AudioBitrate);
        resultCommand.Append(" -pix_fmt yuv420p");
        if(setup.Multipass)
            resultCommand.Append(" -multipass fullres");
        if (setup.FastStart)
            resultCommand.Append(" -movflags +faststart");
        resultCommand.Append(" "+outputName);
        return resultCommand.ToString();
    }
}