using FileFormat;
using NUnit.Framework;
using System.Xml;

namespace FileFormatUnitTest
{
    public class Tests
    {
        public List<CarRecord> records;

        [SetUp]
        public void Setup()
        {
            records = new List<CarRecord>
            {
                new CarRecord(new DateTime(2018, 10, 10), "Лада", 17000),
                new CarRecord(new DateTime(2019, 11, 1), "BMW", 50000 )
            };
        }

        [Test]
        public void AddRecords_ShouldSaveRecordsCorrectly()
        {
            // Arrange.
            var filePath = @"\Files\Xml\sample.xml";
            var fileFormatService = new FileFormatService(filePath);

            // Act.
            fileFormatService.AddRecords(records);
            var currentRecords = fileFormatService.GetRecords();

            // Assert.
            Assert.That(currentRecords, Has.Count.EqualTo(records.Count));

            for (var i = 0; i < records.Count; i++)
            {
                var item = currentRecords
                    .Where(r => r.Date == records[i].Date && r.BrandName == records[i].BrandName && r.Price == records[i].Price)
                    .SingleOrDefault();
                Assert.That(item, Is.Not.Null);
            }
        }

        [Test]
        public void AddRecord_ShouldSaveRecordCorrectly()
        {
            // Arrange.
            var filePath = @"\Files\Xml\sample.xml";
            var fileFormatService = new FileFormatService(filePath);
            var record = new CarRecord(new DateTime(2018, 10, 10), "Лада", 17000);

            // Act.
            fileFormatService.AddRecord(record);
            var currentRecords = fileFormatService.GetRecords();

            // Assert.
            Assert.That(currentRecords, Has.Count.EqualTo(1));

            Assert.Multiple(() =>
            {
                Assert.That(currentRecords[0].Date, Is.EqualTo(record.Date));
                Assert.That(currentRecords[0].BrandName, Is.EqualTo(record.BrandName));
                Assert.That(currentRecords[0].Price, Is.EqualTo(record.Price));
            });
        }

        [Test]
        public void RemoveRecord_ShouldSaveRecordsCorrectly()
        {
            // Arrange.
            var filePath = @"\Files\Xml\sample.xml";
            var fileFormatService = new FileFormatService(filePath);
            var record = new CarRecord(new DateTime(2018, 5, 12), "Mercedes", 170000);
            records.Add(record);

            // Act.
            fileFormatService.AddRecords(records);
            fileFormatService.RemoveRecord(record);
            records.Remove(record);
            var currentRecords = fileFormatService.GetRecords();

            // Assert.
            Assert.That(currentRecords, Has.Count.EqualTo(records.Count));

            for (var i = 0; i < records.Count; i++)
            {
                var item = currentRecords
                    .Where(r => r.Date == records[i].Date && r.BrandName == records[i].BrandName && r.Price == records[i].Price)
                    .SingleOrDefault();
                Assert.That(item, Is.Not.Null);
            }
        }

        [Test]
        public void ClearRecord_ShouldClearRecordsCorrectly()
        {
            // Arrange.
            var filePath = @"\Files\Xml\sample.xml";
            var fileFormatService = new FileFormatService(filePath);

            // Act.
            fileFormatService.AddRecords(records);
            fileFormatService.ClearRecords();
            var currentRecords = fileFormatService.GetRecords();

            // Assert.
            Assert.That(currentRecords, Has.Count.EqualTo(0));
        }

        [Test]
        public void EditRecord_ShouldEditRecordCorrectly()
        {
            // Arrange.
            var filePath = @"\Files\Xml\sample.xml";
            var fileFormatService = new FileFormatService(filePath);
            var oldRecord = new CarRecord(new DateTime(2018, 10, 10), "Лада", 17000);
            var newRecord = new CarRecord(new DateTime(2018, 5, 12), "Mercedes", 170000);

            // Act.
            fileFormatService.AddRecord(oldRecord);
            fileFormatService.EditRecord(oldRecord, newRecord);
            var currentRecords = fileFormatService.GetRecords();

            // Assert.
            Assert.That(currentRecords, Has.Count.EqualTo(1));

            Assert.Multiple(() =>
            {
                Assert.That(currentRecords[0].Date, Is.EqualTo(newRecord.Date));
                Assert.That(currentRecords[0].BrandName, Is.EqualTo(newRecord.BrandName));
                Assert.That(currentRecords[0].Price, Is.EqualTo(newRecord.Price));
            });
        }

        [Test]
        public void SaveRecordsToXml_CorrectXml()
        {
            // Arrange.
            var workingDirectory = Environment.CurrentDirectory;
            var filePath = string.Concat(Directory.GetParent(workingDirectory).Parent.Parent.FullName, @"\Files\Xml\saveRecords.xml");
            var fileFormatService = new FileFormatService(filePath);

            var testFilePath = string.Concat(Directory.GetParent(workingDirectory).Parent.Parent.FullName, @"\Files\Xml\saveRecordsTest.xml");
            XmlDocument testXml = new XmlDocument();
            testXml.Load(testFilePath);

            // Act.
            fileFormatService.AddRecords(records);
            fileFormatService.SaveToFile();

            //Assert.
            Assert.That(filePath, Does.Exist);

            XmlDocument resultXml = new XmlDocument();
            resultXml.Load(filePath);

            Assert.That(testXml.OuterXml, Is.EqualTo(resultXml.OuterXml));
        }

