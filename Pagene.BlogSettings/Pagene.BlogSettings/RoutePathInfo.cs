using System;
using System.Collections.Generic;
using System.Text;

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
        public const string ContentPath = "contents/";
        /// <summary>
        /// Link to the tag list, on your framework.
        /// for example, the reader will read from: https://example.com/{TagPath}/0
        /// </summary>
        public const string TagPath = "tags/";
    }
}
