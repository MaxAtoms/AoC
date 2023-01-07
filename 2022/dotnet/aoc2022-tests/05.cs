using aoc2022;
using aoc2022._02;
using aoc2022._05;

namespace tests;

[TestFixture]
public sealed class AoC_2022_5_Tests : PuzzleTestBase
{
	private static readonly string[] Example = {
		"    [D]    ",
		"[N] [C]    ",
		"[Z] [M] [P]",
		"1   2   3  ",
		"",
		"move 1 from 2 to 1",
		"move 3 from 1 to 3",
		"move 2 from 2 to 1",
		"move 1 from 1 to 2"
	};
	

	private static readonly PuzzleTestData Data = new()
	{
		PuzzlePart1 = new AoC_2022_5a(),
		PuzzlePart2 = new AoC_2022_5b(),
		Example = Example,
		
		Example1ExpectedSolution = "CMZ",
		Example2ExpectedSolution = "MCD",
		Part1ExpectedSolution = "FRDSQRRCD",
		Part2ExpectedSolution = "HRFTQVWNN"
	};

	public AoC_2022_5_Tests() : base(Data) {}
}