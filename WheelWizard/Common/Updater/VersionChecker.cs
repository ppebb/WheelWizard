using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using CT_MKWII.Common.UI;

namespace CT_MKWII.Common.Updater;

public static class VersionChecker
{
    private const string VersionFileURL = "https://raw.githubusercontent.com/patchzyy/WheelWizard/main/version.txt";

    public const string CurrentVersion = "1.1.2";


    public static void CheckForUpdates()
    {
        using var client = new WebClient();

        try
        {
            var version = client.DownloadString(VersionFileURL).Trim();
            if (version != CurrentVersion)
            {
                var result = UIProvider.DialogBox.Show("A new version of WheelWizard is available. Would you like to update?", "Update Available", DialogBoxButtons.YesNo);
                if (result == DialogBoxResponse.Yes)
                {
                    Update();
                }
            }
        }
        catch (Exception e)
        {
            UIProvider.DialogBox.Show("An error occurred while checking for updates. Please try again later. \n \nError: " + e.Message);
        }

    }

    private static string GetActualExecutablePath()
    {
        // Use the process module's filename, which should be the actual .exe path
        using var process = Process.GetCurrentProcess();
        return process.MainModule.FileName;
    }

    public static async Task Update()
    {
        string currentLocation = GetActualExecutablePath();
        var currentFolder = Path.GetDirectoryName(currentLocation);
        var downloadUrl = "https://github.com/patchzyy/WheelWizard/releases/latest/download/WheelWizard.exe";
        var newFilePath = Path.Combine(currentFolder, "CT-MKWII-WPF_new.exe");

        var progressWindow = UIProvider.ProgressWindowFactory.NewProgressWindow();
        progressWindow.Show();

        // Download the new file
        await DownloadUtils.DownloadFileWithWindow(downloadUrl, newFilePath, progressWindow);

        //wait 0.2 seconds
        await Task.Delay(200);
        // Create and run the batch file
        CreateAndRunBatchFile(currentLocation, newFilePath);

        // Close the current program
        Environment.Exit(0);
    }

    // TODO: This definitely isn't cross platform...
    private static void CreateAndRunBatchFile(string currentFilePath, string newFilePath)
    {
        var batchFilePath = Path.Combine(Path.GetDirectoryName(currentFilePath), "update.bat");
        var originalFileName = Path.GetFileName(currentFilePath);
        var newFileName = Path.GetFileName(newFilePath);

        var batchContent = @"
@echo off
timeout /t 2 /nobreak
del """ + originalFileName + @"""
rename """ + newFileName + @""" """ + originalFileName + @"""
start """" """ + originalFileName + @"""
del ""%~f0""
";

        File.WriteAllText(batchFilePath, batchContent);

        Process.Start(new ProcessStartInfo(batchFilePath) { CreateNoWindow = true });
    }

}
