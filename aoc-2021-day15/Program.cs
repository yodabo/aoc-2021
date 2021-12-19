using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AOC_CSharp;

namespace aoc_2021_day15
{
    internal class Program
    {
        static void Main(string[] args)
        {
            AOC aoc = new AOC(2021, 15);

            aoc.AddTestCase(1, @"1163751742
1381373672
2136511328
3694931569
7463417111
1319128137
1359912421
3125421639
1293138521
2311944581", 40);

            aoc.Solve(1, ii =>
            {
                var risks = ii.GetIntGrid();
                Dictionary<Tuple<int, int>, int> bestScore = new Dictionary<Tuple<int, int>, int>();
                Queue<Tuple<int, int>> toExpand = new Queue<Tuple<int, int>>();

                bestScore.Add(new Tuple<int, int>(0, 0), 0);
                toExpand.Enqueue(new Tuple<int, int>(0, 0));

                HashSet<Tuple<int, int>> stuffInQueue = new HashSet<Tuple<int, int>>();
                stuffInQueue.Add(new Tuple<int, int>(0, 0));
                while (toExpand.Any())
                {
                    // keep going.
                    var cur = toExpand.Dequeue();
                    stuffInQueue.Remove(cur);
                    var neighbors = new List<Tuple<int, int>>();
                    if (cur.Item1 > 0)
                        neighbors.Add(new Tuple<int, int>(cur.Item1-1, cur.Item2));
                    if (cur.Item1 < risks[0].Count - 1)
                        neighbors.Add(new Tuple<int, int>(cur.Item1 + 1, cur.Item2));
                    if (cur.Item2 > 0)
                        neighbors.Add(new Tuple<int, int>(cur.Item1, cur.Item2-1));
                    if (cur.Item2 < risks.Count - 1) 
                        neighbors.Add(new Tuple<int, int>(cur.Item1, cur.Item2 + 1));

                    foreach (var n in neighbors)
                    {
                        if (!bestScore.ContainsKey(n) || bestScore[n] > bestScore[cur] + risks[n.Item1][n.Item2])
                        {
                            if (!bestScore.ContainsKey(n)) bestScore.Add(n, 0);
                            bestScore[n] = bestScore[cur] + risks[n.Item1][n.Item2];
                            if (!stuffInQueue.Contains(n))
                            {
                                toExpand.Enqueue(n);
                                stuffInQueue.Add(n);
                            }
                        }
                    }
                }

                return bestScore[new Tuple<int, int>(risks[0].Count - 1, risks.Count - 1)];
            });

            aoc.AddTestCase(2, @"1163751742
1381373672
2136511328
3694931569
7463417111
1319128137
1359912421
3125421639
1293138521
2311944581", 315);

            aoc.Solve(2, ii =>
            {
                var risks = ii.GetIntGrid();

                int x_orig = risks[0].Count;
                int y_orig = risks.Count;
                for (int i = 0; i < 5; ++i)
                {
                    for (int j = 0; j < 5; ++j)
                    {
                        // Don't need to add original copy.
                        if (i == 0 && j == 0) continue;

                        for (int y = 0; y < y_orig; ++y)
                        {
                            if (risks.Count() <= y + y_orig * i)
                            {
                                risks.Add(new List<int>());
                            }

                            for (int x = 0; x < x_orig; ++x)
                            {
                                risks[y + y_orig * i].Add(1 + (risks[y][x] - 1 + j + i) % 9);
                            }
                        }
                    }
                }

                Dictionary<Tuple<int, int>, int> bestScore = new Dictionary<Tuple<int, int>, int>();
                Queue<Tuple<int, int>> toExpand = new Queue<Tuple<int, int>>();

                bestScore.Add(new Tuple<int, int>(0, 0), 0);
                toExpand.Enqueue(new Tuple<int, int>(0, 0));

                HashSet<Tuple<int, int>> stuffInQueue = new HashSet<Tuple<int, int>>();
                stuffInQueue.Add(new Tuple<int, int>(0, 0));
                while (toExpand.Any())
                {
                    // keep going.
                    var cur = toExpand.Dequeue();
                    stuffInQueue.Remove(cur);
                    var neighbors = new List<Tuple<int, int>>();
                    if (cur.Item1 > 0)
                        neighbors.Add(new Tuple<int, int>(cur.Item1 - 1, cur.Item2));
                    if (cur.Item1 < risks[0].Count - 1)
                        neighbors.Add(new Tuple<int, int>(cur.Item1 + 1, cur.Item2));
                    if (cur.Item2 > 0)
                        neighbors.Add(new Tuple<int, int>(cur.Item1, cur.Item2 - 1));
                    if (cur.Item2 < risks.Count - 1)
                        neighbors.Add(new Tuple<int, int>(cur.Item1, cur.Item2 + 1));

                    foreach (var n in neighbors)
                    {
                        if (!bestScore.ContainsKey(n) || bestScore[n] > bestScore[cur] + risks[n.Item1][n.Item2])
                        {
                            if (!bestScore.ContainsKey(n)) bestScore.Add(n, 0);
                            bestScore[n] = bestScore[cur] + risks[n.Item1][n.Item2];
                            if (!stuffInQueue.Contains(n))
                            {
                                toExpand.Enqueue(n);
                                stuffInQueue.Add(n);
                            }
                        }
                    }
                }

                return bestScore[new Tuple<int, int>(risks[0].Count - 1, risks.Count - 1)];
            });

            aoc.Solve(2, ii =>
            {
                var risks = ii.GetIntGrid();
                
                int x_orig = risks[0].Count;
                int y_orig = risks.Count;
                for (int i = 0; i < 5; ++i)
                {
                    for (int j = 0; j < 5; ++j)
                    {
                        // Don't need to add original copy.
                        if (i == 0 && j == 0) continue;

                        for (int y = 0; y < y_orig; ++y)
                        {
                            if (risks.Count() <= y + y_orig * i)
                            {
                                risks.Add(new List<int>());
                            }

                            for (int x = 0; x < x_orig; ++x)
                            {
                                risks[y + y_orig * i].Add(1 + (risks[y][x] - 1 + j + i) % 9);
                            }
                        }
                    }
                }

                Dictionary<Tuple<int, int>, int> bestScore = new Dictionary<Tuple<int, int>, int>();
                SortedSet<Tuple<int, int, int>> toExpand2 = new SortedSet<Tuple<int, int, int>>(); // score, row, column
                
                bestScore.Add(new Tuple<int, int>(0, 0), 0);
                toExpand2.Add(new Tuple<int, int, int>(0, 0, 0));

                HashSet<Tuple<int, int>> stuffInQueue = new HashSet<Tuple<int, int>>();
                stuffInQueue.Add(new Tuple<int, int>(0, 0));
                while (toExpand2.Any())
                {
                    // technically can break out if we get a score for last element.

                    // keep going.
                    var new_cur = toExpand2.First();
                    toExpand2.Remove(new_cur);
                    var cur = new Tuple<int, int>(new_cur.Item2, new_cur.Item3);
                    
                    var neighbors = new List<Tuple<int, int>>();
                    if (cur.Item1 > 0)
                        neighbors.Add(new Tuple<int, int>(cur.Item1 - 1, cur.Item2));
                    if (cur.Item1 < risks[0].Count - 1)
                        neighbors.Add(new Tuple<int, int>(cur.Item1 + 1, cur.Item2));
                    if (cur.Item2 > 0)
                        neighbors.Add(new Tuple<int, int>(cur.Item1, cur.Item2 - 1));
                    if (cur.Item2 < risks.Count - 1)
                        neighbors.Add(new Tuple<int, int>(cur.Item1, cur.Item2 + 1));

                    foreach (var n in neighbors)
                    {
                        if (!bestScore.ContainsKey(n) || bestScore[n] > bestScore[cur] + risks[n.Item1][n.Item2])
                        {
                            if (!bestScore.ContainsKey(n)) bestScore.Add(n, 0);
                            bestScore[n] = bestScore[cur] + risks[n.Item1][n.Item2];
                            if (!stuffInQueue.Contains(n))
                            {
                                toExpand2.Add(new Tuple<int, int, int>(bestScore[n], n.Item1, n.Item2));
                                stuffInQueue.Add(n);
                            }
                        }
                    }
                }

                return bestScore[new Tuple<int, int>(risks[0].Count - 1, risks.Count - 1)];
            });

            Console.WriteLine("done");
            Console.ReadKey();
        }
    }
}
