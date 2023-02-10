using System.Globalization;
using System.Text;

namespace FileFormat
{
    internal class BinaryFileFormatService : IFileFormat
    {
        public void LoadFromFile(string filePath, List<CarRecord> carRecords)
        {
            // Clear all existing records to load new records from file.
            //This is discussed functionality. Depends on the requirements.
            carRecords.Clear();

            using (BinaryReader reader = new BinaryReader(File.Open(filePath, FileMode.Open)))
            {
                try
                {
                    // Read the header.
                    short header = reader.ReadInt16();
                    if (header != 0x2526)
                    {
                        throw new InvalidDataException("Invalid header");
                    }

                    // Read the number of records.
                    int recordCount = reader.ReadInt32();

                    for (int i = 0; i < recordCount; i++)
                    {
                        // Read the date.
                        var dateBytes = reader.ReadBytes(8);
                        DateTime date = DateTime.ParseExact(Encoding.ASCII.GetString(dateBytes), "ddMMyyyy", CultureInfo.InvariantCulture);

                        // Read the brand name length.
                        short brandNameLength = reader.ReadInt16();
                        // Read the brand name.
                        var brandNameBytes = reader.ReadBytes(brandNameLength * 2);
                        string brandName = Encoding.Unicode.GetString(brandNameBytes);

                        // Read the price.
                        int price = reader.ReadInt32();

                        carRecords.Add(new CarRecord(date, brandName, price));
                    }
                }
                catch (Exception)
                {
                    throw new InvalidDataException("Invalid binary file");
                }
            }
        }

        public void SaveToFile(string filePath, List<CarRecord> carRecords)
        {
            using (BinaryWriter writer = new BinaryWriter(File.Open(filePath, FileMode.OpenOrCreate)))
            {
                try
                {
                    // Write the header.
                    writer.Write(Convert.ToInt16(0x2526));

                    // Write the record count.
                    writer.Write(carRecords.Count);

                    foreach (var record in carRecords)
                    {
                        // Write the date.
                        var date = record.Date.ToString("ddMMyyyy");

                        for (int i = 0; i < date.Length; i++)
                        {
                            writer.Write(Convert.ToByte(date[i]));
                        }

                        // Write the brand name length.
                        writer.Write(Convert.ToInt16(record.BrandName.Length));

                        // Write the brand name.
                        writer.Write(Encoding.Unicode.GetBytes(record.BrandName));

                        // Write the price.
                        writer.Write(Convert.ToInt32(record.Price));
                    }
                }
                catch (Exception e)
                {
                    throw;
                }
            }
        }
    }
}
