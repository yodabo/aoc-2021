using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AOC_CSharp;

namespace aoc_2021_day8
{
    internal class Program
    {
        static void Main(string[] args)
        {
            AOC aoc = new AOC(2021, 8);

            aoc.AddTestCase(1, @"acedgfb cdfbe gcdfa fbcad dab cefabd cdfgeb eafb cagedb ab |cdfeb fcadb cdfeb cdbaf", 0);
            aoc.AddTestCase(1, @"be cfbegad cbdgef fgaecd cgeb fdcge agebfd fecdb fabcd edb |fdgacbe cefdb cefbgd gcbe
edbfga begcd cbg gc gcadebf fbgde acbgfd abcde gfcbed gfec |fcgedb cgb dgebacf gc
fgaebd cg bdaec gdafb agbcfd gdcbef bgcad gfac gcb cdgabef |cg cg fdcagb cbg
fbegcd cbd adcefb dageb afcb bc aefdc ecdab fgdeca fcdbega |efabcd cedba gadfec cb
aecbfdg fbg gf bafeg dbefa fcge gcbea fcaegb dgceab fcbdga |gecf egdcabf bgf bfgea
fgeab ca afcebg bdacfeg cfaedg gcfdb baec bfadeg bafgc acf |gebdcfa ecba ca fadegcb
dbcfg fgd bdegcaf fgec aegbdf ecdfab fbedc dacgb gdcebf gf |cefg dcbef fcge gbcadfe
bdfegc cbegaf gecbf dfcage bdacg ed bedf ced adcbefg gebcd |ed bcgafe cdgba cbgef
egadfb cdbfeg cegd fecab cgb gbdefca cg fgcdab egfdb bfceg |gbdfcae bgc cg cgb
gcafb gcf dcaebfg ecagb gf abcdeg gaef cafbge fdbac fegbdc |fgae cfgab fg bagce", 26);
            aoc.Solve(1, i =>
            {
                // 10 unique patterns, | deliminator, four digit output value
                var input = i.GetLines().Select(l => l.Trim().Split(new char[] { ' ', '|' }, StringSplitOptions.RemoveEmptyEntries).Select(s => String.Concat(s.OrderBy(c=>c))/*{ var b = new HashSet<char>(); foreach (char c in s) { b.Add(c); } return b; }*/).ToList()).Where(aa=>aa.Count == 14).ToList();

                int sum = 0;

                // length if 10.
                foreach (var ii in input)
                {
                    // difference of 1 and 7 is top:
                    string one = ii.Where(s => s.Length == 2).ToList()[0];
                    string seven = ii.Where(s => s.Length == 3).ToList()[0];
                    string four = ii.Where(s => s.Length == 4).ToList()[0];
                    string eight = ii.Where(s => s.Length == 7).ToList()[0];

                    char top = seven.ToCharArray().Where(c => !one.Contains(c)).ToList()[0];

                    // one of 2,3,5 has an intersection with 1 of 2.
                    string three = ii.Where(s =>
                    {
                        if (s.Length != 3) return false;
                        int n = 0;
                        for (int j = 0; j < 3; ++j)
                        {
                            if (one.Contains(s[j])) n++;
                        }
                        return n == 2;
                    }).ToList()[0];

                    for (int j = 10; j < 14; ++j)
                    {
                        if (ii[j] == one) sum++;
                        if (ii[j] == four) sum++;
                        if (ii[j] == seven) sum++;
                        if (ii[j] == eight) sum++;
                    }
                    
                }

                return sum;
            });

            aoc.AddTestCase(2, @"be cfbegad cbdgef fgaecd cgeb fdcge agebfd fecdb fabcd edb |fdgacbe cefdb cefbgd gcbe
edbfga begcd cbg gc gcadebf fbgde acbgfd abcde gfcbed gfec |fcgedb cgb dgebacf gc
fgaebd cg bdaec gdafb agbcfd gdcbef bgcad gfac gcb cdgabef |cg cg fdcagb cbg
fbegcd cbd adcefb dageb afcb bc aefdc ecdab fgdeca fcdbega |efabcd cedba gadfec cb
aecbfdg fbg gf bafeg dbefa fcge gcbea fcaegb dgceab fcbdga |gecf egdcabf bgf bfgea
fgeab ca afcebg bdacfeg cfaedg gcfdb baec bfadeg bafgc acf |gebdcfa ecba ca fadegcb
dbcfg fgd bdegcaf fgec aegbdf ecdfab fbedc dacgb gdcebf gf |cefg dcbef fcge gbcadfe
bdfegc cbegaf gecbf dfcage bdacg ed bedf ced adcbefg gebcd |ed bcgafe cdgba cbgef
egadfb cdbfeg cegd fecab cgb gbdefca cg fgcdab egfdb bfceg |gbdfcae bgc cg cgb
gcafb gcf dcaebfg ecagb gf abcdeg gaef cafbge fdbac fegbdc |fgae cfgab fg bagce", 61229);
            aoc.Solve(2, i =>
            {
                // 10 unique patterns, | deliminator, four digit output value
                var input = i.GetLines().Select(l => l.Trim().Split(new char[] { ' ', '|' }, StringSplitOptions.RemoveEmptyEntries).Select(s => String.Concat(s.OrderBy(c => c))/*{ var b = new HashSet<char>(); foreach (char c in s) { b.Add(c); } return b; }*/).ToList()).Where(aa => aa.Count == 14).ToList();

                int sum = 0;

                // length if 10.
                foreach (var ii in input)
                {
                    // difference of 1 and 7 is top:
                    string one = ii.Where(s => s.Length == 2).ToList()[0];
                    string seven = ii.Where(s => s.Length == 3).ToList()[0];
                    string four = ii.Where(s => s.Length == 4).ToList()[0];
                    string eight = ii.Where(s => s.Length == 7).ToList()[0];

                    char top = seven.ToCharArray().Where(c => !one.Contains(c)).ToList()[0];

                    // one of 2,3,5 has an intersection with 1 of 2.
                    string three = ii.Where(s =>
                    {
                        if (s.Length != 5) return false;
                        int n = 0;
                        for (int j = 0; j < 5; ++j)
                        {
                            if (one.Contains(s[j])) n++;
                        }
                        return n == 2;
                    }).ToList()[0];

                    // union of 3 and 4 contains all but bottom left.
                    String all_but_bottom_left = three + four;
                    char bottom_left = eight.Where(c => !all_but_bottom_left.Contains(c)).Single();

                    // 4 with knowledge of top and bottom left tells us bottom
                    String all_but_bottom = four + bottom_left + top;
                    char bottom = eight.Where(c => !all_but_bottom.Contains(c)).Single();

                    // middle is where 3-1-top-bottom
                    char middle = three.Where(c => !one.Contains(c) && c != top && c != bottom).Single();

                    String two = ii.Where(s => s.Length == 5 && s.Contains(bottom_left)).ToList()[0];
                    String five = ii.Where(s => s.Length == 5 && s != two && s != three).ToList()[0];

                    String zero = ii.Where(s => s.Length == 6 && !s.Contains(middle)).ToList()[0];
                    String nine = ii.Where(s => s.Length == 6 && !s.Contains(bottom_left)).ToList()[0];
                    String six = ii.Where(s => s.Length == 6 && s != zero && s != nine).ToList()[0];

                    // 2 vs. 5 - 2 contains bottom left.
                    // 

                    for (int j = 0; j < 10; ++j)
                    {
                        // 2 segments: 1
                        // 3 segments: 7
                        // 4 segments: 4
                        // 5 segments: 2,3,5
                        // 6 segments: 0,6,9
                        // 7 segments: 8
                    }

                    int num = 0;
                    for (int j = 10; j < 14; ++j)
                    {
                        num *= 10;
                        if (ii[j] == one) num += 1;
                        if (ii[j] == two) num += 2;
                        if (ii[j] == three) num += 3;
                        if (ii[j] == four) num += 4;
                        if (ii[j] == five) num += 5;
                        if (ii[j] == six) num += 6;
                        if (ii[j] == seven) num += 7;
                        if (ii[j] == eight) num += 8;
                        if (ii[j] == nine) num += 9;
                    }
                    sum += num;

                }

                return sum;
            });
            Console.WriteLine("done");
        }
    }
}
