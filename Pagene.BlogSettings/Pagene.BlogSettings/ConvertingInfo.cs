namespace Pagene.BlogSettings
{
    /// <summary>
    /// Defines settings about file converting.
    /// </summary>
    public static class ConvertingInfo //TODO: read from appsettings.json.
    {
        /// <summary>
        /// Defines how many files are appeared on recent.json.
        /// </summary>
        public static int RecentPostsCount { get; internal set; } = 10;
        /// <summary>
        /// Defines if summary is generated to blog entry.
        /// </summary>
        public static bool UseSummary { get; internal set; } = true;
        /// <summary>
        /// Defines length of the summary. Useless if <see cref="UseSummary"/> is false.
        /// </summary>
        public static int SummaryLength { get; internal set; } = 80;
    }
}
