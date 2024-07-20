using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Diagnostics;
using CT_MKWII.Common.UI;
using static CT_MKWII.Common.Platform.Platform;

namespace CT_MKWII.Common;

public class DownloadUtils
{
    public static async Task DownloadFileWithWindow(string url, string filePath, IProgressWindow progressWindow,
        string extratext = "")
    {
        string directory = Path.GetDirectoryName(filePath);
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        try
        {
            using HttpClient client = new();

            using var response = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);

            var totalBytes = response.Content.Headers.ContentLength ?? -1;
            using var downloadStream = await response.Content.ReadAsStreamAsync();
            using var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None,
                    bufferSize: 8192, useAsync: true);

            var downloadedBytes = 0L;
            var bufferSize = 8192;
            var buffer = new byte[bufferSize];
            int bytesRead;
            var stopwatch = Stopwatch.StartNew();

            while ((bytesRead = await downloadStream.ReadAsync(buffer.AsMemory(0, bufferSize))) != 0)
            {
                await fileStream.WriteAsync(buffer.AsMemory(0, bytesRead));
                downloadedBytes += bytesRead;

                var progress = totalBytes == -1 ? 0 : (int)((float)downloadedBytes / totalBytes * 100);
                var downloadedMB = downloadedBytes / (1024.0 * 1024.0);
                var totalMB = totalBytes / (1024.0 * 1024.0);
                var status = $"Downloading... {downloadedMB:F2}/{totalMB:F2} MB";

                var elapsedSeconds = stopwatch.Elapsed.TotalSeconds;
                var speedMBps = downloadedMB / elapsedSeconds;
                var remainingMB = totalMB - downloadedMB;
                var estimatedTimeRemaining = remainingMB / speedMBps;

                var bottomText =
                    $"Speed: {speedMBps:F2} MB/s | Estimated time remaining: {FormatTimeSpan(estimatedTimeRemaining)}";

                ExecuteInMainContext(() =>
                {
                    progressWindow.SetProgress(progress, status, extratext);
                    progressWindow.SetExtraText(bottomText);
                });
            }
        }
        catch (Exception x)
        {
            UIProvider.DialogBox.Show($"An error occurred while downloading the file: {x.Message}");
            return;
        }
    }

    private static string FormatTimeSpan(double seconds)
    {
        var timeSpan = TimeSpan.FromSeconds(seconds);
        if (timeSpan.TotalHours >= 1)
            return $"{timeSpan.TotalHours:F1} hours";
        else if (timeSpan.TotalMinutes >= 1)
            return $"{timeSpan.TotalMinutes:F1} minutes";
        else
            return $"{timeSpan.TotalSeconds:F0} seconds";
    }
}
