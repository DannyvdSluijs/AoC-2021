using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC2021DotNet.AoC
{
    public class Day14
    {
        public void Part1()
        {
            var data = input.Split("\n")
                .Select(line => (From: line.Split("-").First(), To: line.Split("-").Last()))
                .ToList();
        }

        public void Part2()
        {

        }

        private string testData = @"
";

        private string input = @"

";
    }
}