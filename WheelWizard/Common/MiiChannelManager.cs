using System;
using System.IO;
using System.Threading.Tasks;
using CT_MKWII.Common.DolphinHelpers;
using CT_MKWII.Common.UI;

namespace CT_MKWII.Common;

public class MiiChannelManager
{
    public static string GetSavedChannelLocation()
    {
        return SettingsUtils.GetWheelWizardAppPath() + "/MiiChannel.wad";
    }

    public async static Task LaunchMiiChannel()
    {
        //if mii channel isnt downloaded yet, download it
        if (!MiiChannelExists())
        {
            await DownloadMiiChannel();
            //todo: maybe remove?
            await Task.Delay(200);
        }
        //launch mii channel
        DolphinSettingHelper.LaunchDolphin($"-b \"{GetSavedChannelLocation()}\"");
    }

    private static bool MiiChannelExists()
    {
        return File.Exists(GetSavedChannelLocation());
    }

    public async static Task DownloadMiiChannel()
    {
        try
        {
            var progressWindow = UIProvider.ProgressWindowFactory.NewProgressWindow();
            progressWindow.Show();
            await DownloadUtils.DownloadFileWithWindow("https://repo.mariocube.com/WADs/Other/Mii%20Channel%20Symbols%20-%20HACS.wad", GetSavedChannelLocation(), progressWindow);
            progressWindow.Close();
        }
        catch (Exception e)
        {
            UIProvider.DialogBox.Show("An error occurred while downloading the Mii Channel. Please try again later. \n \nError: " + e.Message);
        }
    }
}
