# Converting the posts

## GUI

You can convert by "Convert" button on the post list, at the first page.

The directory is automatically initialized when the app is started.

Rebuild and clean via GUI is not supported.

## CLI

The usage is `(command name) (init|build|rebuild|clean)`.

* `init`: Creates empty directory and result path that will be used for converting.
* `build`: Start converting.
* `rebuild`: Removes all converted files (but not attachment files) and rebuild.
* `clean`: Removes hash directory.

## API

See [API Documentation](../api/Pagene.Converter.Converter.html).

TL;DR:

```csharp
var converter = new Pagene.Converter.Converter();
//The files are converted by writing:
converter.BuildAsync();
//It cleans all and builds from first by writing:
converter.RebuildAsync();
//For creating empty directories that will be used by the app:
converter.Initialize();
//The hash directory is cleaned by writing:
converter.Clean();
```
