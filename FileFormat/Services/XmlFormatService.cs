using System.Text;
using System.Xml;

namespace FileFormat
{
    internal class XmlFormatService : IFileFormat
    {
        public void LoadFromFile(string filePath, List<CarRecord> carRecords)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);

            XmlNodeList carNodes = doc.GetElementsByTagName("Car");
            foreach (XmlNode carNode in carNodes)
            {
                XmlNode dateNode = carNode.SelectSingleNode("Date");
                XmlNode brandNameNode = carNode.SelectSingleNode("BrandName");
                XmlNode priceNode = carNode.SelectSingleNode("Price");

                DateTime date = DateTime.ParseExact(dateNode.InnerText, "dd.MM.yyyy", null);
                string brandName = brandNameNode.InnerText;
                int price = int.Parse(priceNode.InnerText);

                CarRecord record = new CarRecord(date, brandName, price);
                carRecords.Add(record);
            }
        }

        public void SaveToFile(string filePath, List<CarRecord> carRecords)
        {
            XmlWriterSettings settings = new XmlWriterSettings
            {
                Indent = true,
                Encoding = Encoding.UTF8
            };

            using (XmlWriter writer = XmlWriter.Create(filePath, settings))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("Document");

                foreach (CarRecord record in carRecords)
                {
                    writer.WriteStartElement("Car");

                    writer.WriteElementString("Date", record.Date.ToString("dd.MM.yyyy"));
                    writer.WriteElementString("BrandName", record.BrandName);
                    writer.WriteElementString("Price", record.Price.ToString());

                    writer.WriteEndElement();
                }

                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
        }
    }
}
