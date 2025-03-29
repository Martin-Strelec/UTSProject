using Microsoft.Extensions.Logging;
using UTSProject.Resources.Models;
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
            builder.Services.AddTransient<UserPage>();
            builder.Services.AddTransient<UserViewModel>();
            //Tickets Page
            builder.Services.AddTransient<TicketsPage>();
            builder.Services.AddTransient<TicketsViewModel>();
            //Connections Page
            builder.Services.AddSingleton<ConnectionsPage>();
            builder.Services.AddSingleton<ConnectionsViewModel>();
            //Detail Page
            builder.Services.AddTransient<DetailPage>();
            builder.Services.AddTransient<DetailViewModel>();
            //GTFSRealTimeData
            builder.Services.AddSingleton<NTAService>();
            //NTALocalDatabase 
            builder.Services.AddSingleton<DbService>();
            //LoadDataService
            builder.Services.AddSingleton<LoadDataService>();
            //SavedConnectionsService
            builder.Services.AddSingleton<SavedConnectionsService>();

#endif

            return builder.Build();
        }
    }
}
