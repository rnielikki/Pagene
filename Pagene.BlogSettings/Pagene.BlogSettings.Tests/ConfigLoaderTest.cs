using Xunit;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace Pagene.BlogSettings.Tests
{
    public class ConfigLoaderTest
    {
        [Fact]
        public void ConfigLoadTest()
        {
            const string inputPath = "workspace";
            const string contentPath = "hello world";
            const int count = 9999;
            IConfigurationRoot config = new ConfigurationBuilder().AddInMemoryCollection(
                new Dictionary<string, string>()
                {
                    { "path:input", inputPath },
                    { "path:route:content", contentPath },
                    { "recentPostsCount", count.ToString() }
                }
                ).Build();
            AppConfigLoader.LoadConfig(config);
            Assert.Equal(inputPath, AppPathInfo.InputPath);
            Assert.Equal(System.Web.HttpUtility.UrlEncode(contentPath), RoutePathInfo.ContentPath);
            Assert.Equal(count, ConvertingInfo.RecentPostsCount);
            Assert.True(ConvertingInfo.UseSummary);
        }
    }
}