        [Test]
        public void SaveEmptyRecordsToXml_CorrectXml()
        {
            // Arrange.
            var workingDirectory = Environment.CurrentDirectory;
            var filePath = string.Concat(Directory.GetParent(workingDirectory).Parent.Parent.FullName, @"\Files\Xml\saveEmptyRecords.xml");
            var fileFormatService = new FileFormatService(filePath);

            var testFilePath = string.Concat(Directory.GetParent(workingDirectory).Parent.Parent.FullName, @"\Files\Xml\saveEmptyRecordsTest.xml");
            XmlDocument testXml = new XmlDocument();
            testXml.Load(testFilePath);

            // Act.
            fileFormatService.SaveToFile();

            //Assert.
            Assert.That(filePath, Does.Exist);

            XmlDocument resultXml = new XmlDocument();
            resultXml.Load(filePath);

            Assert.That(testXml.OuterXml, Is.EqualTo(resultXml.OuterXml));
        }

        [Test]
        public void LoadRecordsFromXml_CorrectRecords()
        {
            // Arrange.
            var workingDirectory = Environment.CurrentDirectory;
            var filePath = string.Concat(Directory.GetParent(workingDirectory).Parent.Parent.FullName, @"\Files\Xml\loadRecordsTest.xml");
            var fileFormatService = new FileFormatService(filePath);

            // Act.
            fileFormatService.LoadFromFile();
            var currentRecords = fileFormatService.GetRecords();

            //Assert.
            Assert.That(currentRecords, Has.Count.EqualTo(records.Count));

            for (var i = 0; i < records.Count; i++)
            {
                var item = currentRecords
                    .Where(r => r.Date == records[i].Date && r.BrandName == records[i].BrandName && r.Price == records[i].Price)
                    .SingleOrDefault();
                Assert.That(item, Is.Not.Null);
            }
        }

        [Test]
        public void LoadEmptyRecordsFromXml_CorrectEmptyRecords()
        {
            // Arrange.
            var workingDirectory = Environment.CurrentDirectory;
            var filePath = string.Concat(Directory.GetParent(workingDirectory).Parent.Parent.FullName, @"\Files\Xml\loadEmptyRecordsTest.xml");
            var fileFormatService = new FileFormatService(filePath);

            // Act.
            fileFormatService.LoadFromFile();
            var currentRecords = fileFormatService.GetRecords();

            //Assert.
            Assert.That(currentRecords, Has.Count.EqualTo(0));
        }

        [Test]
        public void SaveRecordsToBinary_CorrectBinary()
        {
            // Arrange.
            var workingDirectory = Environment.CurrentDirectory;
            var filePath = string.Concat(Directory.GetParent(workingDirectory).Parent.Parent.FullName, @"\Files\Binary\saveRecords.bin");
            var fileFormatService = new FileFormatService(filePath);

            var testFilePath = string.Concat(Directory.GetParent(workingDirectory).Parent.Parent.FullName, @"\Files\Binary\saveRecordsTest.bin");

            // Act.
            fileFormatService.AddRecords(records);
            fileFormatService.SaveToFile();

            byte[] fileBytes = File.ReadAllBytes(filePath);
            byte[] testFileBytes = File.ReadAllBytes(testFilePath);

            //Assert.
            Assert.That(filePath, Does.Exist);
            Assert.That(testFileBytes, Has.Length.EqualTo(fileBytes.Length));

            for (int i = 0; i < fileBytes.Length; i++)
            {
                Assert.That(fileBytes[i], Is.EqualTo(testFileBytes[i]));
            }
        }

        [Test]
        public void SaveEmptyRecordsToBinary_CorrectBinary()
        {
            // Arrange.
            var workingDirectory = Environment.CurrentDirectory;
            var filePath = string.Concat(Directory.GetParent(workingDirectory).Parent.Parent.FullName, @"\Files\Binary\saveEmptyRecords.bin");
            var fileFormatService = new FileFormatService(filePath);

            var testFilePath = string.Concat(Directory.GetParent(workingDirectory).Parent.Parent.FullName, @"\Files\Binary\saveEmptyRecordsTest.bin");

            // Act.
            fileFormatService.SaveToFile();

            byte[] fileBytes = File.ReadAllBytes(filePath);
            byte[] testFileBytes = File.ReadAllBytes(testFilePath);

            //Assert.
            Assert.That(filePath, Does.Exist);
            Assert.That(testFileBytes, Has.Length.EqualTo(fileBytes.Length));

            for (int i = 0; i < fileBytes.Length; i++)
            {
                Assert.That(fileBytes[i], Is.EqualTo(testFileBytes[i]));
            }
        }

        [Test]
        public void LoadRecordsFromBinary_CorrectRecords()
        {
            // Arrange.
            var workingDirectory = Environment.CurrentDirectory;
            var filePath = string.Concat(Directory.GetParent(workingDirectory).Parent.Parent.FullName, @"\Files\Binary\loadRecordsTest.bin");
            var fileFormatService = new FileFormatService(filePath);

            // Act.
            fileFormatService.LoadFromFile();
            var currentRecords = fileFormatService.GetRecords();

            //Assert.
            Assert.That(currentRecords, Has.Count.EqualTo(records.Count));

            for (var i = 0; i < records.Count; i++)
            {
                var item = currentRecords
                    .Where(r => r.Date == records[i].Date && r.BrandName == records[i].BrandName && r.Price == records[i].Price)
                    .SingleOrDefault();
                Assert.That(item, Is.Not.Null);
            }
        }

        [Test]
        public void LoadEmptyRecordsFromBinary_CorrectEmptyRecords()
        {
            // Arrange.
            var workingDirectory = Environment.CurrentDirectory;
            var filePath = string.Concat(Directory.GetParent(workingDirectory).Parent.Parent.FullName, @"\Files\Binary\loadEmptyRecordsTest.bin");
            var fileFormatService = new FileFormatService(filePath);

            // Act.
            fileFormatService.LoadFromFile();
            var currentRecords = fileFormatService.GetRecords();

            //Assert.
            Assert.That(currentRecords, Has.Count.EqualTo(0));
        }
    }
}