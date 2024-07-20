using System;
using System.IO;

namespace CT_MKWII.Common.Platform;

public static class Unix
{

    internal static string DataPath() => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CT-MKWII");
}
