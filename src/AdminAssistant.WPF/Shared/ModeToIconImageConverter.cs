using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using AdminAssistant.UI.Shared;
using MahApps.Metro.IconPacks.Converter;

namespace AdminAssistant.WPF.Shared;

public sealed class ModeToIconImageConverter : PackIconFontAwesomeKindToImageConverter, IValueConverter
{
    /// <inheritdoc />
    [SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Ok to just return DependencyProperty.UnsetValue if anything goes wrong.")]
    object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        try
        {
            if ((value is Mode) == false)
                return DependencyProperty.UnsetValue;


            var enumValue = ((Mode)value).ToPackIconFontAwesomeKind();
            return base.Convert(enumValue, targetType, parameter, culture);
        }
        catch
        {
            return DependencyProperty.UnsetValue;
        }
    }

    /// <inheritdoc />
    [SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Ok to just return DependencyProperty.UnsetValue if anything goes wrong.")]
    object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        try
        {
            return base.ConvertBack(value, targetType, parameter, culture);
        }
        catch
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
