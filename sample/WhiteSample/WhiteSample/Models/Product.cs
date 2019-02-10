using WhiteMvvm.Bases;

namespace WhiteSample.Models
{
    public class Product : BaseModel
    {
        public string Name { get; set; }
        public string Price { get; set; }
        public string Discount { get; set; }
    }
}
