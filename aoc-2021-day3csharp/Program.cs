using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AOC_CSharp;

namespace aoc_2021_day3csharp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            AOC aoc = new AOC(2021, 3);

            aoc.AddTestCase(1, @"00100
11110
10110
10111
10101
01111
00111
11100
10000
11001
00010
01010", 198);
            aoc.Solve(1, i =>
            {
                var lines = i.GetLines();
                int gamma = 0;
                int epsilon = 0;
                for (int j = 0; j < lines[0].Length; ++j)
                {
                    // each bit.
                    int count_1s = 0;
                    int count_0s = 0;
                    foreach (var l in lines)
                    {
                        if (l.Length == 0) continue;
                        if (l[j] == '1') { count_1s++; }
                        if (l[j] == '0') { count_0s++; }
                    }
                    if (count_1s > count_0s)
                    {
                        gamma = gamma * 2 + 1;
                        epsilon = epsilon * 2;
                    } else if (count_0s > count_1s)
                    {
                        gamma = gamma * 2;
                        epsilon = epsilon * 2 + 1;
                    }
                }
                return "" + (gamma * epsilon);
            });

            aoc.AddTestCase(2, @"00100
11110
10110
10111
10101
01111
00111
11100
10000
11001
00010
01010", 230);
            aoc.Solve(2, i =>
            {
                var lines = i.GetLines();
                var o2_set = lines.ToList();
                var co2_set = lines.ToList();

                for (int j = 0; j < o2_set[0].Length; ++j)
                {
                    o2_set.RemoveAll(x=>x=="");
                    if (o2_set.Count == 1) break;

                    // each bit.
                    int count_1s = 0;
                    int count_0s = 0;
                    foreach (var l in o2_set)
                    {
                        if (l[j] == '1') { count_1s++; }
                        if (l[j] == '0') { count_0s++; }
                    }

                    if (count_1s >= count_0s)
                    {
                        o2_set.RemoveAll(x => x[j] == '0');
                    }
                    if (count_1s < count_0s)
                    {
                        o2_set.RemoveAll(x => x[j] == '1');
                    }
                }

                for (int j = 0; j < co2_set[0].Length; ++j)
                {
                    co2_set.RemoveAll(x => x == "");
                    if (co2_set.Count == 1) break;
                    // each bit.
                    int count_1s = 0;
                    int count_0s = 0;
                    foreach (var l in co2_set)
                    {
                        if (l.Length == 0) continue;
                        if (l[j] == '1') { count_1s++; }
                        if (l[j] == '0') { count_0s++; }
                    }
                    if (count_1s >= count_0s)
                    {
                        co2_set.RemoveAll(x => x[j] == '1');
                    }
                    if (count_1s < count_0s)
                    {
                        co2_set.RemoveAll(x => x[j] == '0');
                    }
                }

                int xx = Convert.ToInt32(o2_set[0], 2);
                int yy = Convert.ToInt32(co2_set[0], 2);

                return "" + (xx*yy);
            });
            Console.WriteLine("done");
        }
    }
}
