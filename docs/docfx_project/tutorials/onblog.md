# Using on blog

## Set base path

The base path should be relative to output `contents` directory. This process is important to:

```HTML
<!--add this to header-->
<!--replace {{your blog path}} to path where "contents" folder is-->
<base href="{{your blog path}}">
```

## Make markdown converter ready

Since the converted file content is still markdown, you should make visible HTML format from markdown. For example, markdig or showdown.js will convert to markdown.

> Note: The app automatically converts image that starts with `files` path to `contents/files`. This will show image preview on e.g. Visual Studio Code. If you don't want this, use `./files`.

## Call tags

You can call JSON GET with help of [file structure](../help/file-structure.html) and [reading files](../help/reading.html).

## Some useful tips

* Use `entries/meta.tags.json`** for creating tag cloud.
* Use `entries/tags/(number).json`** to show post summaries.
