﻿using Microsoft.UI.Xaml;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace trabalhoTempo.WinUI
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : MauiWinUIApplication
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            Platform.MapServiceToken = "z55QeeUbRf6mRhiZDgcP~KpONY3zL1wHlGBeb590zGg~Aky4ub2z4AnvS2RMdaU2VoYIfuWnoDGtOoUfgAi9CngN5iWe3Cps0cGqUdAnBVXT";

        }

        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
    }

}
