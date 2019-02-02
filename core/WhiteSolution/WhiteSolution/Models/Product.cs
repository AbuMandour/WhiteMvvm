using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace WhiteSolution.Models
{
    public class Product : BaseModel
    {
        public string Name { get; set; }
        public string Price { get; set; }
        public string Discount { get; set; }
    }
}
