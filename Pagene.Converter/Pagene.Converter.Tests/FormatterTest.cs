﻿using Pagene.Converter.Tests.Models;
using System.IO.Abstractions;
using Xunit;
using FluentAssertions;
using System.Collections.Generic;

namespace Pagene.Converter.Tests
{
    public class FormatterTest
    {
        private readonly IFormatter _formatter = new Formatter("contents");
        [Theory]
        [MemberData(nameof(GetValidationSet))]
        public async System.Threading.Tasks.Task GetBlogHeadTest(IFileInfo sample, IEnumerable<string> tags, string title, bool valid)
        {
            using var fileStream = sample.Open(System.IO.FileMode.Open);
            if (!valid)
            {
                await Assert.ThrowsAsync<System.FormatException>(() => _formatter.GetBlogHead(sample, fileStream));
            }
            else
            {
                (var entryTags, var entry) = await _formatter.GetBlogHead(sample, fileStream);
                entry.Title.Should().Be(title);
                entry.URL.Should().Be("contents/"+sample.Name);
                entryTags.Should().BeEquivalentTo(tags);
            }
        }

         public static TheoryData<IFileInfo, IEnumerable<string>, string, bool> GetValidationSet() => new TheoryData<IFileInfo, IEnumerable<string>, string, bool>() {
            { FormatterTestModel.file1, TagsTestModel.tags1, FormatterTestModel.title1, true },
            { FormatterTestModel.file2, TagsTestModel.tags2, FormatterTestModel.title2, true },
            { FormatterTestModel.error1, null, string.Empty, false },
            { FormatterTestModel.error2, null, string.Empty, false }
        };
    }
}
