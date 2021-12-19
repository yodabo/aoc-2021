using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AOC_CSharp;

namespace aoc_2021_day7
{
    internal class Program
    {
        static void Main(string[] args)
        {
            AOC aoc = new AOC(2021, 7);

            aoc.AddTestCase(1, @"16,1,2,0,4,2,7,1,2,14", 37);
            aoc.Solve(1, i =>
            {
                // find median - sum difference from median
                var nums = i.GetLines()[0].Trim().Split(',').Select(x => int.Parse(x)).ToList();
                nums.Sort();
                int median = nums[nums.Count / 2];
                int sum = 0;
                foreach (var n in nums)
                {
                    sum += Math.Abs(n - median);
                }
                return sum;
            });

            aoc.AddTestCase(2, @"16,1,2,0,4,2,7,1,2,14", 168);
            aoc.Solve(2, i =>
            {
                // find median - sum difference from median
                var nums = i.GetLines()[0].Trim().Split(',').Select(x => int.Parse(x)).ToList();
                nums.Sort();
                int mean = nums.Sum() / nums.Count;
                double expected = (double)nums.Average() - 0.5;
                int min = int.MaxValue;
                int bestj = -1;
                for (int j = nums.Min(); j <= nums.Max(); ++j)
                {
                    int sum = 0;
                    foreach (var n in nums)
                    {
                        // 0, 1, 3, 6, 10, .. (n)(n+1)/2
                        int diff = Math.Abs(n - j);
                        sum += diff * (diff + 1) / 2;
                    }
                    if (sum < min)
                    {
                        bestj = j;
                        min = sum;
                    }
                }
                Console.WriteLine("bestj: {0}", bestj);
                return min;
            });
            Console.WriteLine("done");
        }
    }
}
