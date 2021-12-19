using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AOC_CSharp;

namespace aoc_2021_day17
{
    internal class Program
    {
        static int Hits(int dx, int dy, int tx, int tx2, int ty, int ty2)
        {
            int x = 0;
            int y = 0;
            int maxy = int.MinValue;
            while ((y >= ty2 || dy > 0) && dx <= tx2)
            {
                x += dx;
                y += dy;
                if (dx > 0) dx--;
                else if (dx < 0) dx++;
                dy--;
                
                if (y > maxy) maxy = y;

                if (y >= ty && y <= ty2 && x >= tx && x <= tx2)
                {
                    return maxy;
                }
            }
            return int.MinValue;
        }

        static bool Hits2(int dx, int dy, int tx, int tx2, int ty, int ty2)
        {
            // observation 1:
            // we can enter region and leave region, but won't reenter
            // opservation 2:
            // x_i = sum(dx_i)
            // dx_i = dx_0 + sum(-1), so max(dx_0 - i, 0)
            // x_i = dx_0 * i - i*(i-1)/2... x_0 = 0.  x_1 = dx, x_2=dx*2-1, x_3=dx*3-3, etc... up to terminal
            // after dx steps, velocity is 0.
            // if dx = 2, step(dx=2), step(dx=1), and any more steps leave velocity of 0.
            // so x_dx = dx_0 * dx_0 - dx_0*(dx_0-1)/2 = 2dx_0*...

            // anyway, solve for when we enter (by dx), solve for when we leave (by dx)
            // solve for when we enter/leave by dy - same, but no terminal state.
            // finally, if ranges intersect at all we are good.
             // note that we need an actual value in though - not just hypothetical


            int x = 0;
            int y = 0;
            int maxy = int.MinValue;
            while ((y >= ty || dy > 0) && x <= tx2)
            {
                x += dx;
                y += dy;
                if (dx > 0) dx--;
                else if (dx < 0) dx++;
                dy--;

                if (y > maxy) maxy = y;

                if (y >= ty && y <= ty2 && x >= tx && x <= tx2)
                {
                    return true;
                }
            }
            return false;
        }

        static void Main(string[] args)
        {
            AOC aoc = new AOC(2021, 17);
            
            //aoc.AddTestCase(1, @"target area: x=20..30, y=-10..-5", 45);

            //aoc.Solve(1, ii =>
            //{
            //    var line = ii.GetString();
            //    char[] seps = new char[] { '=', ',','.' };
            //    var parts = line.Split(seps).Select(s => s.Trim()).ToList();
            //    int minx = int.Parse(parts[1]);
            //    int maxx = int.Parse(parts[3]);
            //    int miny = int.Parse(parts[5]);
            //    int maxy = int.Parse(parts[7]);

            //    int best = 0;

            //    for (int dx = 0; dx <= maxx; ++dx)
            //    {
            //        // for this dx, what range of dy hits?
            //        for (int dy = 0; dy <= 10000; ++dy)
            //        {
            //            int cur = Hits(dx, dy, minx, maxx, miny, maxy);
            //            if (cur > best) best = cur;
            //        }
            //    }

            //    Console.WriteLine("{0}, {1}, {2}, {3}", minx, maxx, miny, maxy);
            //    return best;
            //});

            aoc.AddTestCase(2, @"target area: x=20..30, y=-10..-5", 112);

            aoc.Solve(2, ii =>
            {
                var line = ii.GetString();
                char[] seps = new char[] { '=', ',', '.' };
                var parts = line.Split(seps).Select(s => s.Trim()).ToList();
                int minx = int.Parse(parts[1]);
                int maxx = int.Parse(parts[3]);
                int miny = int.Parse(parts[5]);
                int maxy = int.Parse(parts[7]);

                int best = 0;

                for (int dx = 0; dx <= maxx; ++dx)
                {
                    // for this dx, what range of dy hits?
                    for (int dy = -100; dy <= 10000; ++dy)
                    {
                        if (Hits2(dx, dy, minx, maxx, miny, maxy))
                            best++;
                    }
                }

                Console.WriteLine("{0}, {1}, {2}, {3}", minx, maxx, miny, maxy);
                return best;
            });

            Console.WriteLine("done");
            Console.ReadKey();
        }
    }
}
