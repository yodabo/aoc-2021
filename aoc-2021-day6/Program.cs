using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AOC_CSharp;

namespace aoc_2021_day6
{
    internal class Program
    {
        static UInt64[] Step(UInt64[] state)
        {
            UInt64[] r = new ulong[9];
            
            // generate more
            r[8] += state[0];

            // existing keep reproducing
            r[6] += state[0]; 

            // everything else steps down.
            for (int i = 0; i < state.Length-1; i++)
            {
                r[i] += state[i + 1];
            }
            return r;
        }

        static void Main(string[] args)
        {
            AOC aoc = new AOC(2021, 6);

            aoc.AddTestCase(1, @"3,4,3,1,2", 5934);
            aoc.Solve(1, i =>
            {
                // parse input:
                var nums = i.GetLines().ToArray()[0].Trim().Split(',').Select(s => int.Parse(s)).ToList();
                UInt64[] state = new UInt64[]{ 0, 0, 0, 0, 0, 0, 0, 0, 0 }; // counts of numbers at each state.
                foreach (var n in nums)
                {
                    state[n]++;
                }

                for (int j = 0; j < 80; ++j)
                {
                    state = Step(state);
                }
                // after 80 days?
                UInt64 sum = 0;
                for (int j = 0; j < state.Length; ++j)
                {
                    sum += state[j];
                }
                return "" + sum;
            });

            aoc.AddTestCase(2, @"3,4,3,1,2", "26984457539");
            aoc.Solve(2, i =>
            {
                // parse input:
                var nums = i.GetLines().ToArray()[0].Trim().Split(',').Select(s => int.Parse(s)).ToList();
                UInt64[] state = new UInt64[] { 0, 0, 0, 0, 0, 0, 0, 0, 0 }; // counts of numbers at each state.
                foreach (var n in nums)
                {
                    state[n]++;
                }

                for (int j = 0; j < 256; ++j)
                {
                    state = Step(state);
                }
                // after 256 days?
                UInt64 sum = 0;
                for (int j = 0; j < state.Length; ++j)
                {
                    sum += state[j];
                }
                return "" + sum;
            });
            Console.WriteLine("done");
        }
    }
}
