namespace FileFormat.Interfaces
{
    public interface IFileFormatService
    {
        public List<CarRecord> GetRecords();

        public void AddRecord(CarRecord record);

        public void RemoveRecord(CarRecord record);

        public void ClearRecords();

        public void EditRecord(CarRecord oldRecord, CarRecord newRecord);

        public void SaveToFile();

        public void LoadFromFile();
    }
}
