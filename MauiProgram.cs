using Microsoft.Extensions.Logging;
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
                    fonts.AddFont("Roboto-Regular.ttf", "RobotoRegular");
                    fonts.AddFont("Roboto-Semibold.ttf", "RobotoSemibold");
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
            builder.Services.AddSingleton<UserViewModel>();
#endif

            return builder.Build();
        }
    }
}
