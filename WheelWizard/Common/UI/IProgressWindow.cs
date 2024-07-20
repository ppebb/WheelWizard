namespace CT_MKWII.Common.UI;

// Stupid interface so IProgressWindows are constructable without knowing what type they actually are
public interface ICreateProgressWindow
{
    IProgressWindow NewProgressWindow();
}

public interface IProgressWindow
{
    void Show();

    void SetProgress(int percent, string? text = null, string? extraText = null);

    void SetExtraText(string? text);

    void Close();
}
