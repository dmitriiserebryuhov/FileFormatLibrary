using FileFormat.Interfaces;

namespace FileFormat
{
    /// <summary>
    /// Main service to for working with the library.
    /// </summary>
    public class FileFormatService : IFileFormatService
    {
        private List<CarRecord> _records = new List<CarRecord>();

        private readonly string _filePath;
        private readonly IFileFormat _fileFormat;

        public FileFormatService(string filePath)
        {
            _filePath = filePath;
            _fileFormat = new FileFormatFactory(_filePath).GetFileFormat();
        }

        /// <summary>
        /// Get all records from service.
        /// </summary>
        /// <returns></returns>
        public List<CarRecord> GetRecords()
        {
            return _records;
        }

        public void AddRecord(CarRecord record)
        {
            _records.Add(record);
        }

        public void AddRecords(List<CarRecord> records)
        {
            _records.AddRange(records);
        }

        public void RemoveRecord(CarRecord record)
        {
            _records.Remove(record);
        }

        public void ClearRecords()
        {
            _records.Clear();
        }

        public void EditRecord(CarRecord oldRecord, CarRecord newRecord)
        {
            int index = _records.IndexOf(oldRecord);
            _records[index] = newRecord;
        }

        /// <summary>
        /// Loads records from a file whose path is specified in the constructor.
        /// </summary>
        public void LoadFromFile()
        {
            _fileFormat.LoadFromFile(_filePath, _records);
        }

        /// <summary>
        /// Saves records to a file whose path is specified in the constructor.
        /// </summary>
        public void SaveToFile()
        {
            _fileFormat.SaveToFile(_filePath, _records);
        }        
    }
}
