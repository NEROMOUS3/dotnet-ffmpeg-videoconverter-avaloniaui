namespace FFmpegAvaloniaUI.FFmpeg;

public static class FFmpegOptionSetup
{
    public static string[] AudioCodecSetup { get;} =
    [
        "aac",
    ];

    public static string[] VideoCodecSetup { get;} =
    [
        "libx264",
        "libx265",
        "hevc_nvenc",
        "hevc_amf",
    ];

    public static string[] PresetSetup { get;} =
    [
        "ultrafast",
        "fast",
        "medium",
        "slow",
        "veryslow",
        "p1",
        "p4",
        "p6",
        "p7"
    ];

    public static string[] AudioBitrateSetup { get;} =
    [
        "320k",
        "128k",
    ];

    public static string[] OutputFormatSetup { get;} =
    [
        ".mp4",
        ".mkv",
        ".avi",
        ".mov"
    ];
}