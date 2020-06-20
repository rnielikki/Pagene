﻿using Pagene.BlogSettings;
using System.IO.Abstractions;

namespace Pagene.Converter
{
    public partial class Converter
    {
        /// <summary>
        /// Creates input directory if doesn't exist.
        /// </summary>
        public void Initialize()
        {
            InitDirectory(AppPathInfo.BlogInputPath);
            InitDirectory(AppPathInfo.BlogFilePath);
            InitDirectory(AppPathInfo.BlogTagPath);
            InitDirectory(AppPathInfo.BlogHashPath);
        }
        private IDirectoryInfo InitDirectory(string path)
        {
            var info = _fileSystem.DirectoryInfo.FromDirectoryName(path);
            if (!info.Exists)
            {
                return _fileSystem.Directory.CreateDirectory(path);
            }
            else
            {
                return info;
            }
        }
    }
}