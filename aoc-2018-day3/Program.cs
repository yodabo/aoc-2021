using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AOC_CSharp;

namespace aoc_2018_day3
{
    internal class Program
    {
        class Rectangle
        {
            public int x = 0;
            public int y = 0;
            public int width = 0;
            public int height = 0;
            public Rectangle(int x, int y, int x2, int y2)
            {
                this.x = x;
                this.y = y;
                this.width = x2 - x;
                this.height = y2 - y;
            }
            public bool In(int x, int y)
            {
                return x > this.x && x <= this.x + width &&
                    y > this.y && y <= this.y + height;
            }

            // intersection of 2:
            public Rectangle Intersection(Rectangle r)
            {
                return new Rectangle(Math.Max(x, r.x), Math.Max(y, r.y), Math.Min(x + width, r.x + r.width), Math.Min(y + height, r.y + r.height));
            }
            public bool Valid()
            {
                return width > 0 && height > 0;
            }
        }
        static void Main(string[] args)
        {
            AOC aoc = new AOC(2018, 3);
            aoc.AddTestCase(1,
                @"#1 @ 1,3: 4x4
#2 @ 3,1: 4x4
#3 @ 5,5: 2x2", 4);
            aoc.Solve(1, i =>
            {
                var lines = i.GetLines();
                List<Rectangle> rects = new List<Rectangle>();
                foreach (var line in lines)
                {
                    var parts = line.Split(' ', 'x', ',').Select(s=>s.Trim(':')).ToArray();
                    int x = int.Parse(parts[2]);
                    int y = int.Parse(parts[3]);
                    int width = int.Parse(parts[4]);
                    int height = int.Parse(parts[5]);
                    Rectangle r = new Rectangle(x, y, x+width, y+height);
                    rects.Add(r);
                    // Geometry problem.. find count of pixels covered by multiple rects.
                    // rasterize and count? easy if it fits in space.
                    // alternative - intersect pairs, 
                }

                // TODO: iterate by index to avoid a vs a, and handling both a vs. b and b vs. a
                foreach (Rectangle r in rects)
                {
                    foreach (Rectangle r2 in rects)
                    {
                        var inter = r.Intersection(r2);
                        if (inter.Valid())
                        {
                            // Consider any subsequent rectangles that may also intersect this.
                            // basically add number of intersections of 2, subtract intersections of 3, add intersections of 4, etc.


                            // alternatively, consider making it a checkerboard/grid
                            // for n rectangles, we'll have 2n lines on each side, and just have to sum up how many of these 4n^2 regions we care about.  max O(n^2) speed.
                        }
                    }
                }

                return null;
            });

            Console.WriteLine("done");
        }
    }
}
