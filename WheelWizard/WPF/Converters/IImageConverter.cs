using System;
using System.Globalization;
using System.Windows.Data;

namespace CT_MKWII.WPF.Converters;

public sealed class IImageConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo cultue)
    {
        if (value is not BitmapImageWrapper img)
            return value;

        return img.GetBackingImage();
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotSupportedException("IImageConverter can only be used for one way conversion.");
    }
}

