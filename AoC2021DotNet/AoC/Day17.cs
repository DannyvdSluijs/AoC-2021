using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC2021DotNet.AoC
{
    public class Day17
    {
        public void Part1()
        {
            var data = input.Trim()
                .Split(": ")
                .Last()
                .Split(", ")
                .Select(chunk => chunk[2..].Split("..").Select(int.Parse).ToArray())
                .ToArray();

            Console.WriteLine($"{TriangularNumber(data[1].Min())}");
        }

        public void Part2()
        {
            var data = input.Trim()
                .Split(": ")
                .Last()
                .Split(", ")
                .Select(chunk => chunk[2..].Split("..").Select(int.Parse).ToArray())
                .ToArray();

            // Find min and max for X and Y
            // Min & Max X (forward)
            var minVelocityX = 0;
            var maxVelocityX = data[0][1];
            while (true)
            {
                minVelocityX++;
                if (TriangularNumber(minVelocityX) >= data[0].Min())
                {
                    break;
                }
            }

            // Min & Max Y (Up/Down)
            var minVelocityY = data[1].Min();
            var maxVelocityY = Math.Abs(data[1].Min());

            Console.WriteLine($"X: min: {minVelocityX} and max: {maxVelocityX}");
            Console.WriteLine($"Y: min: {minVelocityY} and max: {maxVelocityY}");

            var validVelocities = (
                from x in Enumerable.Range(minVelocityX, maxVelocityX - minVelocityX + 1)
                from y in Enumerable.Range(minVelocityY, maxVelocityY - minVelocityY + 1)
                where IsValidVelocityValues(x, y, data[0][0], data[0][1], data[1][1], data[1][0])
                select (x, y)).ToList();

            Console.WriteLine($"Valid velocity values {validVelocities.Count}");
        }

        private static bool IsValidVelocityValues(int x, int y, int minX, int maxX, int minY, int maxY)
        {
            var positionX = 0;
            var positionY = 0;

            while (true)
            {
                positionX += x > 0 ? x-- : x;
                positionY += y--;

                if (positionX > maxX || positionY < maxY) return false;
                if (positionX >= minX && positionX <= maxX && positionY <= minY && positionY >= maxY) return true;
            }
        }

        private static int TriangularNumber(int velocity)
        {
            return Math.Abs(velocity) * Math.Abs(velocity + 1) / 2;
        }

        private string testData = @"target area: x=20..30, y=-10..-5";

        private string input = @"target area: x=175..227, y=-134..-79";
    }
}