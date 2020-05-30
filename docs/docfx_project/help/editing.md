# Editing the posts (before converting)

## GUI Application (Pagene.Editor) (Windows)

The program shows by creation date order, newer first.

You can edit post by clicking edit button, and can add post by add button.

### Edit/Add interface

#### About markdown syntax

There are some markdown syntax adding buttons, but it's just for convinience and not 100% accurate including every exceptional case.

If you know how to use markdown, we recommend to write it by yourself.

#### About files from the file browser

Image files from the edit interface are from `contents/files`, not including subdirectory.

#### About the tags

You can add tag by Clicking + button. You can also remove tag by clicking it.

The tag suggestion is from the converted data, so tags from unconverted post may not be shown in the suggestions.

> Note: Showing long/too many tags can be buggy. It'll be fixed in future when WinForms layout works.

## Console app

Input file format are markdown format, which ends with `.md`.

The must contain this format:

<pre>
[tags,separated,by,comma]
# title
Content
</pre>

If the format doesn't match, the converter program will throw `FormatException`.

You can open and use GUI (Windows), or use console app (All platforms) by adding files manually.

The blog post (`*.md`) files are inside `inputs/contents/`, without subdirectories.

Image path is same: `content/files`, which contains any attachments. These are not converted, so you should add them directly.

## Notes

* This reads creation date and modification date from metadata. They can be modified so this way is inaccurate, but:
 * There are no other way to know about real immutable creation/edit date.
 * You can get creation and modification date from converted data (reverse).
 ## Markdown format
* We use [MarkDig](https://github.com/lunet-io/markdig) for markdown parse engine. MarkDig is [CommonMark](https://spec.commonmark.org/0.29/#setext-heading-underline) compliant.
 * Some extensible markdown syntax may not work, such as underline, sub, sup etc. You can use HTML tag here instead.
