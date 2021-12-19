using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AOC_CSharp;

namespace aoc_2021_day9
{
    internal class Program
    {
        static void Main(string[] args)
        {
            AOC aoc = new AOC(2021, 9);

            aoc.AddTestCase(1, @"2199943210
3987894921
9856789892
8767896789
9899965678", 15);
            aoc.Solve(1, ii =>
            {
                var g = ii.GetCharGrid();
                int risk = 0;
                for (int i = 0; i < g.Count; ++i)
                {
                    for (int j = 0; j < g[i].Count; ++j)
                    {
                        Tuple<int, int>[] dp = new Tuple<int, int>[] { new Tuple<int, int>(1, 0), new Tuple<int, int>(0, 1), new Tuple<int, int>(-1, 0), new Tuple<int, int>(0, -1) };
                        bool not_low = false;
                        foreach (var d in dp)
                        {
                            if (i + d.Item1 < 0 || i + d.Item1 >= g.Count) continue;
                            if (j + d.Item2 < 0 || j + d.Item2 >= g[i].Count) continue;
                            if (g[i + d.Item1][j + d.Item2] <= g[i][j]) { not_low = true; break ; }
                        }
                        if (not_low) continue;
                        risk += 1 + (g[i][j] - '0');
                    }
                }
                return risk;
            });

            aoc.AddTestCase(2, @"2199943210
3987894921
9856789892
8767896789
9899965678", 1134);
            aoc.Solve(2, ii =>
            {
                var g = ii.GetCharGrid();
                
                List<int> BasinSizes = new List<int>();
                for (int i = 0; i < g.Count; ++i)
                {
                    for (int j = 0; j < g[i].Count; ++j)
                    {
                        if (g[i][j] == ' ' || g[i][j] == '9') continue; // already expanded, or nothing to expand.

                        Stack<Tuple<int, int>> ToExpand = new Stack<Tuple<int, int>>();
                        ToExpand.Push(new Tuple<int, int>(i, j));
                        g[i][j] = ' ';
                        int basin_size = 1;

                        while (ToExpand.Any())
                        {
                            var e = ToExpand.Pop();
                            Tuple<int, int>[] dp = new Tuple<int, int>[] { new Tuple<int, int>(1, 0), new Tuple<int, int>(0, 1), new Tuple<int, int>(-1, 0), new Tuple<int, int>(0, -1) };
                            foreach (var d in dp)
                            {
                                if (e.Item1 + d.Item1 < 0 || e.Item1 + d.Item1 >= g.Count) continue;
                                if (e.Item2 + d.Item2 < 0 || e.Item2 + d.Item2 >= g[i].Count) continue;
                                if (g[e.Item1 + d.Item1][e.Item2 + d.Item2] == '9') continue;
                                if (g[e.Item1 + d.Item1][e.Item2 + d.Item2] == ' ') continue;
                                g[e.Item1 + d.Item1][e.Item2 + d.Item2] = ' ';
                                basin_size++;

                                ToExpand.Push(new Tuple<int, int>(e.Item1 + d.Item1, e.Item2 + d.Item2));
                            }
                        }
                        BasinSizes.Add(basin_size);
                    }
                }
                BasinSizes.Sort();
                return BasinSizes[BasinSizes.Count - 1] * BasinSizes[BasinSizes.Count - 2] * BasinSizes[BasinSizes.Count - 3];
            });

            Console.WriteLine("done");
        }
    }
}
