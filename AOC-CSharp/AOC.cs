using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Globalization;
using System.Net;

namespace AOC_CSharp
{
    // Parsing utility - given string can easilly split and iterate numbers.
    public class AOCInput
    {
        public AOCInput(string rawInput)
        {
            this.rawInput = rawInput;
        }

        public List<int> GetNums()
        {
            return rawInput.Split().Select(s => (int.TryParse(s, System.Globalization.NumberStyles.Integer, CultureInfo.InvariantCulture, out int d), d)).Where(n => n.Item1).Select(n => n.Item2).ToList();            
        }

        public List<double> GetDoubles()
        {
            return rawInput.Split().Select(s => (double.TryParse(s, System.Globalization.NumberStyles.Float, CultureInfo.InvariantCulture, out double d), d)).Where(n => n.Item1).Select(n => n.Item2).ToList();
        }

        public List<List<char>> GetCharGrid()
        {
            return GetLines().Select(s => s.Trim()).Where(s => s != "").Select(s => s.ToList()).ToList();
        }

        public List<List<int>> GetIntGrid()
        {
            return GetLines().Select(s => s.Trim()).Where(s => s != "").Select(s => s.Select(c => int.Parse(""+c)).ToList()).ToList();
        }

        public List<String> GetLines()
        {
            //char[] separators = new char[] { '\n', '\r' };
            //return rawInput.Split(separators, StringSplitOptions.RemoveEmptyEntries).ToList();
            return rawInput.Split('\n').Select(s => s.Trim()).ToList();
        }

        // Sometimes there is a prefix (some lines to describe data) then later part is parsed differently...
        // look through old problems to work on this.

        public String GetString() { return rawInput; }

        private string rawInput;

        // get a stream
        // get a stream per line
        // regex parsing
    }

    public class AOC
    {
        private int year;
        private int day;

        public AOC(int year, int day)
        {
            this.year = year;
            this.day = day;

            String cacheFolder = @"c:\users\William.000\source\repos\AOC\data2";
            String cacheFile = String.Format(@"{0}\{1}\{2}.txt", cacheFolder, year, day);
            Directory.CreateDirectory(String.Format(@"{0}\{1}", cacheFolder, year));

            if (File.Exists(cacheFile))
            {
                rawInput = File.ReadAllText(cacheFile);
            } else
            {
                WebRequest request = WebRequest.Create(String.Format("https://adventofcode.com/{0}/day/{1}/input", year, day));

                HttpWebRequest http = request as HttpWebRequest;
                http.CookieContainer = new CookieContainer();
                http.CookieContainer.Add(new Cookie("session", "<my session>", "", "adventofcode.com")); //, "path", "domain"))
                request.Method = "GET";
                //request.

                HttpWebResponse reponse = (HttpWebResponse)(request.GetResponse());
                Console.WriteLine(reponse.StatusDescription);
                StreamReader reader = new StreamReader(reponse.GetResponseStream());
                String input = reader.ReadToEnd();
                Console.WriteLine(input);

                File.WriteAllText(cacheFile, input);
                rawInput = input;
            }

            // Dump first 5 lines of input
            var lines = rawInput.Split('\n').Select(s => s.Trim()).ToArray();
            Console.WriteLine("First lines of input");
            for (int i = 0; i < 5 && i < lines.Length; i++)
            {
                Console.WriteLine(lines[i]);
            }
        }

