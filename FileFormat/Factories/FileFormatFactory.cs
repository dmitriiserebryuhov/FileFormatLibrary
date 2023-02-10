namespace FileFormat
{
    /// <summary>
    /// Factory for choosing the implementation of file formatting.
    /// </summary>
    internal class FileFormatFactory
    {
        private readonly string _filePath;

        internal FileFormatFactory(string filePath)
        {
            _filePath = filePath;
        }

        internal IFileFormat GetFileFormat()
        {
            string ext = Path.GetExtension(_filePath);

            switch (ext)
            {
                case ".xml":
                    return new XmlFormatService();
                case ".bin":
                    return new BinaryFileFormatService();
                default:
                    break;
            }

            return null;
        }
    }
}
