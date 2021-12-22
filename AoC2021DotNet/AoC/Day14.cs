using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Xml.XPath;

namespace AoC2021DotNet.AoC
{
    public class Day14
    {
        public void Part1()
        {
            Solve(10, input);
        }

        public void Part2()
        {
            Solve(40, input);
        }

        private void Solve(int cycles, string puzzleInput)
        {
            var data = puzzleInput.Trim().Split("\n\n");

            var polymer = data.First().Trim();
            var mapping = data.Last().Trim()
                .Split("\n")
                .Select(line => line.Split(" -> "))
                .ToDictionary(
                    items => items.First(),
                    items => $"{items.First()[0]}{items.Last().ToCharArray().First()}{items.First()[1]}");

            var chunks = new Dictionary<string, long>();
            for (var i = 0; i < polymer.Length - 1; i++)
            {
                var chunk = polymer.Substring(i, 2);
                IncreaseChunk(chunk, 1, chunks);
            }

            for (var i = 0; i < cycles; i++)
            {
                foreach (var kvp in chunks.ToArray())
                {
                    DecreaseChunk(kvp.Key, kvp.Value, chunks);
                    var replacement = mapping[kvp.Key];
                    IncreaseChunk(replacement.Substring(0, 2), kvp.Value, chunks);
                    IncreaseChunk(replacement.Substring(1, 2), kvp.Value, chunks);
                }
            }

            var chars = new Dictionary<string, long>();
            foreach (var kvp in chunks)
            {
                IncreaseChunk(kvp.Key.Substring(0, 1), kvp.Value, chars);
            }

            IncreaseChunk(polymer[^1..], 1, chars);

            var min = chars.Min(kvp => kvp.Value);
            var max = chars.Max(kvp => kvp.Value);

            Console.WriteLine($"{max} - {min} = {max - min}");
        }


        private void IncreaseChunk(string chunk, long increment, Dictionary<string, long> chunks)
        {
            if (!chunks.ContainsKey(chunk))
            {
                chunks.Add(chunk, increment);
            }
            else
            {
                chunks[chunk] += increment;
            }
        }

        private void DecreaseChunk(string chunk, long increment, Dictionary<string, long> chunks)
        {
            if (!chunks.ContainsKey(chunk))
            {
                throw new Exception("Cannot decrease on missing key");
            }

            chunks[chunk] -= increment;
        }

        private string testData = @"
NNCB

CH -> B
HH -> N
CB -> H
NH -> C
HB -> C
HC -> B
HN -> C
NN -> C
BH -> H
NC -> B
NB -> B
BN -> B
BB -> N
BC -> B
CC -> N
CN -> C
";

        private string input = @"
HHKONSOSONSVOFCSCNBC

OO -> N
VK -> B
KS -> N
PK -> H
FB -> H
BF -> S
BB -> V
KO -> N
SP -> K
HK -> O
PV -> K
BP -> O
VO -> V
OP -> C
BS -> V
OK -> V
KN -> H
KC -> N
PP -> F
NB -> V
CH -> V
HO -> K
PN -> H
SS -> O
CK -> P
VV -> K
FN -> O
BH -> B
SC -> B
HH -> P
FO -> O
CC -> H
OS -> H
FP -> S
HC -> F
BO -> F
CF -> S
NC -> S
HS -> V
KF -> O
ON -> C
CN -> K
VF -> F
NO -> K
CP -> N
HF -> K
CV -> N
HN -> K
VH -> B
KK -> P
CS -> O
VS -> P
NH -> F
CB -> S
BV -> P
FK -> F
NV -> O
OV -> K
SB -> N
NF -> O
VN -> S
OH -> O
PS -> N
HB -> H
SV -> V
CO -> H
SO -> P
FV -> N
PF -> O
NN -> S
KB -> P
NP -> F
OC -> S
FS -> P
FH -> P
VP -> K
BN -> O
NS -> H
VB -> V
PO -> K
KP -> N
SN -> O
BC -> H
SF -> V
PC -> O
NK -> F
BK -> V
KH -> S
SH -> S
SK -> H
OB -> V
PH -> N
PB -> C
HV -> N
HP -> V
FF -> B
OF -> P
VC -> S
KV -> C
FC -> F
";
    }
}