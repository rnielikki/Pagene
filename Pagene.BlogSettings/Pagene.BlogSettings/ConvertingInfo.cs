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
        public const int RecentPostsLength = 10;
        /// <summary>
        /// Defines if summary is generated to blog entry.
        /// </summary>
        public const bool UseSummary = true;
        /// <summary>
        /// Defines length of the summary. Useless if <see cref="UseSummary"/> is false.
        /// </summary>
        public const int SummaryLength = 80;
    }
}
