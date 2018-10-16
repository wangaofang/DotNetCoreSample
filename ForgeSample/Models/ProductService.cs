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
                    Price=2.5f,
                    Materials=new List<Material>
                    {
                        new Material
                        {
                            Id=1,
                            Name="水"
                        },
                        new Material
                        {
                            Id=2,
                            Name="奶粉"
                        }
                    },
                    Description="这是牛奶啊！"
                },
                new Product
                {
                    Id=2,
                    Name="面包",
                    Price=4.5f,
                    Materials=new List<Material>
                    {
                        new Material
                        {
                            Id=3,
                            Name="面粉"
                        },
                        new Material
                        {
                            Id=4,
                            Name="糖"
                        }
                    },
                    Description="这是面包啊！"

                } ,
                new Product
                {
                    Id=3,
                    Name="啤酒",
                    Price=7.5f,
                    Materials=new List<Material>
                    {
                        new Material
                        {
                            Id=5,
                            Name="麦芽"
                        },
                        new Material
                        {
                            Id=6,
                            Name="地下水"
                        }
                    },
                    Description="这是啤酒啊！"

                }               
            };
        }
    }
}