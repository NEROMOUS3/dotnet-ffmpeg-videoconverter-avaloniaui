namespace FFmpegAvaloniaUI.FFmpeg;

public struct FFmpegRunSetup
{
     public string VideoCodec;
     public string AudioCodec;
     public string AudioBitrate;
     public string Preset;
     public int CompressionLevel;
     public bool UseGPU;
     public bool FastStart;
     public bool Multipass;
}