using System;
using System.Windows;

namespace Charpad
{
    public partial class App : Application
    {
        public static string[] CommandLineArgs { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            CommandLineArgs = e.Args;
        }
    }
}
