# Reading posts and tags
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

> Note : All dates are provided with **UTC format**, so if you want local time, you must convert it manually.

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

`meta.tags.json` contains key-value pair, and the value contains Url and Count informations.

* *Url*: All tag file names are just numbers, and the numbers mean nothing but unique key.
* *Count*: How many posts are using this tag.

```json
{
    "Tag1": {
        "Url": "entries/tags/0.json",
        "Count": 1
    },
    "Tag2": {
        "Url": "entries/tags/1.json",
        "Count": 3
    }
}
```

Each tag file is number and contains real tag name (Tag) and content.

```json
{
    "Tag": "Tag1",
    "Posts": [ 
        //arrays of "entry" that contains this tag
    ]
}
```

## API
You can use [Reader API](../api/Pagene.Reader.html) for C# implementation (Blazor).

It returns:
* [BlogItem](../api/Pagene.Models.BlogItem.html) for posts
* [BlogEntry](../api/Pagene.Models.BlogItem.html) for post summary
* [TagInfo](../api/Pagene.Models.TagInfo.html) for post summaries of each tag
* [TagMeta](../api/Pagene.Models.TagMeta.html) for meta.tags.json information