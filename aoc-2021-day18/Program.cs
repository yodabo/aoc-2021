using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AOC_CSharp;

namespace aoc_2021_day18
{
    class Number {
        public Number first = null;
        public Number second = null;
        public Number parent = null;

        int value = 0;
        bool is_int = false;

        void Reduce()
        {
            // find leftmost st IsInside4 is true and !is_int if we can.
            while (true)
            {
                if (TryExplode()) { continue; }
                if (TrySplit()) { continue; }
                break;
            }
        }

        public Number Add(Number other)
        {
            Number ret = new Number();
            ret.first = this;
            this.parent = ret;
            ret.second = other;
            other.parent = ret;
            ret.is_int = false;
            ret.Reduce();
            return ret;
        }

        bool CanExplode()
        {
            return (!is_int && IsInside4() && first.is_int && second.is_int);
        }

        bool TryExplode()
        {
            if (CanExplode())
            {
                DoExplode();
                return true;
            }
            else if (!is_int)
            {
                if (first.TryExplode())
                {
                    return true;
                }
                else if (second.TryExplode())
                {
                    return true;
                }
            }
            return false;
        }

        void DoExplode()
        {
            // is_int should be false
            if (is_int) throw new Exception();
            // we should be inside 4.
            if (!IsInside4()) throw new Exception();
            if (!first.is_int) throw new Exception();
            if (!second.is_int) throw new Exception();

            var left = Previous();
            var right = Next();
            if (left != null)
            {
                left.value += first.value;
            }

            if (right != null)
            {
                right.value += second.value;
            }

            left = null;
            right = null;
            is_int = true;
            value = 0;

            // We may need to split now?
        }

        bool IsInside4()
        {
            var c = parent;
            for (int i = 0; i < 4; ++i)
            {
                if (c == null || c.is_int) return false;
                c = c.parent;
            }
            return true;
        }

        bool TrySplit()
        {
            if (CanSplit())
            {
                DoSplit();
                return true;
            }
            if (!is_int)
            {
                if (first.TrySplit()) return true;
                if (second.TrySplit()) return true;
            }
            return false;
        }

        bool CanSplit()
        {
            return is_int && value >= 10;
        }

        void DoSplit()
        {
            is_int = false;

            first = new Number();
            first.is_int = true;
            first.value = value / 2;
            first.parent = this;

            second = new Number();
            second.is_int = true;
            second.value = value - value / 2;
            second.parent = this;

            // Should we explode now?
        }

        Number FirstNumber()
        {
            // leftmost node.
            if (is_int) return this;
            return first.FirstNumber();
        }

        Number LastNumber()
        {
            if (is_int) return this;
            return second.LastNumber();
        }

        Number Next()
        {
            // next regular number
            if (parent != null && parent.first == this)
            {
                // leftmost node under parent.second.
                return parent.second.FirstNumber();
            }
            else if (parent != null)
            {
                // go up
                return parent.Next();
            }
            return null;
        }
        Number Previous()
        {
            // previous regular number
            if (parent != null && parent.second == this)
            {
                return parent.first.LastNumber();
            } else if (parent != null)
            {
                // go up.
                return parent.Previous();
            }
            return null;
        }

        public static Number Parse(String s)
        {
            int index = 0;
            return Parse(s, ref index);
        }

        static Number Parse(String s, ref int index)
        {
            Number ret = new Number();

            if (s[index] == '[')
            {
                index++; //[

                ret.is_int = false;
                ret.first = Parse(s, ref index);
                ret.first.parent = ret;
                index++; //,
                ret.second = Parse(s, ref index);
                ret.second.parent = ret;
                index++;//]
                return ret;
            }
            else
            {
                // count how many num chars we have?
                int num = 0;
                while(s[num + index] >= '0' && s[num + index] <= '9') { num++; }

                ret.value = int.Parse(s.Substring(index, num));
                index+= num;
                ret.is_int = true;
                return ret;
            }
        }

        public UInt64 Magnitude()
        {
            if (is_int)
            {
                return (UInt64)value;
            }

            return first.Magnitude() * 3 + second.Magnitude() * 2;
        }

        public void Dump()
        {
            if (is_int)
            {
                Console.Write(value);
            } else
            {
                Console.Write('[');
                first.Dump();
                Console.Write(',');
                second.Dump();
                Console.Write(']');
            }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            AOC aoc = new AOC(2021, 18);

            // TODO: it would be nice to test individual functions instead of just the test cases...
            
            aoc.AddTestCase(1, @"[[[0,[5,8]],[[1,7],[9,6]]],[[4,[1,2]],[[1,4],2]]]
[[[5,[2,8]],4],[5,[[9,9],0]]]
[6,[[[6,2],[5,6]],[[7,6],[4,7]]]]
[[[6,[0,7]],[0,9]],[4,[9,[9,0]]]]
[[[7,[6,4]],[3,[1,3]]],[[[5,5],1],9]]
[[6,[[7,3],[3,2]]],[[[3,8],[5,7]],4]]
[[[[5,4],[7,7]],8],[[8,3],8]]
[[9,3],[[9,9],[6,[4,9]]]]
[[2,[[7,7],7]],[[5,8],[[9,3],[0,2]]]]
[[[[5,2],5],[8,[3,7]]],[[5,[7,5]],[4,4]]]", 4140);

            aoc.Solve(1, ii =>
            {
                Number sum = null;
                foreach (var line in ii.GetLines().Where(s=>s.Trim() != ""))
                {
                    // Parse this line..
                    var num = Number.Parse(line);

                    Console.WriteLine("Next number:");
                    num.Dump();
                    Console.WriteLine();


                    if (sum == null) sum = num;
                    else sum = sum.Add(num);

                    Console.WriteLine("current sum:");
                    sum.Dump();
                    Console.WriteLine();
                }

                return "" + sum.Magnitude();
            });

            aoc.Solve(2, ii =>
            {
                UInt64 max = UInt64.MinValue;

                foreach (var line1 in ii.GetLines().Where(s => s.Trim() != ""))
                {
                    foreach (var line2 in ii.GetLines().Where(s => s.Trim() != ""))
                    {
                        if (line1 == line2) continue;

                        var num1 = Number.Parse(line1);
                        var num2 = Number.Parse(line2);

                        var total = num1.Add(num2);
                        var mag = total.Magnitude(); // will we need UINT64?
                        max = Math.Max(max, mag);
                    }
                }

                return "" + max;
            });

            Console.WriteLine("done");
            Console.ReadKey();
        }
    }
}
