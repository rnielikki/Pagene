# Converting the posts
## GUI

You can convert by "Convert" button on the post list, at the first page.

## CLI
parameter is either `init` or `convert`. `init` creates all directories that is used for the program.

`init (path)` or `convert (path)` executes program relative to the (path).

The default path is "", which is relative to the program path.

## API

See [API Documentation](../api/Pagene.Converter.Converter.html).

TL;DR: The file are converted by writing:

```csharp
new Pagene.Converter.Converter(/* path, optional */).ConvertAsync();
```