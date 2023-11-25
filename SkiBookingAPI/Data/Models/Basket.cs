using System.Reflection;

namespace SkiBookingAPI.Data.Models
{
    public class Basket
    {
        public class Item {
            public long BasketItemID { get; set; }
            public string ObjectType { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public int Quantity { get; set; }
            public int? NumDays { get; set; }
            public decimal TotalCost { get; set; }
            public decimal TotalGST { get; set; }
        }

        public Item[] BasketItems { get; set; }
        public decimal TotalCost { get; set; }
        public decimal TotalGST { get; set; }
    }
}
