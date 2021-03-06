﻿using System.IO.Abstractions;
using System.Text;
using System.Globalization;
using System.Text.RegularExpressions;
using System;
using System.Linq;
using Pagene.BlogSettings;

namespace Pagene.Editor
{
    internal class NamingLogic
    {
        private readonly Regex _spaceRegex = new Regex("[ ]+");
        private readonly Regex _invalidCharsRegex = new Regex($"[{Regex.Escape(new string(System.IO.Path.GetInvalidFileNameChars()))}]+");
        private static readonly string[] _reserved = new string[] { "con", "prn", "aux", "nul", "com1", "com2", "com3", "com4", "com5", "com6", "com7", "com8", "com9", "lpt1", "lpt2", "lpt3", "lpt4", "lpt5", "lpt6", "lpt7", "lpt8", "lpt9" };
        private readonly IFileSystem _fileSystem;
        internal NamingLogic(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }
        internal string GetName(string title)
        {
            string baseName = GetBaseName(title);
            string baseFileName = baseName + ".md";
            if (!ExistsFile(baseFileName) && !_reserved.Contains(baseName))
            {
                 return baseFileName;
            }
            else
            {
                int i = 0;
                while (ExistsFile($"{baseName}-{i}.md"))
                {
                    i++;
                }
                return $"{baseName}-{i}.md";
            }
        }
        private bool ExistsFile(string name) => _fileSystem.File.Exists(AppPathInfo.BlogInputPath+name);
        private string GetBaseName(string title)
        {
            try
            {
                var normalized = NormalizeDiacritics(title);
                IdnMapping idn = new IdnMapping
                {
                    AllowUnassigned = true
                };
                var resultFileName = System.Web.HttpUtility.UrlEncode(
                    ReplaceSpaces(
                        RemoveInvalidChars(idn.GetAscii(normalized))
                    )
                ).Replace('%', '_').ToLower();
                if (string.IsNullOrEmpty(resultFileName))
                {
                    return GenerateFromDate();
                }
                else
                {
                    return resultFileName;
                }
            }
            catch(ArgumentException) //
            {
                return GenerateFromDate();
            }
        }
        private static string NormalizeDiacritics(string input)
        {
            var separated = input.Normalize(NormalizationForm.FormD); // separates umlaut to other char etc.
            int length = separated.Length;
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                var uc = CharUnicodeInfo.GetUnicodeCategory(separated[i]);
                if (uc != UnicodeCategory.NonSpacingMark)
                {
                    builder.Append(separated[i]);
                }
            }
            return builder.ToString().Normalize(NormalizationForm.FormC); // rejoin them
        }
        private string ReplaceSpaces(string input) => _spaceRegex.Replace(input, "-");
        private string RemoveInvalidChars(string input) => _invalidCharsRegex.Replace(input, "");
        private static string GenerateFromDate() => new DateTime().ToString("yyyyMMdd");
    }
}
