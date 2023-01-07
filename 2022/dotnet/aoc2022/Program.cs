using System.Runtime.CompilerServices;
using aoc2022;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("aoc2022-tests")]

const int day = 3;
const Part part = Part.Part1;

var provider = DependencyInjection.BuildServiceProvider();
var puzzles = provider.GetServices<IPuzzle>();

var puzzle = puzzles.FirstOrDefault(t => t is { NumberOfDay: day, Part: part });

if (puzzle is null)
{
	throw new Exception($"Requested Puzzle day {day} part {part.GetNumber()} not found");
}

Console.WriteLine($"Running puzzle day {day}, part {part.GetNumber()}");
Console.WriteLine($"The answer is: {puzzle.CalculatePuzzleSolution()}");
