using System;
using System.IO;

namespace CT_MKWII.Common.Platform;

public static class Windows
{
    internal static string DataPath() => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "CT-MKWII");
}
