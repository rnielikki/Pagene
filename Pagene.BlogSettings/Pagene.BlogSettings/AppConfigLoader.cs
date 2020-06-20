using Microsoft.Extensions.Configuration;

namespace Pagene.BlogSettings
{
    /// <summary>
    /// Loads configuration by reading the appsettings.json.
    /// </summary>
    public static class AppConfigLoader
    {
        /// <summary>
        /// Loads configurations from appsettings.json.
        /// </summary>
        /// <remarks>Please don't forget to use this before using the app, unless it will use default paths, which is may not intended.</remarks>
        public static void LoadConfig()
        {
            IConfigurationRoot config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, false)
                .Build();
            LoadConfig(config);
        }
        public static void LoadConfig(IConfigurationRoot config)
        {
            AppPathInfo.InputPath = AssignConfig(config.GetSection("path:input"), AppPathInfo.InputPath);
            AppPathInfo.OutputPath = AssignConfig(config.GetSection("path:output"), AppPathInfo.OutputPath);

            RoutePathInfo.ContentPath = AssignConfig(config.GetSection("path:route:content"), RoutePathInfo.ContentPath);
            RoutePathInfo.TagPath = AssignConfig(config.GetSection("path:route:tag"), RoutePathInfo.TagPath);

            ConvertingInfo.RecentPostsCount = AssignConfig(config.GetSection("recentPostsCount"), ConvertingInfo.RecentPostsCount);
            ConvertingInfo.UseSummary = AssignConfig(config.GetSection("summary:enable"), ConvertingInfo.UseSummary);
            ConvertingInfo.SummaryLength = AssignConfig(config.GetSection("summary:length"), ConvertingInfo.SummaryLength);
        }
         //NOTE: if value does not exist, it returns default. (especailly boolean is false, which possible to change true to false) - so "exists" check is necessary.
        private static T AssignConfig<T>(IConfigurationSection section, T defaultValue)
        {
            if (!section.Exists())
            {
                return defaultValue;
            }
            else
            {
                T value = section.Get<T>();
                if (value is int number && number < 1)
                {
                    return defaultValue;
                }
                return value;
            }
        }
    }
}
