using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC2021DotNet.AoC
{
    public class Day12
    {
        public void Part1()
        {
            var data = input.Split("\n")
                .Select(line => (From: line.Split("-").First(), To: line.Split("-").Last()))
                .ToList();

            var paths = GetPaths("start", data, new List<string>(), true);

            Console.WriteLine($"Paths: {paths.Count}");
        }

        public void Part2()
        {
            var data = input.Split("\n")
                .Select(line => (From: line.Split("-").First(), To: line.Split("-").Last()))
                .ToList();

            var paths = GetPaths("start", data, new List<string>());

            Console.WriteLine($"Paths: {paths.Count}");
        }

        private static List<List<string>> GetPaths(string from, List<(string from, string to)> instructions, List<string> previousSteps, bool visitedSmallTwice = false)
        {
            var paths = new List<List<string>>();
            previousSteps.Add(from);

            if (from == "end")
            {
                return new List<List<string>> { previousSteps };
            }
            var options = instructions.Where(inst => inst.from == from || inst.to == from)
                .Select(instr => instr.from == from ? instr.to : instr.from)
                .Where(destination => destination.ToLower() != destination || !previousSteps.Contains(destination));

            foreach (var option in options)
            {
                paths.AddRange(GetPaths(option, instructions, previousSteps.ToList(), visitedSmallTwice));
            }

            if (visitedSmallTwice) return paths;

            var smallCaves = instructions.Where(inst => inst.@from == @from || inst.to == @from)
                .Select(instr => instr.@from == @from ? instr.to : instr.@from)
                .Where(destination => destination.ToLower() == destination && previousSteps.Count(dest => dest == destination) == 1)
                .Where(destination => destination.Length <= 2);
            foreach (var option in smallCaves)
            {
                paths.AddRange(GetPaths(option, instructions, previousSteps.ToList(), true));
            }

            return paths;
        }

        private string testData = @"
fs-end
he-DX
fs-he
start-DX
pj-DX
end-zg
zg-sl
zg-pj
pj-he
RW-he
fs-DX
pj-RW
zg-RW
start-pj
he-WI
zg-he
pj-fs
start-RW
";

        private string input = @"
BC-gt
gt-zf
end-KH
end-BC
so-NL
so-ly
start-BC
NL-zf
end-LK
LK-so
ly-KH
NL-bt
gt-NL
start-zf
so-zf
ly-BC
BC-zf
zf-ly
ly-NL
ly-LK
IA-bt
bt-so
ui-KH
gt-start
KH-so
";
    }
}