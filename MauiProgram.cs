using Microsoft.Extensions.Logging;
using UTSProject.Resources.Services;
using UTSProject.Resources.ViewModels;
using UTSProject.Resources.Views;

namespace UTSProject
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();

            //Main Page
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<MainViewModel>();
            //User Page
            builder.Services.AddSingleton<UserPage>();
            builder.Services.AddSingleton<UserViewModel>();
            //Tickets Page
            builder.Services.AddSingleton<TicketsPage>();
            builder.Services.AddSingleton<TicketsViewModel>();
            //Connections Page
            builder.Services.AddSingleton<ConnectionsPage>();
            builder.Services.AddSingleton<ConnectionsViewModel>();
            //GTFSRealTimeData
            builder.Services.AddSingleton<NTAService>();
#endif

            return builder.Build();
        }
    }
}
