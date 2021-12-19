using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AOC_CSharp;

namespace aoc_2021_day12
{
    internal class Program
    {
        static void Main(string[] args)
        {
            AOC aoc = new AOC(2021, 13);

            aoc.AddTestCase(1, @"6,10
0,14
9,10
0,3
10,4
4,11
6,0
6,12
4,1
0,13
10,12
3,4
3,0
8,4
1,10
2,14
8,10
9,0

fold along y=7
fold along x=5", 17);

            aoc.Solve(1, ii =>
            {
                // lines with x,y pts
                // blank line
                // lines with "fold along"
                var lines = ii.GetLines();
                List<Tuple<int, int>> pts = new List<Tuple<int, int>>();
                List<Tuple<int, bool>> folds = new List<Tuple<int, bool>>();
                foreach (var line in lines)
                {
                    if (line.StartsWith("fold along"))
                    {
                        bool x = (line.Contains('x'));
                        int pos = int.Parse(line.Split('=')[1]);
                        folds.Add(new Tuple<int, bool>(pos, x));
                    } else if (line.Trim() != "")
                    {
                        var parts = line.Split(',');
                        pts.Add(new Tuple<int, int>(int.Parse(parts[0]), int.Parse(parts[1])));
                    }
                }

                // Count number of points after first fold.
                pts = pts.Select(pt => {
                    // only first transform
                    var fold = folds[0];
                    if (fold.Item2)
                    {
                        // fold on line x=A
                        // stuff <=A stays same
                        // stuff >A goes to 2A-x
                        // ex. if A = 5, 4->5, 5->5, 6->4, 7->3, etc.
                        return (pt.Item1 <= fold.Item1 ? pt : new Tuple<int, int>(2 * fold.Item1 - pt.Item1, pt.Item2));
                    }
                    else
                    {
                        return (pt.Item2 <= fold.Item1 ? pt : new Tuple<int, int>(pt.Item1, 2 * fold.Item1 - pt.Item2));
                    }
                }).ToList();

                return pts.Distinct().Count();
            });

            aoc.Solve(2, ii =>
            {
                // lines with x,y pts
                // blank line
                // lines with "fold along"
                var lines = ii.GetLines();
                List<Tuple<int, int>> pts = new List<Tuple<int, int>>();
                List<Tuple<int, bool>> folds = new List<Tuple<int, bool>>();
                foreach (var line in lines)
                {
                    if (line.StartsWith("fold along"))
                    {
                        bool x = (line.Contains('x'));
                        int pos = int.Parse(line.Split('=')[1]);
                        folds.Add(new Tuple<int, bool>(pos, x));
                    }
                    else if (line.Trim() != "")
                    {
                        var parts = line.Split(',');
                        pts.Add(new Tuple<int, int>(int.Parse(parts[0]), int.Parse(parts[1])));
                    }
                }

                // Count number of points after first fold.
                foreach (var fold in folds)
                {
                    pts = pts.Select(pt =>
                    {
                        if (fold.Item2)
                        {
                            // fold on line x=A
                            // stuff <=A stays same
                            // stuff >A goes to 2A-x
                            // ex. if A = 5, 4->5, 5->5, 6->4, 7->3, etc.
                            return (pt.Item1 <= fold.Item1 ? pt : new Tuple<int, int>(2 * fold.Item1 - pt.Item1, pt.Item2));
                        }
                        else
                        {
                            return (pt.Item2 <= fold.Item1 ? pt : new Tuple<int, int>(pt.Item1, 2 * fold.Item1 - pt.Item2));
                        }
                    }).ToList();
                }
                pts = pts.Distinct().ToList();
                int minx = int.MaxValue;
                int maxx = int.MinValue;
                int miny = int.MaxValue;
                int maxy = int.MinValue;
                foreach (var pt in pts)
                {
                    minx = Math.Min(minx, pt.Item1);
                    maxx = Math.Max(maxx, pt.Item1);

                    miny = Math.Min(miny, pt.Item2);
                    maxy = Math.Max(maxy, pt.Item2);
                }

                for (int y = miny; y <= maxy; ++y)
                {
                    for (int x = minx; x <= maxx; ++x)
                    {
                        if (pts.Contains(new Tuple<int, int>(x, y)))
                            Console.Write('#');
                        else
                            Console.Write(' ');
                    }
                    Console.WriteLine();
                }

                return null;
            });

            Console.WriteLine("done");
            Console.ReadKey();
        }
    }
}
