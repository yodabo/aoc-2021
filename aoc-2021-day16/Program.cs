using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AOC_CSharp;

namespace aoc_2021_day15
{
    internal class Program
    {
        static IEnumerable<int> ParseBits(String input)
        {
            foreach(char c in input)
            {
                int next = 0;
                if (c >= '0' && c <= '9')
                {
                    next = c - '0';
                }
                else if (c >= 'a' && c <= 'f')
                {
                    next = c - 'a' + 10;
                }
                else if (c >= 'A' && c <= 'F')
                {
                    next = c - 'A' + 10;
                }
                else continue;

                if (next >= 8) yield return 1;
                else yield return 0;
                next = next % 8;
                if (next >= 4) yield return 1;
                else yield return 0;
                next = next % 4;
                if (next >= 2) yield return 1;
                else yield return 0;
                next = next % 2;
                if (next >= 1) yield return 1;
                else yield return 0;
            }
        }

        class Packet
        {
            public int version;
            public int type;
            public UInt64 literal = 0;
            bool unfinished = false; // is this a real packet?
            public List<Packet> children = new List<Packet>();

            public UInt64 SumVersion()
            {
                UInt64 ret = (UInt64)version;
                foreach (var c in children)
                {
                    ret += c.SumVersion();
                }
                return ret;
            }

            public UInt64 Eval()
            {
                if (type == 4)
                {
                    return literal;
                } else if (type == 0)
                {
                    UInt64 sum = 0;
                    foreach(var c in children)
                    {
                        sum += c.Eval();
                    }
                    return sum;
                }
                else if (type == 1)
                {
                    UInt64 prod = 1;
                    foreach (var c in children)
                    {
                        prod *= c.Eval();
                    }
                    return prod; // 
                } else if (type == 2) // min
                {
                    return children.Select(c => c.Eval()).Min();
                }
                else if (type == 3) // max
                {
                    return children.Select(c => c.Eval()).Max();
                }
                else if (type == 5) // greater than
                {
                    return children[0].Eval() > children[1].Eval() ? (UInt64)1 : (UInt64)0;
                }
                else if (type == 6) // less than
                {
                    return children[0].Eval() < children[1].Eval() ? (UInt64)1 : (UInt64)0;
                }
                else if (type == 7) // equal
                {
                    return children[0].Eval() == children[1].Eval() ? (UInt64)1 : (UInt64)0;
                }
                return UInt64.MaxValue;
            }
        }

        static int ReadBits(List<int> bits, ref int index, int num_bits)
        {
            int ret = 0;
            for (int i = 0; i < num_bits; ++i)
            {
                ret = ret * 2 + ((index >= bits.Count) ? 0 :  bits[index]);
                index++;
            }
            return ret;
        }

        static Packet ParsePacket(List<int> bits, ref int index)
        {
            Packet packet = new Packet();

            // read 3 for version
            packet.version = ReadBits(bits, ref index, 3);
            packet.type = ReadBits(bits, ref index, 3);
            Console.WriteLine("version {0}  type {1}", packet.version, packet.type);
            //if (packet.type == 0 && packet.version == 0)
            //{
            //    return packet;
            // }
            if (packet.type == 4)
            {
                // Literal
                // peal off 1 bit, then 4 bits.
                UInt64 literal = 0;
                do
                {
                    int not_last = ReadBits(bits, ref index, 1);
                    literal = literal * 16 + (UInt64)ReadBits(bits, ref index, 4);
                    packet.literal = literal;
                    if (not_last == 0) break;
                }
                while (true);
            } else
            {
                // Operator
                var sub_type = ReadBits(bits, ref index, 1);
                if (sub_type == 0)
                {
                    int total_children_bits_length = ReadBits(bits, ref index, 15);
                    int next_index = index + total_children_bits_length;
                    while (index < next_index)
                    {
                        packet.children.Add(ParsePacket(bits, ref index));
                    }
                }
                if (sub_type == 1)
                {
                    int total_children = ReadBits(bits, ref index, 11);
                    for (int i = 0; i < total_children; ++i)
                    {
                        packet.children.Add(ParsePacket(bits, ref index));
                    }
                }
            }

            return packet;
        }

        static List<Packet> ParsePackets(List<int> bits)
        {
            List<Packet> ret = new List<Packet>();
            int index = 0;
            while (index < bits.Count) // +5 to ignore trailing 0's
            {
                ret.Add(ParsePacket(bits, ref index));
            }
            return ret;
        }

        static void Main(string[] args)
        {
            AOC aoc = new AOC(2021, 16);
            //aoc.AddTestCase(1, @"38006F45291200", 0);

            aoc.AddTestCase(1, @"8A004A801A8002F478", 16);
            aoc.AddTestCase(1, @"620080001611562C8802118E34", 12);
            aoc.AddTestCase(1, @"C0015000016115A2E0802F182340", 23);
            aoc.AddTestCase(1, @"A0016C880162017C3686B18A3D4780", 31);

            aoc.Solve(1, ii =>
            {
                String input = ii.GetString();
                Console.WriteLine(input);

                var bits = ParseBits(input);
                foreach (int b in bits)
                {
                    Console.Write(b);
                }
                Console.WriteLine();
                var packets = ParsePackets(bits.ToList());

                // add up all version numbers.
                UInt64 sum = 0;

                foreach (Packet p in packets)
                {
                    sum += p.SumVersion();
                }



                // hex -> binary
                // first 3 bits, version number
                // next 3 bits - packet type id (4=literal)
                //  literal: numer padded with leading zeros until length is multiple of 4 bits., then broken into multiple of 4 bits, each prefixed by a 1 except last which is prefixed by 0
                // operator (any other packet type):
                //  contains subpackets...
                //  1-bit: length type id... 0: following 15 bits are number representing total length of bits of subpackets
                //                           1: number of subpackets (11 bits)
                // 
                return "" + sum;
            });

            aoc.AddTestCase(2, @"C200B40A82", 3);
            aoc.AddTestCase(2, @"04005AC33890", 54);
            aoc.AddTestCase(2, @"880086C3E88112", 7);
            aoc.AddTestCase(2, @"CE00C43D881120", 9);
            aoc.AddTestCase(2, @"D8005AC2A8F0", 1);
            aoc.AddTestCase(2, @"F600BC2D8F", 0);
            aoc.AddTestCase(2, @"9C005AC2F8F0", 0);
            aoc.AddTestCase(2, @"9C0141080250320F1802104A08", 1);


            aoc.Solve(2, ii =>
            {
                String input = ii.GetString();
                Console.WriteLine(input);

                var bits = ParseBits(input);
                foreach (int b in bits)
                {
                    Console.Write(b);
                }
                Console.WriteLine();
                var packets = ParsePackets(bits.ToList());

                return "" + packets[0].Eval();
            });

            Console.WriteLine("done");
            Console.ReadKey();
        }
    }
}
