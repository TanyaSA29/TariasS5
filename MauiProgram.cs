using Microsoft.Extensions.Logging;
using TariasS5.Utiles;

namespace TariasS5
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
            
            string dbRuta = FileAccessHelper.GetForlderPath("uisrael.db3");
            builder.Services.AddSingleton<PersonRepo>
                (s=> ActivatorUtilities.CreateInstance<PersonRepo>(s, dbRuta));

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
