using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AOC_CSharp;

namespace aoc_2021_day5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            AOC aoc = new AOC(2021, 5);

            aoc.AddTestCase(1, @"0,9 -> 5,9
8,0 -> 0,8
9,4 -> 3,4
2,2 -> 2,1
7,0 -> 7,4
6,4 -> 2,0
0,9 -> 2,9
3,4 -> 1,4
0,0 -> 8,8
5,5 -> 8,2", 5);
            aoc.Solve(1, i =>
            {
                List<Tuple<int, int, int, int>> lines = i.GetLines().Where(s => s.Trim() != "").Select(s =>
                {
                    var parts = s.Split(' ', ',');
                    return new Tuple<int, int, int, int>(int.Parse(parts[0]), int.Parse(parts[1]), int.Parse(parts[3]), int.Parse(parts[4]));
                }).ToList();

                // only hor or vert.
                lines = lines.Where(l => l.Item1 == l.Item3 || l.Item2 == l.Item4).ToList();

                Dictionary<Tuple<int, int>, int> points = new Dictionary<Tuple<int, int>, int>();
                foreach (var line in lines)
                {
                    int dx = 0;
                    int dy = 0;
                    dy = line.Item2 < line.Item4 ? 1 : 
                         line.Item2 == line.Item4 ? 0 : -1;
                    dx = line.Item1 < line.Item3 ? 1 :
                        line.Item1 == line.Item3 ? 0 : -1;
                    int x = line.Item1;
                    int y = line.Item2;
                    do
                    {
                        if (points.ContainsKey(new Tuple<int, int>(x,y)))
                        {
                            points[new Tuple<int, int>(x, y)]++;
                        }
                        else
                        {
                            points.Add(new Tuple<int, int>(x, y), 1);
                        }
                        x += dx;
                        y += dy;
                    } while (x - dx != line.Item3 || y - dy != line.Item4);
                }

                return "" + points.Where(p => p.Value >= 2).Count();
            });

            aoc.AddTestCase(2, @"0,9 -> 5,9
8,0 -> 0,8
9,4 -> 3,4
2,2 -> 2,1
7,0 -> 7,4
6,4 -> 2,0
0,9 -> 2,9
3,4 -> 1,4
0,0 -> 8,8
5,5 -> 8,2", 12);
            aoc.Solve(2, i =>
            {
                List<Tuple<int, int, int, int>> lines = i.GetLines().Where(s => s.Trim() != "").Select(s =>
                {
                    var parts = s.Split(' ', ',');
                    return new Tuple<int, int, int, int>(int.Parse(parts[0]), int.Parse(parts[1]), int.Parse(parts[3]), int.Parse(parts[4]));
                }).ToList();

                // only hor or vert.
                //lines = lines.Where(l => l.Item1 == l.Item3 || l.Item2 == l.Item4).ToList();

                Dictionary<Tuple<int, int>, int> points = new Dictionary<Tuple<int, int>, int>();
                foreach (var line in lines)
                {
                    int dx = 0;
                    int dy = 0;
                    dy = line.Item2 < line.Item4 ? 1 :
                         line.Item2 == line.Item4 ? 0 : -1;
                    dx = line.Item1 < line.Item3 ? 1 :
                        line.Item1 == line.Item3 ? 0 : -1;
                    int x = line.Item1;
                    int y = line.Item2;
                    do
                    {
                        if (points.ContainsKey(new Tuple<int, int>(x, y)))
                        {
                            points[new Tuple<int, int>(x, y)]++;
                        }
                        else
                        {
                            points.Add(new Tuple<int, int>(x, y), 1);
                        }
                        x += dx;
                        y += dy;
                    } while (x - dx != line.Item3 || y - dy != line.Item4);
                }

                return "" + points.Where(p => p.Value >= 2).Count();
            });
            Console.WriteLine("done");
        }
    }
}
