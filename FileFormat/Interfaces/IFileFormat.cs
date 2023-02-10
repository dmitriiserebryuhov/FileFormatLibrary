namespace FileFormat
{
    public interface IFileFormat
    {
        internal void LoadFromFile(string filePath, List<CarRecord> carRecords);

        internal void SaveToFile(string filePath, List<CarRecord> carRecords);
    }
}
