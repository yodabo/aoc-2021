using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AOC_CSharp;

namespace aoc_2021_day19
{
    internal class Program
    {
        struct Pos
        {
            public int x;
            public int y;
            public int z;
        }

        struct Transform
        {
            public Pos T(Pos initial)
            {
                Pos ret = new Pos();
                ret.x = (x == 0) ? initial.x :
                    (x == 1) ? initial.y :
                    initial.z;
                ret.y = (y == 0) ? initial.x :
                    (y == 1) ? initial.y :
                    initial.z;
                ret.z = (z == 0) ? initial.x :
                    (z == 1) ? initial.y :
                    initial.z;

                if (sx == 1) ret.x *= -1;
                if (sy == 1) ret.y *= -1;
                if (sz == 1) ret.z *= -1;
                return ret;
            }

            public int x;
            public int y;
            public int z;
            public int sx;
            public int sy;
            public int sz;
        };

        static IEnumerable<Transform> EnumTransforms()
        {
            for (int i = 0; i < 216; ++i)
            {
                Transform t = new Transform();
                t.x = i % 3;
                t.y = (i / 3) % 3;
                t.z = (i / 9) % 3;
                t.sx = (i / 27) % 2;
                t.sy = (i / 54) % 2;
                t.sz = (i / 108) % 2;
                if (t.x != t.y && t.y != t.z && t.z != t.x)
                {
                    yield return t;
                }
            }
        }

