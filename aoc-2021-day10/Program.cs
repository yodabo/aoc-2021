using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AOC_CSharp;

namespace aoc_2021_day10
{
    internal class Program
    {
        static void Main(string[] args)
        {
            AOC aoc = new AOC(2021, 10);

            aoc.AddTestCase(1, @"[({(<(())[]>[[{[]{<()<>>
[(()[<>])]({[<{<<[]>>(
{([(<{}[<>[]}>{[]{[(<()>
(((({<>}<{<{<>}{[]{[]{}
[[<[([]))<([[{}[[()]]]
[{[{({}]{}}([{[{{{}}([]
{<[[]]>}<{[{[{[]{()[[[]
[<(<(<(<{}))><([]([]()
<{([([[(<>()){}]>(<<{{
<{([{{}}[<[[[<>{}]]]>[]]", 26397);
            aoc.Solve(1, ii =>
            {
                var input = ii.GetLines();
                Dictionary<char, char> pairs = new Dictionary<char, char>();
                pairs.Add('[', ']');
                pairs.Add('(', ')');
                pairs.Add('{', '}');
                pairs.Add('<', '>');
                Dictionary<char, int> scores = new Dictionary<char, int>();
                scores.Add(')', 3);
                scores.Add(']', 57);
                scores.Add('}', 1197);
                scores.Add('>', 25137);
                int score = 0;
                foreach (var line in input)
                {
                    Stack<char> stack = new Stack<char>();
                    foreach (char c in line.Trim())
                    {
                        if (pairs.ContainsKey(c))
                        {
                            // push closing token we expect.
                            stack.Push(pairs[c]);
                        }
                        else if (pairs.ContainsValue(c))
                        {
                            char top = stack.Pop();
                            if (c != top)
                            {
                                // expected top, found c.
                                // first illegal character is c.
                                score += scores[c];
                            }
                        }
                    }
                }
                return score;
            });

            aoc.AddTestCase(2, @"[({(<(())[]>[[{[]{<()<>>
[(()[<>])]({[<{<<[]>>(
{([(<{}[<>[]}>{[]{[(<()>
(((({<>}<{<{<>}{[]{[]{}
[[<[([]))<([[{}[[()]]]
[{[{({}]{}}([{[{{{}}([]
{<[[]]>}<{[{[{[]{()[[[]
[<(<(<(<{}))><([]([]()
<{([([[(<>()){}]>(<<{{
<{([{{}}[<[[[<>{}]]]>[]]", 288957);
            aoc.Solve(2, ii =>
            {
                var input = ii.GetLines();
                Dictionary<char, char> pairs = new Dictionary<char, char>();
                pairs.Add('[', ']');
                pairs.Add('(', ')');
                pairs.Add('{', '}');
                pairs.Add('<', '>');
                Dictionary<char, UInt64> scores = new Dictionary<char, UInt64>();
                scores.Add(')', 1);
                scores.Add(']', 2);
                scores.Add('}', 3);
                scores.Add('>', 4);
                List<UInt64> string_scores = new List<UInt64>();
                foreach (var line in input)
                {
                    Stack<char> stack = new Stack<char>();
                    bool illegal = false;
                    foreach (char c in line.Trim())
                    {
                        if (pairs.ContainsKey(c))
                        {
                            // push closing token we expect.
                            stack.Push(pairs[c]);
                        }
                        else if (pairs.ContainsValue(c))
                        {
                            char top = stack.Pop();
                            if (c != top)
                            {
                                // expected top, found c.
                                // first illegal character is c.
                                //score += scores[c];
                                illegal = true;
                                break;
                            }
                        }
                    }
                    if (!illegal)
                    {
                        UInt64 string_score = 0;
                        while (stack.Count > 0)
                        {
                            char c = stack.Pop();
                            string_score = string_score * (UInt64)5 + scores[c];
                        }
                        string_scores.Add(string_score);
                    }
                }
                string_scores.Sort();
                return "" + string_scores[string_scores.Count / 2];
            });

            Console.WriteLine("done");
        }
    }
}
