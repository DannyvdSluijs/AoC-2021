using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC2021DotNet.AoC
{
    public class Day11
    {
        public void Part1()
        {
            var data = input.Trim().Split("\n")
                .Select(line => line.ToCharArray()
                    .Select(c => int.Parse(c.ToString()))
                    .ToList()
                )
                .ToList();

            var values = new Dictionary<(int x, int y), int>();

            for (int y = 0; y < data.First().Count; y++)
            {
                for (int x = 0; x < data.Count; x++)
                {
                    values.Add((x, y), data[x][y]);
                }
            }

            var steps = 100;
            var flashCount = 0;
            for (var i = 0; i < steps; i++)
            {
                Console.WriteLine($"Step {i}");
                for (int j = 0; j < 10; j++)
                {
                    Console.WriteLine(string.Join("", values.Where(kvp => kvp.Key.x == j)
                        .OrderBy(kvp => kvp.Key.y)
                        .Select(kvp => kvp.Value)));
                }
                Console.WriteLine($"");

                values = values.ToDictionary(kvp => kvp.Key, kvp => kvp.Value + 1);
                var flashes = new List<(int x, int y)>();
                while (true)
                {
                    var candidates = values.Where(kvp => kvp.Value > 9)
                        .Select(kvp => kvp.Key)
                        .Where(coord => !flashes.Contains(coord))
                        .ToList();

                    if (!candidates.Any())
                    {
                        break;
                    }

                    flashes.AddRange(candidates);
                    foreach (var (x, y) in candidates)
                    {
                        for (int j = -1; j <= 1; j++)
                        {
                            for (int k = -1; k <= 1; k++)
                            {
                                if (j == 0 && k == 0 || x + j < 0 || y + k < 0 || x + j > 9 || y + k > 9)
                                {
                                    continue;
                                }

                                values[(x + j, y + k)] += 1;
                            }
                        }
                    }
                }

                flashCount += flashes.Count;
                values = values.ToDictionary(kvp => kvp.Key, kvp => kvp.Value > 9 ? 0 : kvp.Value);
            }

            Console.WriteLine($"{flashCount}");
        }

        public void Part2()
        {
            var data = input.Trim().Split("\n")
                .Select(line => line.ToCharArray()
                    .Select(c => int.Parse(c.ToString()))
                    .ToList()
                )
                .ToList();

            var values = new Dictionary<(int x, int y), int>();

            for (int y = 0; y < data.First().Count; y++)
            {
                for (int x = 0; x < data.Count; x++)
                {
                    values.Add((x, y), data[x][y]);
                }
            }

            var steps = 1500;
            var flashCount = 0;
            for (var i = 0; i < steps; i++)
            {
                values = values.ToDictionary(kvp => kvp.Key, kvp => kvp.Value + 1);
                var flashes = new List<(int x, int y)>();
                while (true)
                {
                    var candidates = values.Where(kvp => kvp.Value > 9)
                        .Select(kvp => kvp.Key)
                        .Where(coord => !flashes.Contains(coord))
                        .ToList();

                    if (!candidates.Any())
                    {
                        break;
                    }

                    flashes.AddRange(candidates);
                    foreach (var (x, y) in candidates)
                    {
                        for (int j = -1; j <= 1; j++)
                        {
                            for (int k = -1; k <= 1; k++)
                            {
                                if (j == 0 && k == 0 || x + j < 0 || y + k < 0 || x + j > 9 || y + k > 9)
                                {
                                    continue;
                                }

                                values[(x + j, y + k)] += 1;
                            }
                        }
                    }
                }

                if (flashes.Count == values.Count)
                {
                    Console.Write($"{i + 1}");
                    return;
                }
                flashCount += flashes.Count;
                values = values.ToDictionary(kvp => kvp.Key, kvp => kvp.Value > 9 ? 0 : kvp.Value);
            }

            Console.WriteLine($"{flashCount}");
        }

        private string testData = @"
5483143223
2745854711
5264556173
6141336146
6357385478
4167524645
2176841721
6882881134
4846848554
5283751526
";

        private string input = @"
4871252763
8533428173
7182186813
2128441541
3722272272
8751683443
3135571153
5816321572
2651347271
7788154252
";
    }
}