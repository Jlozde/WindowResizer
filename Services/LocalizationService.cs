using System;
using System.Globalization;
using System.Linq;
using System.Windows;

namespace WindowResizer.Services
{
    public static class LocalizationService
    {
        private const string Marker = "/Localization/Strings.";

        public static string T(string key)
            => Application.Current.TryFindResource(key) as string ?? key;

        public static void Apply(string cultureCode)
        {
            var app = Application.Current;

            var uri = new Uri($"pack://application:,,,/Localization/Strings.{cultureCode}.xaml", UriKind.Absolute);
            var newDict = new ResourceDictionary { Source = uri };

            var old = app.Resources.MergedDictionaries
                .FirstOrDefault(d => d.Source != null && d.Source.OriginalString.Contains(Marker, StringComparison.OrdinalIgnoreCase));

            if (old != null)
                app.Resources.MergedDictionaries.Remove(old);

            app.Resources.MergedDictionaries.Add(newDict);

            var culture = new CultureInfo(cultureCode);
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;

            // RTL (Arabic)
            if (app.MainWindow != null)
                app.MainWindow.FlowDirection = culture.TextInfo.IsRightToLeft
                    ? FlowDirection.RightToLeft
                    : FlowDirection.LeftToRight;
        }
    }
}
