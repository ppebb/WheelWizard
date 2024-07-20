using System.Windows;
using CT_MKWII.Common.UI;
using CT_MKWII.Common.Updater;

namespace CT_MKWII.WPF
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            UIProvider.ImageFactory = new BitmapImageWrapperFactory();
            UIProvider.ProgressWindowFactory = new ProgressWindowFactory();
            UIProvider.DialogBox = new DialogBoxWrapper();
            VersionChecker.CheckForUpdates();
        }
    }
}
