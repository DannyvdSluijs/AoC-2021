using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC2021DotNet.AoC
{
    public class Day6
    {
        public void Part1()
        {
            var groups = Run(ParseInput(input), 8);

            Console.WriteLine($"There would be a total of {groups.Select(g => g.Value).Sum()}");
        }

        public void Part2()
        {
            var groups = Run(ParseInput(input), 256);

            Console.WriteLine($"There would be a total of {groups.Select(g => g.Value).Sum()}");
        }

        private Dictionary<int, long> ParseInput(string input)
        {
            var data = input.Trim()
                .Split(",")
                .Select(int.Parse)
                .ToList();

            var groups = data.GroupBy(n => n)
                .ToDictionary(group => group.Key, group => long.Parse(group.Count().ToString()));
            for (var i = 0; i < 8; i++)
            {
                groups.TryAdd(i, 0);
            }

            return groups;
        }

        private Dictionary<int, long> Run(Dictionary<int, long> groups, int days)
        {
            for (var i = 0; i < days; i++)
            {
                var newFish = groups.GetValueOrDefault(0);
                for (var j = 0; j < 8; j++)
                {
                    var value = groups.Remove(j);
                    groups.Add(j, groups.GetValueOrDefault(j + 1));
                }

                var currentSixes = groups.GetValueOrDefault(6);
                groups.Remove(6);
                groups.Add(6, (currentSixes + newFish));

                groups.Remove(8);
                groups.Add(8, newFish);
            }

            return groups;
        }

        private string testData = @"
3,4,3,1,2
";

        private string input = @"
1,2,1,1,1,1,1,1,2,1,3,1,1,1,1,3,1,1,1,5,1,1,1,4,5,1,1,1,3,4,1,1,1,1,1,1,1,5,1,4,1,1,1,1,1,1,1,5,1,3,1,3,1,1,1,5,1,1,1,1,1,5,4,1,2,4,4,1,1,1,1,1,5,1,1,1,1,1,5,4,3,1,1,1,1,1,1,1,5,1,3,1,4,1,1,3,1,1,1,1,1,1,2,1,4,1,3,1,1,1,1,1,5,1,1,1,2,1,1,1,1,2,1,1,1,1,4,1,3,1,1,1,1,1,1,1,1,5,1,1,4,1,1,1,1,1,3,1,3,3,1,1,1,2,1,1,1,1,1,1,1,1,1,5,1,1,1,1,5,1,1,1,1,2,1,1,1,4,1,1,1,2,3,1,1,1,1,1,1,1,1,2,1,1,1,2,3,1,2,1,1,5,4,1,1,2,1,1,1,3,1,4,1,1,1,1,3,1,2,5,1,1,1,5,1,1,1,1,1,4,1,1,4,1,1,1,2,2,2,2,4,3,1,1,3,1,1,1,1,1,1,2,2,1,1,4,2,1,4,1,1,1,1,1,5,1,1,4,2,1,1,2,5,4,2,1,1,1,1,4,2,3,5,2,1,5,1,3,1,1,5,1,1,4,5,1,1,1,1,4
";
    }
}