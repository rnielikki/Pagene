# Reading posts and tags
## Blog Post format

A file inside `contents/` contains a post in this format:

```json
{
    "Title": "Title of the post",
    "Content": "Content of the post",
    "CreationDate": "2020-05-30T08:04:55.6725298Z",
    "ModificationDate": "2020-05-30T08:05:21.9718843Z",
    "Tags": ["tag1","tag2"]
}
```

If modificatoin date is same as creation date, it means that the app is not edited since creation.

> Note : All dates are provided with **UTC format**, so if you want local time, you must convert it manually.

## Blog Entries
Blog entries (inside `entries/` directory) are all JSON files.

One "entry" model looks like this:

```json
    {
        "Title": "Title",
        "Date": "2020-05-29T13:40:26.8565100Z",
        "Url": "posts/filename",
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

Currently the blog url and tag url are constant from [RoutePathInfo](../api/Pagene.BlogSettings.RoutePathInfo.html), but someday it will read from appsettings.json in future.

```json
{
    "Tag1": {
        "Url": "entries/tags/0",
        "Count": 1
    },
    "Tag2": {
        "Url": "entries/tags/1",
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

## Models
The [Models](../api/Pagene.Models.html) define the reading format. If you use C# with JSON Serializer, you can use the models as dll.

Or you can implement on your own, in any language with any JSON parser.

These are model documentation:

* [BlogItem](../api/Pagene.Models.BlogItem.html) for posts
* [BlogEntry](../api/Pagene.Models.BlogItem.html) for post summary
* [TagInfo](../api/Pagene.Models.TagInfo.html) for post summaries of each tag
* [TagMeta](../api/Pagene.Models.TagMeta.html) for meta.tags.json information