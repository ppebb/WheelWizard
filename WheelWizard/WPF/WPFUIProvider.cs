using System;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using CT_MKWII.Common.UI;
using CT_MKWII.WPF.Pages;

namespace CT_MKWII.WPF;

public class BitmapImageWrapperFactory : ICreateImage
{
    public IImage NewImage(string url)
    {
        BitmapImage img = new(new Uri(url));

        return new BitmapImageWrapper(img);
    }
}

public class BitmapImageWrapper : IImage
{
    private readonly BitmapImage _image;

    public BitmapImageWrapper(BitmapImage image)
    {
        _image = image;
    }

    public BitmapImage GetBackingImage()
    {
        return _image;
    }
}

public class ProgressWindowFactory : ICreateProgressWindow
{
    public IProgressWindow NewProgressWindow()
    {
        return new ProgressWindowWrapper(new ProgressWindow());
    }
}

public class ProgressWindowWrapper : IProgressWindow
{
    private readonly ProgressWindow _pwin;

    public ProgressWindowWrapper(ProgressWindow pwin)
    {
        _pwin = pwin;
    }

    public void Close()
    {
        _pwin.Close();
    }

    public void SetExtraText(string? text)
    {
        _pwin.BottomTextLabel.Text = text;
    }

    public void SetProgress(int percent, string? text = null, string? extraText = null)
    {
        _pwin.UpdateProgress(percent, text, extraText);
    }

    public void Show()
    {
        _pwin.Show();
    }
}

public class DialogBoxWrapper : IDialogBox
{
    public DialogBoxResponse Show(string message, string? caption = null, DialogBoxButtons? buttons = null, DialogBoxIcon? icon = null)
    {
        if (icon != null)
            return (DialogBoxResponse)MessageBox.Show(message, caption, (MessageBoxButton)buttons, (MessageBoxImage)icon);

        if (buttons != null)
            return (DialogBoxResponse)MessageBox.Show(message, caption, (MessageBoxButton)buttons);

        if (caption != null)
            return (DialogBoxResponse)MessageBox.Show(message, caption);

        return (DialogBoxResponse)MessageBox.Show(message);
    }
}
