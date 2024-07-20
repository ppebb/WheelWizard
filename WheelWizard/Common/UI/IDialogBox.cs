namespace CT_MKWII.Common.UI;

public interface IDialogBox
{
    public DialogBoxResponse Show(string message, string? caption = null, DialogBoxButtons? buttons = null, DialogBoxIcon? icon = null);
}

// Matches the WPF dialog responses. See https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.dialogresult?view=windowsdesktop-8.0
public enum DialogBoxResponse
{
    None = 0,
    OK,
    Cancel,
    Abort,
    Retry,
    Ignore,
    Yes,
    No,
    TryAgain = 10,
    Continue,
}

// Matches the WPF message box buttons. https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.messageboxbuttons?view=windowsdesktop-8.0
public enum DialogBoxButtons
{
    OK = 0,
    OKCancel,
    AbortRetryIgnore,
    YesNoCancel,
    YesNo,
    RetryCancel,
    CancelTryContinue
}

// Matches the WPF message box image. https://learn.microsoft.com/en-us/dotnet/api/system.windows.messageboximage?view=windowsdesktop-8.0
public enum DialogBoxIcon
{
    None = 0,
    Hand = 16,
    Error = 16,
    Stop = 16,
    Question = 32,
    Exclamation = 48,
    Warning = 48,
    Asterisk = 64,
    Information = 64,
}
