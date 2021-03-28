# Editing with Visual Studio Code

If you have Visual Studio Code and you know how to use markdown, this way is better than built-in editor. In this case, you need converter [console app](../help/editing.html#console-app).

## Open

In Visaul Studio Code, File -> Open Folder (`Ctrl+K` then `Ctrl+O` will do same) and select where the input "contents" path is (default is `input/contents` relative to the console app).

## Preview

On the top right, click book with magnifier. This will show preview right of the app.

![vscode0](~/images/vscode/0.png)

## Edit

The file should have this format: otherwise, everything won't be converted. **Remember to add tags (if no tags, []) on the first title** !

<pre>
[tags,separated,by,comma]
# title
Content
</pre>

The content follows just ordinal markdown syntax.

> Note: markdown lint will warn this, because markdown syntax requires to start with first heading (#). But we're doing this for specific app, so ignore it.