        private bool SubmitAnswer(int part, string answer)
        {
            if (answer == null || answer == "") return false;

            // TODO: read answers in constructor.
            // TODO: store in local to avoid disk flush issues.
            String cacheFolder = @"c:\users\<my username>\source\repos\AOC\data";
            String cacheFile = String.Format(@"{0}\{1}\{2}-answers{3}.txt", cacheFolder, year, day, part);
            if (!File.Exists(cacheFile))
            {
                File.Create(cacheFile);
            }
            else
            {
                var existing = File.ReadAllLines(cacheFile).ToList();
                if (existing.Contains("0" + answer))
                {
                    Console.WriteLine("wrong answer for {0}: {1}", part, answer);
                    return false;
                }
                else if (existing.Contains("*" + answer))
                {
                    Console.WriteLine("already correct answer for {0}: {1}", part, answer);
                    // this was correct.
                    return true;
                }
                //foreach (var s in existing) {
                //    if (s[0] == '*') return false;
                    Console.WriteLine("wrong answer for {0}: {1}", part, answer);
                //}
            }

            bool correct = false;
            // "https://adventofcode.com/{year}/day/{day}"
            // post to 2/answer
            // hidden level=1
            // answer=foo
            // example parses input to look for "That's the right answer" / "Did you already solve it" / "That's not the right answer" / "You gave an answer too recently" ("You have <time> left to wait")

            Console.WriteLine("submitting...(but not really)");
            Console.WriteLine("answer for {0}: {1}", part, answer);
            correct = true;

            // append our answer and whether it was right or wrong:
            List<String> lines = new List<string>();
            lines.Add("" + (correct ? '*' : '0') + answer);
            while (true)
            {
                try
                {
                    File.AppendAllLines(cacheFile, lines);
                    break; // succeeded to write.
                }
                catch(Exception ex)
                {
                    // Failed to write.  Try again.
                    System.Threading.Thread.Sleep(100);
                }
            }

            return correct;
        }

        public AOCInput GetInput()
        {
            return new AOCInput(rawInput);
        }
        private bool testing = false;
        public bool Testing()
        {
            return testing;
        }

        public void AddTestCase(int part, String input, String output)
        {
            if (part == 1)
            {
                part1_test_cases.Add(input, output);
            } else
            {
                part2_test_cases.Add(input, output);
            }
        }
        public void AddTestCase(int part, String input, int output)
        {
            AddTestCase(part, input, "" + output);
        }

        public void AddTestCase(int part, int input, string output)
        {
            AddTestCase(part, "" + input, output);
        }

        public void AddTestCase(int part, int input, int output)
        {
            AddTestCase(part, "" + input, "" + output);
        }

        public bool Solve(int part, Func<AOCInput, String> solver)
        {
            var cases = part == 1 ? part1_test_cases : part2_test_cases;

            bool has_failed_submit = false;
            while (true)
            {
                while (true)
                {
                    bool passed_all = true;
                    testing = true;
                    foreach (var kv in cases)
                    {
                        AOCInput input = new AOCInput(kv.Key);
                        while (true)
                        {
                            String result = solver(input);
                            if (result == kv.Value)
                            {
                                // We successfully passed this case.
                                break;
                            }
                            else
                            {
                                passed_all = false;
                                System.Threading.Thread.Sleep(200);
                                if (result != "" && result != null)
                                {
                                    Console.WriteLine();
                                    Console.WriteLine("Retrying case {0}", kv.Key);
                                }
                            }
                        }
                    }
                    testing = false;
                    if (passed_all) break;
                }

                // We passed the test cases on the first try. Now try the real problem.
                var watch = new System.Diagnostics.Stopwatch();
                
                watch.Start();
                String output = solver(GetInput());
                watch.Stop();
                Console.WriteLine("Executed solution{0} in {1} ms", part, watch.ElapsedMilliseconds);
                if (output != null)
                {
                    bool should_submit = true;
                    if (has_failed_submit)
                    {
                        // Check first with user.
                        Console.WriteLine("Should we submit answer: {0}?", output);
                        should_submit = Console.ReadLine() == "y";
                    }
                    if (should_submit)
                    {
                        if (SubmitAnswer(part, output))
                            break;
                        else
                            has_failed_submit = false;
                    }
                }
            }

            // Run test cases for input
            // if they are successful, run solver on real input, and submit the answer (cache submitted answers, don't submit same one multiple times, confirm before next submit)
            // 
            // otherwise we run them again in a loop (so we can easily add debug breakpoints)

            return false; // returns true when webserver accepts our answer.  TODO: cache answers submitted on disk so we don't re-try bad answers again.
        }

        public bool Solve(int part, Func<AOCInput, int> solver)
        {
            return Solve(part, i => "" + solver(i));
        }

        private String rawInput = "";
        Dictionary<String, String> part1_test_cases = new Dictionary<string, string>();
        Dictionary<String, String> part2_test_cases = new Dictionary<string, string>();
    }
}
