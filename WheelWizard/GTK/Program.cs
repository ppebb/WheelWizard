using Gtk;

namespace CT_MKWII.GTK;

public static class Program
{
    public static void Main(string[] args)
    {
        using Application app = Application.New("patchzyy.wheelwizard", Gio.ApplicationFlags.FlagsNone);
        app.OnActivate += (sender, _) =>
        {
            Window win = new()
            {
                Application = (Application)sender,
                Title = "Wheel Wizard",
            };

            win.Show();
        };
        app.RunWithSynchronizationContext(args);
    }
}
