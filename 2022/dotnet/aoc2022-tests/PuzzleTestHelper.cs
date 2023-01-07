using aoc2022;

namespace tests;

public sealed class PuzzleTestData
{
	public required IPuzzle PuzzlePart1 { get; init; }
	public required IPuzzle PuzzlePart2 { get; init; }

	public required string[] Example { get; init; }

	public required string Example1ExpectedSolution { get; init; }
	public required string Example2ExpectedSolution { get; init; }
	
	public required string Part1ExpectedSolution { get; init; }
	public required string Part2ExpectedSolution { get; init; }
}