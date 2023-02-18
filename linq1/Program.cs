using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
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
            Console.WriteLine($"1. 計算所有商品的總價格:{sum}");    
            Console.WriteLine("");
            decimal average = (sum / list.Count);
            Console.WriteLine($"2. 計算所有商品的平均價格:{average}"); 
            Console.WriteLine("");
            Console.WriteLine($"3. 計算商品的總數量{list.Sum(x => x.Quantity)}"); 
            Console.WriteLine("");
            Console.WriteLine($"4. 計算商品的平均數量:{list.Average(x => x.Quantity):f2}"); //第四題-1
            Console.WriteLine($"5. 找出哪一項商品最貴{list.Sum(x => x.Quantity) / list.Count}"); //第四題-2
            Console.WriteLine("");
            Console.WriteLine($"6. 找出哪一項商品最便宜:{list.Max(x => x.Price)}"); 
            Console.WriteLine("");
            Console.WriteLine($"7. 計算產品類別為 3C 的商品總價:{list.Min(x => x.Price)}"); 
            Console.WriteLine("");
            var product_3c = list.Where(x => x.Category == "3C").Sum(x => x.Price);
            Console.WriteLine($"8. 計算產品類別為飲料及食品的商品價格:{product_3c}"); 
            Console.WriteLine("");
            var porduct_beverage= list.Where((x) => x.Category == "飲料" ||  x.Category == "食品").Sum(x => x.Price);
            Console.WriteLine($"9. 找出所有商品類別為食品，而且商品數量大於 100 的商品:{porduct_beverage}");  
            Console.WriteLine("");
            var product_food = list.Where((x) => x.Category == "食品" || x.Quantity > 100);
            Console.WriteLine("10. 找出各個商品類別底下有哪些商品的價格是大於 1000 的商品:");  
            foreach (var i in product_food)
            {
                Console.WriteLine($"{i.Category} {i.Name} {i.Quantity}");
            }
            Console.WriteLine("");
            var product_1000 = list.Where((x) => x.Price > 1000).GroupBy((x) => x.Category);
            Console.WriteLine("11. 呈上題，請計算該類別底下所有商品的平均價格"); 
            foreach (var i in product_1000)
            {
                foreach (var j in i)
                    {
                    Console.WriteLine($"{j.Category} {j.Price}");
                }               
                Console.WriteLine($"{i.Average((x) => x.Price)}"); 
            }
            Console.WriteLine("");
            Console.WriteLine("12. 依照商品價格由高到低排序:");
            list.OrderByDescending(x => x.Price);
            foreach (var i in list)
            {
                Console.WriteLine($"{i.Name} {i.Price}");
            }
            Console.WriteLine("");
            Console.WriteLine("13. 依照商品數量由低到高排序:"); 
            list.OrderBy(x => x.Quantity);
            foreach (var i in list)
            {
                Console.WriteLine($"{i.Name} {i.Quantity}");
            }          
            Console.WriteLine("");
            Console.WriteLine("14. 找出各商品類別底下，最貴的商品:"); 
            var expensive = list.GroupBy((x) => x.Category);
            foreach (var i in expensive)
            { 
                Console.WriteLine($"{i.Key} 最貴的商品:");
                var richest = i.Where(x => x.Price == (i.Max(y => y.Price)));  
                foreach (var r in richest)
                {
                    Console.WriteLine($"{r.Name} {r.Price}");

                }
            }
            
            Console.WriteLine("");
            Console.WriteLine("15. 找出各商品類別底下，最便宜的商品:");  
            var cheap = list.GroupBy((x) => x.Category);
            foreach (var i in expensive)
            {
                Console.WriteLine($"{i.Key} 最便宜的商品:");
                var cheapest = i.Where(x => x.Price == (i.Min(y => y.Price)));
                foreach (var c in cheapest)
                {
                    Console.WriteLine($"{c.Name} {c.Price}");

                }
            }
            Console.WriteLine("16. 找出價格小於等於 10000 的商品:");
            var priceless_10000 = list.Where(x => x.Price <= 10000);
            foreach (var p in priceless_10000)
            {
                Console.WriteLine($"{p.Name} {p.Price}");

            }

            Console.WriteLine("17. 製作一頁 4 筆總共 5 頁的分頁選擇器:");
            Console.WriteLine("想看1~5頁的哪一頁? \n請輸入數字1~5喔");
            string page = Console.ReadLine();
            var pages = list.Skip((int.Parse(page)-1) * 4).Take(4).ToList();          
            foreach(var p in pages)
            {              
                if ( page!="0" )
                {
                    Console.WriteLine($"{p.Po}  {p.Name} {p.Quantity} {p.Price} {p.Category}");
                }                             
            }
            


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
               
            }
            return list;
           
        }
    }

}

