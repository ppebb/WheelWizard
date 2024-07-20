using System.Threading.Tasks;

namespace CT_MKWII.Common;

public class RRStatusManager
{
    public static async Task<ActionButtonStatus> GetCurrentStatus()
    {
        var serverEnabled = await RetroRewindInstaller.IsServerEnabled();
        if (!serverEnabled) return ActionButtonStatus.NoServer;
        var configCorrectAndExists = SettingsUtils.ConfigCorrectAndExists();
        if (!configCorrectAndExists) return ActionButtonStatus.ConfigNotFinished;
        var retroRewindInstalled = RetroRewindInstaller.IsRetroRewindInstalled();
        if (!retroRewindInstalled) return ActionButtonStatus.noRR;
        bool retroRewindUpToDate;
        // string latestRRVersion;
        if (!SettingsUtils.IsConfigFileFinishedSettingUp()) return ActionButtonStatus.ConfigNotFinished;
        retroRewindUpToDate = await RetroRewindInstaller.IsRRUpToDate(RetroRewindInstaller.CurrentRRVersion());
        if (!retroRewindUpToDate) return ActionButtonStatus.OutOfDate;
        // latestRRVersion = await RetroRewindInstaller.GetLatestVersionString();
        return ActionButtonStatus.UpToDate;
    }
}
