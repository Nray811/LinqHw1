using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace linq2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            do
            {
                Random random = new Random();
                int[] gm = Enumerable.Range(1, 9).OrderBy(x => random.Next()).Take(4).ToArray();   //生成亂數，拿四個到int array裡面
                Console.WriteLine("猜4個數字!");
                // Console.WriteLine($"電腦選號為:{gm[0]}{gm[1]}{gm[2]}{gm[3]}");   //電腦選號結果
                while (true)
                {
                    Console.WriteLine("猜一波唄");
                    string input = Console.ReadLine();
                    if (input.Length == 4)
                    {

                        int[] guess = input.Select(x => int.Parse(x.ToString())).ToArray();  //把輸入的字串轉數字再存進int array裡面
                        int a = gm.Where((i, j) => guess[j] == i).Count(); //位置完全正確的
                        int b = gm.Intersect(guess).Count() - a; //差集扣掉位置完全正確的
                        if (a == 4)
                        {
                            Console.WriteLine("你終於猜對啦");
                            break;
                        }
                        else
                        {
                            Console.WriteLine($"{a}A{b}B");
                        }
                    }
                    else
                    {
                        Console.WriteLine("只能輸入四個數字!");
                        continue;
                    }
                }
                Console.WriteLine("你要繼續玩嗎？ yes to continue");
            } while (Console.ReadLine() == "yes");

        }
    }
}

