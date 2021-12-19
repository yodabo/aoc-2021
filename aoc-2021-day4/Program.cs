using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AOC_CSharp;

namespace aoc_2021_day4
{
    internal class Program
    {
        class Board
        {
            public Dictionary<int, Tuple<int, int>> squares;
            public List<List<bool>> covered;

            public Board()
            {
                squares = new Dictionary<int, Tuple<int, int>>(); // add later.

                covered = new List<List<bool>>();
                for (int i = 0; i < 5; ++i)
                {
                    covered.Add(new List<bool>());
                    for (int j = 0; j < 5; ++j)
                    {
                        covered[i].Add(false);
                    }
                }
            }

            public bool Done()
            {
                for (int i = 0; i < 5; ++i)
                {
                    // i is a row or column.  now check if all are true in it.

                    bool all_true_across = true;
                    bool all_true_down = true;

                    for (int j = 0; j < 5; j++)
                    {
                        all_true_across &= covered[i][j];
                        all_true_down &= covered[j][i];
                    }

                    if (all_true_down || all_true_across) return true;
                }
                return false;
            }

            public int Score(int last_num)
            {
                int sum_unmarked = 0;
                foreach (var s in squares)
                {
                    if (!covered[s.Value.Item1][s.Value.Item2])
                    {
                        sum_unmarked += s.Key;
                    }
                }
                return last_num * sum_unmarked;
            }

            public void Mark(int num)
            {
                if (squares.ContainsKey(num))
                {
                    covered[squares[num].Item1][squares[num].Item2] = true;
                }
            }
        };

        static void Main(string[] args)
        {
            AOC aoc = new AOC(2021, 4);

            aoc.AddTestCase(1, @"7,4,9,5,11,17,23,2,0,14,21,24,10,16,13,6,15,25,12,22,18,20,8,19,3,26,1

22 13 17 11  0
 8  2 23  4 24
21  9 14 16  7
 6 10  3 18  5
 1 12 20 15 19

 3 15  0  2 22
 9 18 13 17  5
19  8  7 25 23
20 11 10 24  4
14 21 16 12  6

14 21 17 24  4
10 16 15  9 19
18  8 23 26 20
22 11 13  6  5
 2  0 12  3  7", 4512);
            aoc.Solve(1, i =>
            {
                var lines = i.GetLines();
                // first line - numbers
                List<int> nums = lines[0].Trim().Split(',').Select(s => int.Parse(s)).ToList();

                // other lines:
                List<Board> boards = new List<Board>();
                List<String> boardnums = lines.Skip(1).Where(s => s.Trim() != "").ToList();
                for (int j = 0; j < boardnums.Count; j += 5)
                {
                    var board = new Board();
                    for (int r = 0; r < 5; ++r)
                    {
                        var line_nums = boardnums[r+j].Trim().Split().Where(s=>s.Trim() != "").Select(s=>int.Parse(s)).ToList();
                        for(int c = 0; c < 5; ++c)
                        {
                            board.squares.Add(line_nums[c], new Tuple<int, int>(r, c));
                        }
                    }
                    boards.Add(board);
                }

                foreach (int n in nums)
                {
                    foreach (Board b in boards)
                    {
                        b.Mark(n);
                        if (b.Done())
                        {
                            return "" + b.Score(n);
                        }
                    }
                }

                return null;
            });

            aoc.AddTestCase(2, @"7,4,9,5,11,17,23,2,0,14,21,24,10,16,13,6,15,25,12,22,18,20,8,19,3,26,1

22 13 17 11  0
 8  2 23  4 24
21  9 14 16  7
 6 10  3 18  5
 1 12 20 15 19

 3 15  0  2 22
 9 18 13 17  5
19  8  7 25 23
20 11 10 24  4
14 21 16 12  6

14 21 17 24  4
10 16 15  9 19
18  8 23 26 20
22 11 13  6  5
 2  0 12  3  7", 1924);
            aoc.Solve(2, i =>
            {
                var lines = i.GetLines();
                // first line - numbers
                List<int> nums = lines[0].Trim().Split(',').Select(s => int.Parse(s)).ToList();

                // other lines:
                List<Board> boards = new List<Board>();
                List<String> boardnums = lines.Skip(1).Where(s => s.Trim() != "").ToList();
                for (int j = 0; j < boardnums.Count; j += 5)
                {
                    var board = new Board();
                    for (int r = 0; r < 5; ++r)
                    {
                        var line_nums = boardnums[r + j].Trim().Split().Where(s => s.Trim() != "").Select(s => int.Parse(s)).ToList();
                        for (int c = 0; c < 5; ++c)
                        {
                            board.squares.Add(line_nums[c], new Tuple<int, int>(r, c));
                        }
                    }
                    boards.Add(board);
                }

                foreach (int n in nums)
                {
                    foreach (Board b in boards)
                    {
                        b.Mark(n);
                    }
                    if (boards.Count > 1)
                        boards = boards.Where(b => !b.Done()).ToList();

                    if (boards.Count == 1 && boards[0].Done())
                    {
                        return "" + boards[0].Score(n);
                    }
                }
                return null;
            });
            Console.WriteLine("done");
        }
    }
}
