using aoc2022;
using aoc2022._02;

namespace tests;

public sealed class AoC_2022_2_Tests : PuzzleTestBase
{
	public AoC_2022_2_Tests() : base(Data) {}
	
	private static readonly string[] Example = {
		"A Y",
		"B X",
		"C Z"
	};

	private static readonly PuzzleTestData Data = new()
	{
		PuzzlePart1 = new AoC_2022_2a(),
		PuzzlePart2 = new AoC_2022_2b(),
		Example = Example,
		
		Example1ExpectedSolution = "15",
		Example2ExpectedSolution = "12",
		Part1ExpectedSolution = "15691",
		Part2ExpectedSolution = "12989"
	};
}