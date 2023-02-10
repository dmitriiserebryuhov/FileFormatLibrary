using FileFormat;
using System.Text;

namespace FileFormatClient
{
    class Program
    {
        public static void Main()
        {
            var workingDirectory = Environment.CurrentDirectory;
            var xmlFilePath = string.Concat(Directory.GetParent(workingDirectory).Parent.Parent.FullName, @"\Files\sample.xml");

            var fileFormatService = new FileFormatService(xmlFilePath);

            var firstRecord = new CarRecord(new DateTime(2022, 3, 9), "BMW", 75000);
            var secondRecord = new CarRecord(new DateTime(2015, 10, 25), "Лада", 10999);

            fileFormatService.AddRecord(firstRecord);
            fileFormatService.AddRecord(secondRecord);
            var currentRecords = fileFormatService.GetRecords(); //Two records in the list.

            fileFormatService.AddRecord(new CarRecord(new DateTime(2023, 01, 15), "Мercedes", 99000));
            currentRecords = fileFormatService.GetRecords(); //Three records in the list.

            fileFormatService.RemoveRecord(secondRecord);
            currentRecords = fileFormatService.GetRecords(); //Record deleted, two records in the list.

            fileFormatService.EditRecord(firstRecord, new CarRecord(new DateTime(2021, 3, 9), "BMW", 65000));
            currentRecords = fileFormatService.GetRecords(); //Record edited, two records in the list.

            fileFormatService.SaveToFile(); // Records saves into \Files\sample.xml.

            fileFormatService.ClearRecords();
            currentRecords = fileFormatService.GetRecords(); // Records list is empty;

            fileFormatService.LoadFromFile();
            currentRecords = fileFormatService.GetRecords(); // Records list are loaded from \Files\sample.xml.

            var binFilePath = string.Concat(Directory.GetParent(workingDirectory).Parent.Parent.FullName, @"\Files\sample.bin");

            fileFormatService = new FileFormatService(binFilePath);

            var records = new List<CarRecord> { firstRecord, secondRecord };
            fileFormatService.AddRecords(records);
            currentRecords = fileFormatService.GetRecords(); //Two records in the list.

            fileFormatService.SaveToFile(); // Records saves into \Files\sample.bin.

            fileFormatService.ClearRecords();
            currentRecords = fileFormatService.GetRecords(); // Records list is empty;

            fileFormatService.LoadFromFile();
            currentRecords = fileFormatService.GetRecords(); // Records list are loaded from \Files\sample.bin.
        }
    }
}
