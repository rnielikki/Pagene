namespace Pagene.Converter
{
    public partial class Converter
    {
        /// <summary>
        /// Creates input directory if doesn't exist.
        /// </summary>
        public void Initialize() => InitializationHelper.Initialize(_fileSystem);
    }
}
