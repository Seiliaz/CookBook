namespace CookBook.FileAccess
{
    public static class FileFormatExtension
    {
        public static string AsFileExtension(this FileFormat fileFormat)
        {
            return fileFormat == FileFormat.Json ? "json" : "txt";
        }
    }
}
