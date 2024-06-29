using InputKit.Handlers;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Controls;
using Microsoft.Maui;
using Microsoft.Maui.Controls.Compatibility.Hosting;

namespace Lerdo_MX_PQM
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureMauiHandlers(handlers =>
                {
                    handlers.AddInputKitHandlers();
                })
                .UseMauiCompatibility()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("Inter-Black.ttf", "Inter-Black");
                    fonts.AddFont("Inter-Bold.ttf", "Inter-Bold");
                    fonts.AddFont("Inter-ExtraBold.ttf", "Inter-ExtraBold");
                    fonts.AddFont("Inter-ExtraLight.ttf", "Inter-ExtraLight");
                    fonts.AddFont("Inter-Medium.ttf", "Inter-Medium");
                    fonts.AddFont("Inter-Regular.ttf", "Inter-Regular");
                    fonts.AddFont("Inter-SemiBold.ttf", "Inter-SemiBold");
                    fonts.AddFont("Inter-Thin.ttf", "Inter-Thin");
                    fonts.AddFont("fontello.ttf", "iconos_font"); // agregamos esta fuente.
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
