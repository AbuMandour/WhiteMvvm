using System;
using System.Collections.Generic;
using System.Text;
using WhiteMvvm.Bases;
using WhiteSample.Models;

namespace WhiteSample.Transitions
{
    public class ApiProduct : BaseTransitional
    {
        public string FirstName { get; set; }
        public string Price { get; set; }
        public string SecondName { get; set; }
        public string Discount { get; set; }
        
        public override TBaseModel ToModel<TBaseModel>()
        {
            var product = new Product();
            product.Name = FirstName + SecondName;
            product.Price = (Convert.ToInt32(Price) * Convert.ToDouble(Discount)).ToString();
            product.Discount = Discount;
            return product as TBaseModel;
        }
    }
}
