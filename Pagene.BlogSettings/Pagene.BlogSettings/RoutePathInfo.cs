namespace Pagene.BlogSettings
{
    /// <summary>
    /// Defines front-end reader framework-specific (Blazor, React, Vue, Angular or whatever) routes.
    /// </summary>
    public static class RoutePathInfo //TODO: read from appsettings.json.
    {
        /// <summary>
        /// Link to the blog post, on your framework.
        /// for example, the reader will read from: https://example.com/{ContentPath}/your-blog-post-name
        /// </summary>
        /// <remarks>This is automatically URL-encoded. Don't put slash("/") into this.</remarks>
        public static string ContentPath { get => _contentPath; internal set => _contentPath = System.Web.HttpUtility.UrlEncode(value); }
        private static string _contentPath = "posts";
        /// <summary>
        /// Link to the tag list, on your framework.
        /// for example, the reader will read from: https://example.com/{TagPath}/0
        /// </summary>
        /// <remarks>This is automatically URL-encoded. Don't put slash("/") into this.</remarks>
        public static string TagPath { get => _tagPath; internal set => _tagPath = System.Web.HttpUtility.UrlEncode(value); }
        private static string _tagPath = "tags";
    }
}
