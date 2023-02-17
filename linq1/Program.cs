using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace linq1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var list = CreateList();
            Decimal sum = 0;
            foreach (var i in list)
            {
                sum += i.Price * i.Quantity;
            }
            Console.WriteLine($"總價格:{sum}");    //第一題
            decimal average = (sum / list.Count);
            Console.WriteLine($"平均價格:{average}"); //第二題        
            Console.WriteLine($"商品總數量{list.Sum(x => x.Quantity)}"); //第三題
            Console.WriteLine($"商品平均數量:{list.Average(x => x.Quantity):f2}"); //第四題-1
            Console.WriteLine($"商品平均數量{list.Sum(x => x.Quantity) / list.Count}"); //第四題-2
            Console.WriteLine($"最貴的商品:{list.Max(x => x.Price)}"); //第五題
            Console.WriteLine($"最貴的商品:{list.Min(x => x.Price)}"); //第六題

            var query = list.Where(x => x.Category == "3C").Sum(x => x.Price);


            Console.WriteLine($"算產品類別為 3C 的商品總價:{list.Sum(x => x.Price)}"); //第六題










        }
        static List<Product> CreateList()
        {
            List<Product> list = new List<Product>();

            var reader = new StreamReader(@"product.csv");
            while (!reader.EndOfStream)
            {
                
                var line = reader.ReadLine();
                var values = line.Split(',');
                if (values[0] == "商品編號") 
                { 
                    continue; 
                }
                else
                {
                    list.Add(new Product { Po = values[0], Name = values[1], Quantity = int.Parse(values[2]), Price = Decimal.Parse(values[3]), Category = values[4] });
                }
                Console.WriteLine(values[0]);
            }
            return list;
           
        }
    }

}

