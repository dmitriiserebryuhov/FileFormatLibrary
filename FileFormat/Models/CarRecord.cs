namespace FileFormat
{
    public class CarRecord
    {
        public CarRecord(DateTime date, string brandName, int price)
        {
            Date = date;
            BrandName = brandName;
            Price = price;
        }

        public DateTime Date { get; set; }

        public string BrandName { get; set; }

        public int Price { get; set; }
    }
}