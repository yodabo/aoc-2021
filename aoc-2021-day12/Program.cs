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
            AOC aoc = new AOC(2021, 12);

            aoc.AddTestCase(1, @"start-A
start-b
A-c
A-b
b-d
A-end
b-end", 10);

            aoc.AddTestCase(1, @"dc-end
HN-start
start-kj
dc-start
dc-HN
LN-dc
HN-end
kj-sa
kj-HN
kj-dc", 19);

            aoc.AddTestCase(1, @"fs-end
he-DX
fs-he
start-DX
pj-DX
end-zg
zg-sl
zg-pj
pj-he
RW-he
fs-DX
pj-RW
zg-RW
start-pj
he-WI
zg-he
pj-fs
start-RW", 226);

            aoc.Solve(1, ii =>
            {
                Dictionary<string, HashSet<string>> map = new Dictionary<string, HashSet<string>>();
                var input = ii.GetLines().Select(s=>s.Trim()).Where(s=> s != "").ToList();
                foreach (var line in input)
                {
                    var parts = line.Split('-');

                    Add(ref map, parts[0], parts[1]);
                    Add(ref map, parts[1], parts[0]);
                }

                // walk start to end. Only visit "small" rooms once
                Queue<Path> ToExpand = new Queue<Path>();
                ToExpand.Enqueue(new Path("start", null));

                int count = 0;
                while (ToExpand.Count > 0)
                {
                    var node = ToExpand.Dequeue();
                    if (node.IsComplete()) { count++; continue; }

                    foreach (var next in map[node.room_name])
                    {
                        if (node.CanGo(next, map))
                        {
                            ToExpand.Enqueue(new Path(next, node));
                        }
                    }

                }


                return count;
            });

            aoc.AddTestCase(2, @"start-A
start-b
A-c
A-b
b-d
A-end
b-end", 36);

            aoc.AddTestCase(2, @"dc-end
HN-start
start-kj
dc-start
dc-HN
LN-dc
HN-end
kj-sa
kj-HN
kj-dc", 103);

            aoc.AddTestCase(2, @"fs-end
he-DX
fs-he
start-DX
pj-DX
end-zg
zg-sl
zg-pj
pj-he
RW-he
fs-DX
pj-RW
zg-RW
start-pj
he-WI
zg-he
pj-fs
start-RW", 3509);

            aoc.Solve(2, ii =>
            {
                Dictionary<string, HashSet<string>> map = new Dictionary<string, HashSet<string>>();
                var input = ii.GetLines().Select(s => s.Trim()).Where(s => s != "").ToList();
                foreach (var line in input)
                {
                    var parts = line.Split('-');

                    Add(ref map, parts[0], parts[1]);
                    Add(ref map, parts[1], parts[0]);
                }

                // walk start to end. Only visit "small" rooms once
                Queue<Path> ToExpand = new Queue<Path>();
                ToExpand.Enqueue(new Path("start", null));

                int count = 0;
                while (ToExpand.Count > 0)
                {
                    var node = ToExpand.Dequeue();
                    //node.Dump(); Console.WriteLine();
                    if (node.IsComplete()) { count++; continue; }

                    foreach (var next in map[node.room_name])
                    {
                        if (node.CanGo2(next, map))
                        {
                            ToExpand.Enqueue(new Path(next, node));
                        }
                    }

                }


                return count;
            });

            // faster solution.
            aoc.Solve(2, ii =>
            {
                Dictionary<int, List<int>> int_map = new Dictionary<int, List<int>>();
                Dictionary<string, int> to_int = new Dictionary<string, int>(); // negative for small.

                int unique_big = 1;
                int unique_small = -1;

                var input = ii.GetLines().Select(s => s.Trim()).Where(s => s != "").ToList();
                foreach (var line in input)
                {
                    var parts = line.Split('-');

                    int id1;
                    if (!to_int.ContainsKey(parts[0]))
                    {
                        if (parts[0].ToUpper() == parts[0])
                        {
                            id1 = unique_big;
                            unique_big++;
                        } else
                        {
                            id1 = unique_small;
                            unique_small--;
                        }
                        to_int.Add(parts[0], id1);
                        int_map.Add(id1, new List<int>());
                    } else
                    {
                        id1 = to_int[parts[0]];
                    }

                    int id2;
                    if (!to_int.ContainsKey(parts[1]))
                    {
                        if (parts[1].ToUpper() == parts[1])
                        {
                            id2 = unique_big;
                            unique_big++;
                        }
                        else
                        {
                            id2 = unique_small;
                            unique_small--;
                        }
                        to_int.Add(parts[1], id2);
                        int_map.Add(id2, new List<int>());
                    }
                    else
                    {
                        id2 = to_int[parts[1]];
                    }

                    int_map[id1].Add(id2);
                    int_map[id2].Add(id1);
                }

                int start = to_int["start"];
                int end = to_int["end"];
                
                HashSet<int> path = new HashSet<int>();
                return Recurse(start, ref path, false, ref int_map, end, start);
            });

            Console.WriteLine("done");
            Console.ReadKey();
        }

        static int Recurse(int node, ref HashSet<int> existing, bool already_have_duplicate, ref Dictionary<int, List<int>> map, int end, int start)
        {
            int result = 0;
            foreach(int child in map[node])
            {
                if (child == end) { result++; continue; }
                if (child == start) { continue; }

                bool duplicate = child < 0 && existing.Contains(child);
                if (child < 0 && !duplicate)
                {
                    existing.Add(child);
                }
                if (already_have_duplicate && duplicate) continue;

                result += Recurse(child, ref existing, already_have_duplicate || duplicate, ref map, end, start);

                if (child < 0 && !duplicate)
                {
                    existing.Remove(child);
                }
            }

            return result;
        }

        static void Add(ref Dictionary<string, HashSet<string>> map, string a, string b)
        {
            if (!map.ContainsKey(a))
            {
                map.Add(a, new HashSet<string> { b });
            }
            else
            {
                map[a].Add(b);
            }
        }

        class Path
        {
            public void Dump()
            {
                if (parent != null) { parent.Dump(); Console.Write(","); }
                Console.Write(room_name);
            }

            bool has_double = false;
            public Path(string room_name, Path parent)
            {
                if (parent != null && parent.has_double) this.has_double = true;
                else if (parent != null)
                {
                    this.has_double = IsSmall(room_name) && parent.Contains(room_name);
                }

                this.room_name = room_name;
                this.parent = parent;
            }

            public bool IsComplete()
            {
                return room_name == "end";
            }

            public bool CanGo(string next_room, Dictionary<string, HashSet<string>> map)
            {
                // can we go to nextroom, and get to end without visiting same small room multiple times?
                if (IsSmall(next_room) && Contains(next_room)) { return false; }

                // Can we walk to end without violating rules?
                /// what if we just return true here?
                return true;
            }

            public bool CanGo2(string next_room, Dictionary<string, HashSet<string>> map)
            {
                if (next_room == "start") return false; // if we get to end, we won't keep going...

                // can we go to nextroom, and get to end without visiting same small room multiple times?
                if (IsSmall(next_room) && Contains(next_room) && has_double) { return false; }

                // Can we walk to end without violating rules?
                /// what if we just return true here?
                return true;
            }

            public bool Contains(string room)
            {
                return room_name == room || (parent != null && parent.Contains(room));
            }

            bool IsSmall(string a)
            {
                return a.ToUpper() != a;
            }

            public String room_name = null;
            Path parent;
        }

    }
}
