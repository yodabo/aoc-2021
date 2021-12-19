using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AOC_CSharp;

namespace aoc_2021_day1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            AOC aoc = new AOC(2021, 1);
//            aoc.AddTestCase(1,
//                @"#1 @ 1,3: 4x4
//#2 @ 3,1: 4x4
//#3 @ 5,5: 2x2", 4);

            aoc.Solve(1, i =>
            {
                int numg = 0;
                //var lines = i.GetLines();
                //var nums = i.GetDoubles();
                var nums = i.GetNums();
                int lasti = 0;
                foreach (var ii in nums)
                {
                    if (ii > lasti)
                    {
                        numg++;
                    }
                    lasti = ii;
                }
                return "" + (numg - 1);
            });
            aoc.AddTestCase(2, @"199
200
208
210
200
207
240
269
260
263", 5);

            aoc.Solve(2, i =>
            {
                int numg = 0;
                //var lines = i.GetLines();
                //var nums = i.GetDoubles();
                var nums = i.GetNums();

                int last_sum = 0;
                for (int j = 2; j < nums.Count; j++)
                {
                    int sum = nums[j] + nums[j - 1] + nums[j - 2];
                    if (sum > last_sum)
                    {
                        numg++;
                    }
                    last_sum = sum;

                }
                return "" + (numg - 1);
            });

            Console.WriteLine("done");
        }
    }
}
