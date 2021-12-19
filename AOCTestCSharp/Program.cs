using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.IO;

using AOC_CSharp;

namespace AOCTestCSharp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            AOC aoc = new AOC(2018, 2);
            aoc.AddTestCase(1,
                @"abcdef
bababc
abbcde
abcccd
aabcdd
abcdee
ababab", 12);
            aoc.Solve(1, i =>
            {
                int num2 = 0;
                int num3 = 0;

                var lines = i.GetLines();
                foreach (String l in lines)
                {
                    Dictionary<char, int> counts = new Dictionary<char, int>();
                    foreach (char c in l)
                    {
                        if (!counts.ContainsKey(c)) counts.Add(c, 1);
                        else counts[c] = counts[c] + 1;
                    }
                    bool has2 = false;
                    bool has3 = false;
                    foreach (var kv in counts)
                    {
                        if (kv.Value == 2 && !has2)
                        {
                            has2 = true;
                            num2++;
                        }
                        if (kv.Value == 3 && !has3)
                        {
                            has3 = true;
                            num3++;
                        }
                    }
                    if (aoc.Testing())
                    {
                        Console.WriteLine("{0} {1} {2}", l, has2, has3);
                    }
                }
                return num2 * num3;
            });

            aoc.AddTestCase(2,
              @"abcde
fghij
klmno
pqrst
fguij
axcye
wvxyz", "fgij");
            aoc.Solve(2, input =>
            {
                var lines = input.GetLines();
                foreach (String l in lines)
                {
                    foreach (String l2 in lines)
                    {
                        int num = 0;
                        if (l.Length != l2.Length) break;
                        for (int i = 0; i < l.Length; ++i)
                        {
                            if (l[i] != l2[i]) num++;
                            if (num > 1) break;
                        }
                        if (num == 1)
                        {
                            string ret = "";
                            for (int i = 0; i < l.Length; ++i)
                            {
                                if (l[i] == l2[i])
                                {
                                    ret = ret + l[i];
                                }
                            }
                            return ret;
                        }
                    }
                }
                return null; // todo: make this an error
            });


            // aoc.AddTestCase(1, @"raw data", 5);
            // aoc.AddTestCase(1, @"raw data2", 7);
            // aoc.Solve(1, i => {
            // });


            //var input = aoc.GetInput();
            //var lines = input.GetLines();
            //var ints = input.GetNums();
            //var doubles = input.GetDoubles();
            //var s = input.GetString();
            Console.WriteLine("done");
        }
    }
}
