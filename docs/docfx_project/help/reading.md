# Reading the API

## Blog Post format

It's same as converter format, but it contains creation date and modification date.

If modificatoin date is same as creation date, it means that the app is not edited since creation.

<pre>
> (Creation Date)
> (Modification Date)
[tags,separated,by,comma]
# title
Content
</pre>

> Note : All dates are provided with UTC format, so if you want local time, you should convert it manually.

## Blog Entries
Blog entries (inside `entries/` directory) are all JSON files.

One "entry" model looks like this:

```json
    {
        "Title": "Title",
        "Date": "2020-05-29T13:40:26.8565100Z",
        "URL": "contents/filename.md",
        "Summary": "Content, but max 50 words",
        "Tags": ["tag","separated","by","comma"]
    }
```

`entries/recent.json` is just an array of recent entries.

### Tags

Tags are inside `entries/tags`. `entries/tags` contains `meta.tags.json` and each tag files.

`meta.tags.json` only contains key(string)-value(number) pair: key is tag name and value is number of posts that contains the tag.

Each tag files contains real tag name (Tag) and content.

```json
{
    "Tag": "Blazor",
    "Posts": [ 
        //arrays of "entry" that contains this tag
    ]
}
```

Next Step; Using Reading API