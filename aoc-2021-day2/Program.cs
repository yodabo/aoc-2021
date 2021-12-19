using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AOC_CSharp;

namespace aoc_2021_day2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            AOC aoc = new AOC(2021, 2);

            aoc.AddTestCase(1, @"forward 5
down 5
forward 8
up 3
down 8
forward 2", 150);
            aoc.Solve(1, i =>
            {
                int x = 0;
                int y = 0;
                foreach (var s in i.GetLines())
                {
                    if (s.StartsWith("forward"))
                    {
                        int amt = int.Parse(s.Split()[1]);
                        x+=amt;
                    } else if (s.StartsWith("down"))
                    {
                        int amt = int.Parse(s.Split()[1]);
                        y+=amt;
                    }
                    else if (s.StartsWith("up"))
                    {
                        int amt = int.Parse(s.Split()[1]);
                        y-=amt;
                    }
                    Console.WriteLine("" + x + " " + y);
                }
                return "" + x * y;
            });

            aoc.AddTestCase(2, @"forward 5
down 5
forward 8
up 3
down 8
forward 2", 900);
            aoc.Solve(2, i =>
            {
                int x = 0;
                int y = 0;
                int aim = 0;
                foreach (var s in i.GetLines())
                {
                    if (s.StartsWith("forward"))
                    {
                        int amt = int.Parse(s.Split()[1]);
                        x += amt;
                        y += aim * amt;
                    }
                    else if (s.StartsWith("down"))
                    {
                        int amt = int.Parse(s.Split()[1]);
                        aim += amt;
                    }
                    else if (s.StartsWith("up"))
                    {
                        int amt = int.Parse(s.Split()[1]);
                        aim -= amt;
                    }
                }
                return "" + x * y;
            });
            Console.WriteLine("done");
        }
    }
}
