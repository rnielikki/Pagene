# App Settings

You can create or edit `appsettings.json` to the same path as the exe file - for your own definition.

The exe file path is same as Editor path if you use editor, and Converter.Entry path if you use console converter.

All default settings can be found from [here](../api/Pagene.BlogSettings.html).

If you don't define your own, it uses automatically the default value.

So, make sure that you don't have typo to the key, unless it will use default value!

The setting file looks like (you can copypasta this to your own `appsettings.json`):

```json
{
    "path":{
        "input": "./input-path",
        "output": "./output-path",
        "route":{
             "content": "./content-path",
             "tag": "./tag-path"
        }
    },
    "recentPostsCount": 15,
    "summary":{
        "enable": true,
        "length": 150
    },
 }
```

* `path`: Defines all paths
  * `input`: Defines where are your blog post and attachments, relative to the exe/settings path, but can be absolute path too.
  * `output`: Defines where the converted result resides, relative to the exe/settings path, but can be absolute path too.
  * `route`: This section is related to your web page URL, not local path. Possibly relative to your blog root.
    * `content`: Path to show blog content list, as well as blog post (`{contentPath}/{postName}`).
    * `tag`: Path to show tag list, as well as list of post that contains each tag (`{tagPath}/{tagName}`).
* `recentPostsCount`: How much recent posts will be shown (for example, your blog app main page).
* `summary`: This handles settings about summaries.
  * `enable`: `true` if you will use the summary on the list. Otherwise it's `false`.
  * `length`: Length of the characters in the summary. Useless if `summary.enable` is `false`.
