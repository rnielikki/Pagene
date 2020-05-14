using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pagene.Converter
{
    //this should read appsettings.json
    //tag list file path: {ResultDir}
    //result file path: {ResultDir}/{ContentDir}
    //hash file path: {HashDir}
    internal class DirSettings
    {
        internal readonly string ContentDir;
        internal readonly string ResultDir;
        internal readonly string AttachmentDir;
        internal readonly string HashDir;
        internal DirSettings()
        {
            //default settings
            ContentDir = DirSettingsDefault.ContentDir;
            ResultDir = DirSettingsDefault.ResultDir;
            AttachmentDir = DirSettingsDefault.AttachmentDir;
            HashDir = DirSettingsDefault.HashDir;
        }
        internal DirSettings(IConfiguration configuration)
        {
            var _config = configuration.GetSection("dirs");
            ContentDir = _config.GetSection("contentPath")?.Value ?? DirSettingsDefault.ContentDir;
            ResultDir = _config.GetSection("resultPath")?.Value ?? DirSettingsDefault.ResultDir;
            AttachmentDir = _config.GetSection("attachmentPath")?.Value ?? DirSettingsDefault.AttachmentDir;
            HashDir = _config.GetSection("hashPath")?.Value ?? DirSettingsDefault.HashDir;
        }
    }
}
