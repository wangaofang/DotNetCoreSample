using System.Collections.Generic;

namespace ForgeSample.Models
{
    public class ProductService
    {
        public static ProductService Current { get; } = new ProductService();

        public List<Product> Products { get; }

        private ProductService()
        {
            Products=new List<Product>
            {
                new Product
                {
                    Id=1,
                    Name="牛奶",
                    Price=2.5f

                },
                new Product
                {
                    Id=2,
                    Name="面包",
                    Price=4.5f
                } ,
                new Product
                {
                    Id=3,
                    Name="啤酒",
                    Price=7.5f
                }               
            };
        }
    }
}