using aoc2022._01;

namespace tests;

[TestFixture]
public sealed class AoC_2022_1_Tests : PuzzleTestBase
{
	public AoC_2022_1_Tests() : base(PuzzleTestHelper) {}
	
	private static readonly string[] Example = {
		"1000",
		"2000",
		"3000",
		"",
		"4000",
		"",
		"5000",
		"6000",
		"",
		"7000",
		"8000",
		"9000",
		"",
		"10000"
	};
	
	private static readonly PuzzleTestData PuzzleTestHelper = new()
	{
		PuzzlePart1 = new AoC_2022_1a(),
		PuzzlePart2 = new AoC_2022_1b(),
		Example = Example,
		
		Example1ExpectedSolution = "24000",
		Example2ExpectedSolution = "45000",
		Part1ExpectedSolution = "64929",
		Part2ExpectedSolution = "193697"
	};
}