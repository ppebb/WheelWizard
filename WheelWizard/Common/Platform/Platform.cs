using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

#if IS_WIN
using static CT_MKWII.Common.Platform.Windows;
#else
using static CT_MKWII.Common.Platform.Unix;
#endif

namespace CT_MKWII.Common.Platform;

public static class Platform
{
    public static SynchronizationContext? Context { get; set; }

    // TODO: Move somewhere else...?
    public static void ExecuteInMainContext(Action action)
    {
        using ManualResetEventSlim resetEvent = new(false);

        if (Context != null)
            Context.Post(_ =>
            {
                action();
                resetEvent.Set();
            }, null);
        else
            Task.Factory.StartNew(() =>
            {
                action();
                resetEvent.Set();
            });

        resetEvent.Wait();
    }

    public static readonly string DataPath = DataPath();

    public static readonly string ConfigPath = Path.Combine(DataPath, "config.json");
}
