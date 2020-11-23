using System;
using System.Drawing;
using System.IO;
using System.Linq;
using Pagene.BlogSettings;
using Svg;

namespace Pagene.Editor
{
    internal partial class FileWindow
    {
        //https://docs.microsoft.com/en-us/dotnet/api/system.drawing.imaging.imageformat?view=dotnet-plat-ext-3.1
        private readonly string[] imageExtensions = new string[] { ".bmp", ".emf", ".exif", ".gif", ".ico", ".jpg", ".jpeg", ".png", ".tif", ".tiff", ".wmf", ".svg" };
        private void Preview(object sender, EventArgs e)
        {
            PreviewPicture.Image = null;
            ErrorMessage.Hide();
            var fileName = FileList.FocusedItem?.Text;
            if (fileName == null) return;
            try
            {
                if (string.Equals(Path.GetExtension(fileName), ".svg", StringComparison.OrdinalIgnoreCase))
                {
                    PreviewSvg(fileName);
                }
                else
                {
                    PreviewImage(fileName);
                }
            }
            catch (OutOfMemoryException)
            {
                ErrorMessage.Show();
            }
        }
        private void PreviewImage(string fileName)
        {
            using var image = Image.FromFile(AppPathInfo.BlogFilePath + fileName);
            PreviewImage(image);
        }
        private void PreviewSvg(string name)
        {
            using Image image = SvgDocument.Open(AppPathInfo.BlogFilePath + name).Draw();
            PreviewImage(image);
        }
        private void PreviewImage(Image image)
        {
            int width, height;
            if (image.Width < 1 && image.Height < 1)
            {
                return;
            }
            float imageRatio = (float)image.Width / image.Height;
            if (imageRatio > _thumbRatio)
            {
                width = _thumbWidth;
                height = (int)(_thumbHeight / imageRatio);
            }
            else
            {
                width = (int)(_thumbWidth * imageRatio);
                height = _thumbHeight;
            }
            PreviewPicture.Image = image.GetThumbnailImage(width, height, null, IntPtr.Zero);
        }
        private bool IsImageFile(FileInfo file)
        {
            return imageExtensions.Contains(file.Extension, StringComparer.OrdinalIgnoreCase);
        }
    }
}
