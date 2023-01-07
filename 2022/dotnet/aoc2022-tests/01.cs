using aoc2022._01;

namespace tests;

public sealed class AoC_2022_1_Tests
{
	private readonly PuzzleTestHelper _puzzleTestHelper = new()
	{
		PuzzlePart1 = new AoC_2022_1a(),
		PuzzlePart2 = new AoC_2022_1b(),
		Example = Example
	};
	
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

	[Test]
	public void ExamplePart1()
	{
		_puzzleTestHelper.TestExample1( "24000" );
	}

	[Test]
	public void ExamplePart2()
	{
		_puzzleTestHelper.TestExample2( "45000" );
	}

	[Test]
	public void PuzzleSolutionPart1()
	{
		_puzzleTestHelper.TestSolutionPart1( "64929" );
	}
	
	[Test]
	public void PuzzleSolutionPart2()
	{
		_puzzleTestHelper.TestSolutionPart2( "193697" );
	}
}