using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AOC_CSharp;

namespace aoc_2021_day11
{
    internal class Program
    {
        static void Main(string[] args)
        {
            AOC aoc = new AOC(2021, 11);

//            aoc.AddTestCase(1, @"5483143223
//2745854711
//5264556173
//6141336146
//6357385478
//4167524645
//2176841721
//6882881134
//4846848554
//5283751526", 1656);
//            aoc.Solve(1, ii =>
//            {
//                var state = ii.GetIntGrid();
//                //var nextState = ii.GetIntGrid(); // same size...

//                int num_flashes = 0;

//                for (int i = 0; i < 100; ++i)
//                {
//                    Console.WriteLine("State {0}: ", i);
//                    for (int r = 0; r < state.Count(); ++r)
//                    {
//                        for (int c = 0; c < state[r].Count(); ++c)
//                        {
//                            Console.Write(state[r][c]);
//                        }
//                        Console.WriteLine();
//                    }
//                    Console.WriteLine();

//                    // iterate elements to increment
//                    for (int r = 0; r < state.Count(); ++r)
//                    {
//                        for (int c = 0; c < state[r].Count(); ++c)
//                        {
//                            state[r][c] = state[r][c]+1;
//                        }
//                    }

//                    bool changes = true; // could do more of a flood fill.
//                    while (changes)
//                    {
//                        changes = false;
//                        for (int r = 0; r < state.Count(); ++r)
//                        {
//                            for (int c = 0; c < state[r].Count(); ++c)
//                            {
//                                if (state[r][c] > 9)
//                                {
//                                    state[r][c] = int.MinValue;
//                                    changes = true;
//                                    num_flashes++;
//                                    // increment neighbors. Exception to avoid checking bounds (could add apron instead.)
//                                    for (int dr = -1; dr <= 1; ++dr)
//                                    {
//                                        for (int dc = -1; dc <= 1; ++dc)
//                                        {
//                                            try { state[r + dr][c + dc]++; } catch { }
//                                        }
//                                    }
//                                }
//                            }
//                        }
//                    }

//                    // move negative to 0
//                    for (int r = 0; r < state.Count(); ++r)
//                    {
//                        for (int c = 0; c < state[r].Count(); ++c)
//                        {
//                            if (state[r][c] < 0) state[r][c] = 0;
//                        }
//                    }
//                }
//                return num_flashes;
//            });

            aoc.AddTestCase(2, @"5483143223
2745854711
5264556173
6141336146
6357385478
4167524645
2176841721
6882881134
4846848554
5283751526", 195);
            aoc.Solve(2, ii =>
            {
                var state = ii.GetIntGrid();
                //var nextState = ii.GetIntGrid(); // same size...

                for (int i = 0; ; ++i)
                {
                    Console.WriteLine("State {0}: ", i);
                    for (int r = 0; r < state.Count(); ++r)
                    {
                        for (int c = 0; c < state[r].Count(); ++c)
                        {
                            Console.Write(state[r][c]);
                        }
                        Console.WriteLine();
                    }
                    Console.WriteLine();

                    // iterate elements to increment
                    for (int r = 0; r < state.Count(); ++r)
                    {
                        for (int c = 0; c < state[r].Count(); ++c)
                        {
                            state[r][c] = state[r][c] + 1;
                        }
                    }

                    bool changes = true; // could do more of a flood fill.
                    while (changes)
                    {
                        changes = false;
                        for (int r = 0; r < state.Count(); ++r)
                        {
                            for (int c = 0; c < state[r].Count(); ++c)
                            {
                                if (state[r][c] > 9)
                                {
                                    state[r][c] = int.MinValue;
                                    changes = true;
                                    // increment neighbors. Exception to avoid checking bounds (could add apron instead.)
                                    for (int dr = -1; dr <= 1; ++dr)
                                    {
                                        for (int dc = -1; dc <= 1; ++dc)
                                        {
                                            try { state[r + dr][c + dc]++; } catch { }
                                        }
                                    }
                                }
                            }
                        }
                    }

                    // move negative to 0
                    bool all_flashed = true;
                    for (int r = 0; r < state.Count(); ++r)
                    {
                        for (int c = 0; c < state[r].Count(); ++c)
                        {
                            if (state[r][c] < 0) state[r][c] = 0;
                            else all_flashed = false;
                        }
                    }
                    if (all_flashed)
                        return "" + (i+1);
                }
                return null;
            });

            Console.WriteLine("done");
        }
    }
}
