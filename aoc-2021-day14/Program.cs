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
            AOC aoc = new AOC(2021, 14);

            aoc.AddTestCase(1, @"NNCB

CH -> B
HH -> N
CB -> H
NH -> C
HB -> C
HC -> B
HN -> C
NN -> C
BH -> H
NC -> B
NB -> B
BN -> B
BB -> N
BC -> B
CC -> N
CN -> C", 1588);

            aoc.Solve(1, ii =>
            {
                var lines = ii.GetLines();
                var template = lines[0];
                var insertions = lines.Where(s => s.Contains('-')).Select(s => {
                    char[] seps = new char[] { '-', '>' };
                    var parts = s.Split(seps, StringSplitOptions.RemoveEmptyEntries).Select(s2 => s2.Trim()).ToList();
                    return new Tuple<string, string>(parts[0], parts[1]);
                }).ToList();

                // turn into a map:
                Dictionary<string, char> ins = new Dictionary<string, char>();
                foreach (var i in insertions)
                {
                    ins.Add(i.Item1, i.Item2[0]);
                }

                for (int j = 0; j < 10; ++j)
                {
                    String next = "";
                    for (int i = 0; i < template.Count(); ++i)
                    {
                        string insertion = "";
                        if (i > 0)
                        {
                            string prev = "" + template[i - 1] + template[i];
                            if (ins.ContainsKey(prev))
                            {
                                insertion = "" + ins[prev];
                            }
                        }
                        next = next + insertion + template[i];
                    }
                    template = next;
                    next = "";
                }

                // most common count - least common count.
                Dictionary<char, int> counts = new Dictionary<char, int>();
                foreach (char c in template)
                {
                    if (!counts.ContainsKey(c))
                    {
                        counts.Add(c, 1);
                    } else
                    {
                        counts[c]++;
                    }
                }

                int min = int.MaxValue;
                int max = int.MinValue;
                foreach (var p in counts)
                {
                    min = Math.Min(min, p.Value);
                    max = Math.Max(max, p.Value);
                }

                return max - min;
            });

            //aoc.Solve(2, ii =>
            //{
            //    var lines = ii.GetLines();
            //    var template = lines[0];
            //    var insertions = lines.Where(s => s.Contains('-')).Select(s => {
            //        char[] seps = new char[] { '-', '>' };
            //        var parts = s.Split(seps, StringSplitOptions.RemoveEmptyEntries).Select(s2 => s2.Trim()).ToList();
            //        return new Tuple<string, string>(parts[0], parts[1]);
            //    }).ToList();

            //    // turn into a map:
            //    Dictionary<string, char> ins = new Dictionary<string, char>();
            //    foreach (var i in insertions)
            //    {
            //        ins.Add(i.Item1, i.Item2[0]);
            //    }

            //    for (int j = 0; j < 40; ++j)
            //    {
            //        String next = "";
            //        for (int i = 0; i < template.Count(); ++i)
            //        {
            //            string insertion = "";
            //            if (i > 0)
            //            {
            //                string prev = "" + template[i - 1] + template[i];
            //                if (ins.ContainsKey(prev))
            //                {
            //                    insertion = "" + ins[prev];
            //                }
            //            }
            //            next = next + insertion + template[i];
            //        }
            //        template = next;
            //        next = "";
            //    }

            //    // most common count - least common count.
            //    Dictionary<char, UInt64> counts = new Dictionary<char, UInt64>();
            //    foreach (char c in template)
            //    {
            //        if (!counts.ContainsKey(c))
            //        {
            //            counts.Add(c, 1);
            //        }
            //        else
            //        {
            //            counts[c]++;
            //        }
            //    }

            //    UInt64 min = UInt64.MaxValue;
            //    UInt64 max = UInt64.MinValue;
            //    foreach (var p in counts)
            //    {
            //        min = Math.Min(min, p.Value);
            //        max = Math.Max(max, p.Value);
            //    }

            //    return "" + (max - min);
            //});

            Dictionary<Tuple<string, int>, Dictionary<char, UInt64>> cache = new Dictionary<Tuple<string, int>, Dictionary<char, UInt64>>();

            Dictionary<char, UInt64> Count(string pair, Dictionary<string, char> map, int depth)
            {
                if (cache.ContainsKey(new Tuple<string, int>(pair, depth)))
                {
                    return cache[new Tuple<string, int>(pair, depth)];
                }

                Dictionary<char, UInt64> ret = new Dictionary<char, UInt64>();

                if (depth > 0 && map.ContainsKey(pair))
                {
                    var part1 = Count("" + pair[0] + map[pair], map, depth-1);
                    var part2 = Count("" + map[pair] + pair[1], map, depth - 1);

                    foreach (var p in part1)
                    {
                        ret.Add(p.Key, p.Value);
                    }

                    foreach (var p in part2)
                    {
                        if (ret.ContainsKey(p.Key))
                        {
                            ret[p.Key] += p.Value;
                        }
                        else
                        {
                            ret.Add(p.Key, p.Value);
                        }
                    }
                    ret[map[pair]]--;
                }
                else
                {
                    if (pair[0] == pair[1])
                    {
                        ret.Add(pair[0], 2);
                    } else {
                        ret.Add(pair[0], 1);
                        ret.Add(pair[1], 1);
                    }
                }
                cache.Add(new Tuple<string, int>(pair, depth), ret);
                return ret;
            }

            aoc.AddTestCase(2, @"NNCB

CH -> B
HH -> N
CB -> H
NH -> C
HB -> C
HC -> B
HN -> C
NN -> C
BH -> H
NC -> B
NB -> B
BN -> B
BB -> N
BC -> B
CC -> N
CN -> C", "2188189693529");
            

            aoc.Solve(2, ii =>
            {
                cache = new Dictionary<Tuple<string, int>, Dictionary<char, ulong>>();
                var lines = ii.GetLines();
                var template = lines[0];
                var insertions = lines.Where(s => s.Contains('-')).Select(s => {
                    char[] seps = new char[] { '-', '>' };
                    var parts = s.Split(seps, StringSplitOptions.RemoveEmptyEntries).Select(s2 => s2.Trim()).ToList();
                    return new Tuple<string, string>(parts[0], parts[1]);
                }).ToList();

                // turn into a map:
                Dictionary<string, char> ins = new Dictionary<string, char>();
                foreach (var i in insertions)
                {
                    ins.Add(i.Item1, i.Item2[0]);
                }

                Dictionary<char, UInt64> counts = new Dictionary<char, UInt64>();
                for (int i = 0; i < template.Count() - 1; ++i)
                {
                    var next_parts = Count(template.Substring(i, 2), ins, 40);
                    foreach (var p in next_parts)
                    {
                        if (counts.ContainsKey(p.Key))
                        {
                            counts[p.Key] += p.Value;
                        }
                        else
                        {
                            counts.Add(p.Key, p.Value);
                        }
                    }
                    if (i > 1)
                        counts[template[i]]--; // don't double count template.
                }

                UInt64 min = UInt64.MaxValue;
                UInt64 max = UInt64.MinValue;
                foreach (var p in counts)
                {
                    min = Math.Min(min, p.Value);
                    max = Math.Max(max, p.Value);
                }

                return "" + (max - min);
            });

            Console.WriteLine("done");
            Console.ReadKey();
        }
    }
}