        static void Main(string[] args)
        {
            AOC aoc = new AOC(2021, 19);

            // TODO: it would be nice to test individual functions instead of just the test cases...

            aoc.AddTestCase(1, @"--- scanner 0 ---
404,-588,-901
528,-643,409
-838,591,734
390,-675,-793
-537,-823,-458
-485,-357,347
-345,-311,381
-661,-816,-575
-876,649,763
-618,-824,-621
553,345,-567
474,580,667
-447,-329,318
-584,868,-557
544,-627,-890
564,392,-477
455,729,728
-892,524,684
-689,845,-530
423,-701,434
7,-33,-71
630,319,-379
443,580,662
-789,900,-551
459,-707,401

--- scanner 1 ---
686,422,578
605,423,415
515,917,-361
-336,658,858
95,138,22
-476,619,847
-340,-569,-846
567,-361,727
-460,603,-452
669,-402,600
729,430,532
-500,-761,534
-322,571,750
-466,-666,-811
-429,-592,574
-355,545,-477
703,-491,-529
-328,-685,520
413,935,-424
-391,539,-444
586,-435,557
-364,-763,-893
807,-499,-711
755,-354,-619
553,889,-390

--- scanner 2 ---
649,640,665
682,-795,504
-784,533,-524
-644,584,-595
-588,-843,648
-30,6,44
-674,560,763
500,723,-460
609,671,-379
-555,-800,653
-675,-892,-343
697,-426,-610
578,704,681
493,664,-388
-671,-858,530
-667,343,800
571,-461,-707
-138,-166,112
-889,563,-600
646,-828,498
640,759,510
-630,509,768
-681,-892,-333
673,-379,-804
-742,-814,-386
577,-820,562

--- scanner 3 ---
-589,542,597
605,-692,669
-500,565,-823
-660,373,557
-458,-679,-417
-488,449,543
-626,468,-788
338,-750,-386
528,-832,-391
562,-778,733
-938,-730,414
543,643,-506
-524,371,-870
407,773,750
-104,29,83
378,-903,-323
-778,-728,485
426,699,580
-438,-605,-362
-469,-447,-387
509,732,623
647,635,-688
-868,-804,481
614,-800,639
595,780,-596

--- scanner 4 ---
727,592,562
-293,-554,779
441,611,-461
-714,465,-776
-743,427,-804
-660,-479,-426
832,-632,460
927,-485,-438
408,393,-506
466,436,-512
110,16,151
-258,-428,682
-393,719,612
-211,-452,876
808,-476,-593
-575,615,604
-485,667,467
-680,325,-822
-627,-443,-432
872,-547,-609
833,512,582
807,604,487
839,-516,451
891,-625,532
-652,-548,-490
30,-46,-14", 79);

            //aoc.Solve(1, ii =>
            //{
            //    // scanners and beacons.
            //    // scanner can find all beacons at most 1000 away in x/y/z dir.

            //    // positions reported are relative
            //    // find pairs of scanners with overlapping detections of at least 12 beacons.

            //    // don't know orientations. x/y/z can be ordered and negated.

            //    List<HashSet<Pos>> scanners = new List<HashSet<Pos>>();
            //    HashSet<Pos> current_scanner = null;

            //    foreach (var line in ii.GetLines())
            //    {
            //        if (line.StartsWith("---"))
            //        {
            //            if (current_scanner != null)
            //            {
            //                scanners.Add(current_scanner);
            //            }

            //            current_scanner = new HashSet<Pos>();
            //        } else if (line.Contains(','))
            //        {
            //            var parts = line.Split(',');
            //            Pos pos = new Pos();
            //            pos.x = int.Parse(parts[0]);
            //            pos.y = int.Parse(parts[1]);
            //            pos.z = int.Parse(parts[2]);
            //            current_scanner.Add(pos);
            //        }
            //    }
            //    scanners.Add(current_scanner);


            //    // Find pairs where they share 12.
            //    foreach (var s in scanners)
            //    {
            //        foreach (var s1 in scanners)
            //        {
            //            if (!s1.Any()) continue;

            //            foreach (var s2 in scanners)
            //            {
            //                if (!s2.Any()) continue;
            //                if (s1 == s2) continue;

            //                bool merged = false;
            //                // try all transforms...
            //                foreach (Transform t in EnumTransforms())
            //                {
            //                    foreach (var b1 in s1)
            //                    {
            //                        foreach (var b2 in s2)
            //                        {
            //                            var tb2 = t.T(b2);
            //                            int count = 0;
            //                            int dx = b1.x - tb2.x;
            //                            int dy = b1.y - tb2.y;
            //                            int dz = b1.z - tb2.z;
            //                            // if b1 and b2 are the same, how many do we have the same?
            //                            foreach (var b3 in s2)
            //                            {
            //                                var tb3 = t.T(b3);
            //                                tb3.x += dx;
            //                                tb3.y += dy;
            //                                tb3.z += dz;
            //                                if (s1.Contains(tb3))
            //                                {
            //                                    count++;
            //                                }
            //                            }

            //                            if (count >= 12)
            //                            {
            //                                // merge these, and remove s2, add everything to s1.
            //                                foreach (var b in s2)
            //                                {
            //                                    var tb = t.T(b);
            //                                    tb.x += dx;
            //                                    tb.y += dy;
            //                                    tb.z += dz;
            //                                    if (!s1.Contains(tb))
            //                                    {
            //                                        s1.Add(tb);
            //                                    }
            //                                }
            //                                s2.Clear();

            //                                merged = true;
            //                                break;
            //                            }
            //                            if (merged)
            //                            {
            //                                break;
            //                            }
            //                        }
            //                        if (merged)
            //                        {
            //                            break;
            //                        }
            //                    }
            //                    if (merged)
            //                    {
            //                        break;
            //                    }
            //                }
            //                if (merged)
            //                {
            //                    break;
            //                }
            //            }
            //        }
            //    }

            //    int sum = 0;
            //    foreach (var s in scanners)
            //    {
            //        sum += s.Count;
            //    }


            //    return sum;
            //});

            aoc.Solve(2, ii =>
            {
                // scanners and beacons.
                // scanner can find all beacons at most 1000 away in x/y/z dir.

                // positions reported are relative
                // find pairs of scanners with overlapping detections of at least 12 beacons.

                // don't know orientations. x/y/z can be ordered and negated.

                List<HashSet<Pos>> scanners = new List<HashSet<Pos>>();
                HashSet<Pos> current_scanner = null;

                foreach (var line in ii.GetLines())
                {
                    if (line.StartsWith("---"))
                    {
                        if (current_scanner != null)
                        {
                            scanners.Add(current_scanner);
                        }

                        current_scanner = new HashSet<Pos>();
                    }
                    else if (line.Contains(','))
                    {
                        var parts = line.Split(',');
                        Pos pos = new Pos();
                        pos.x = int.Parse(parts[0]);
                        pos.y = int.Parse(parts[1]);
                        pos.z = int.Parse(parts[2]);
                        current_scanner.Add(pos);
                    }
                }
                scanners.Add(current_scanner);

                List<HashSet<Pos>> scanner_poses = new List<HashSet<Pos>>();
                foreach (var s in scanners)
                {
                    var pos = new HashSet<Pos>();
                    var p = new Pos();
                    p.x = 0;
                    p.y = 0;
                    p.z = 0;
                    pos.Add(p);
                    scanner_poses.Add(pos);
                }

                // Find pairs where they share 12.
                foreach (var s in scanners)
                {
                    for (int i = 0; i < scanners.Count; ++i) {
                        var s1 = scanners[i];
                        if (!s1.Any()) continue;

                        for (int j = 0; j < scanners.Count; ++j)
                        {
                            var s2 = scanners[j];
                            if (!s2.Any()) continue;
                            if (s1 == s2) continue;

                            bool merged = false;
                            // try all transforms...
                            foreach (Transform t in EnumTransforms())
                            {
                                foreach (var b1 in s1)
                                {
                                    foreach (var b2 in s2)
                                    {
                                        var tb2 = t.T(b2);
                                        int count = 0;
                                        int dx = b1.x - tb2.x;
                                        int dy = b1.y - tb2.y;
                                        int dz = b1.z - tb2.z;
                                        // if b1 and b2 are the same, how many do we have the same?
                                        foreach (var b3 in s2)
                                        {
                                            var tb3 = t.T(b3);
                                            tb3.x += dx;
                                            tb3.y += dy;
                                            tb3.z += dz;
                                            if (s1.Contains(tb3))
                                            {
                                                count++;
                                            }
                                        }

                                        if (count >= 12)
                                        {
                                            // merge these, and remove s2, add everything to s1.
                                            foreach (var b in s2)
                                            {
                                                var tb = t.T(b);
                                                tb.x += dx;
                                                tb.y += dy;
                                                tb.z += dz;
                                                if (!s1.Contains(tb))
                                                {
                                                    s1.Add(tb);
                                                }
                                            }
                                            s2.Clear();

                                            // Convert scanner poses.
                                            foreach (var sp in scanner_poses[j])
                                            {
                                                var tsp = t.T(sp);
                                                tsp.x += dx;
                                                tsp.y += dy;
                                                tsp.z += dz;
                                                scanner_poses[i].Add(tsp);
                                            }
                                            scanner_poses[j].Clear();
                                            
                                            merged = true;
                                            break;
                                        }
                                        if (merged)
                                        {
                                            break;
                                        }
                                    }
                                    if (merged)
                                    {
                                        break;
                                    }
                                }
                                if (merged)
                                {
                                    break;
                                }
                            }
                            if (merged)
                            {
                                break;
                            }
                        }
                    }
                }

                

                foreach (var s in scanner_poses)
                {
                    if (!s.Any()) continue;
                    int max = 0;
                    foreach (var sp1 in s)
                    {
                        foreach (var sp2 in s)
                        {
                            int dx = sp1.x - sp2.x;
                            int dy = sp1.y - sp2.y;
                            int dz = sp1.z - sp2.z;
                            int sum = Math.Abs(dx) + Math.Abs(dy) + Math.Abs(dz);
                            if (sum > max) max = sum;
                        }
                    }
                    return "" + max;
                }


                return null;
            });

            Console.WriteLine("done");
            Console.ReadKey();
        }
    }
}
