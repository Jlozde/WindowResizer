using System.Configuration;
using System.Data;
using System.Windows;

namespace WindowResizer
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            // DPI farklarıyla "resolution" tutarlı olsun (özellikle %125/%150 scaling)
            Native.Win32.TrySetPerMonitorDpiAwareness();
            base.OnStartup(e);
        }
    }
}
