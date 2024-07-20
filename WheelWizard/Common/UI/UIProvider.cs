namespace CT_MKWII.Common.UI;

public static class UIProvider
{
    public static IDialogBox DialogBox { get; set; }
    public static ICreateProgressWindow ProgressWindowFactory { get; set; }
    public static ICreateImage ImageFactory { get; set; }
}
