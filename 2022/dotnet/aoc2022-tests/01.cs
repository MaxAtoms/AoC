using aoc2022._01;

namespace tests;

public class AoC_2022_1_Tests
{
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
		var answer = new AoC_2022_1a().SolvePuzzle(Example);
		Assert.That( answer, Is.EqualTo( "24000" ) );
	}

	[Test]
	public void ExamplePart2()
	{
		var answer = new AoC_2022_1b().SolvePuzzle(Example);
		Assert.That( answer, Is.EqualTo( "45000" ) );
	}

	[Test]
	public void PuzzleSolutionPart1()
	{
		var answer = new AoC_2022_1a().CalculatePuzzleSolution();
		Assert.That( answer, Is.EqualTo( "64929" ));
	}
	
	[Test]
	public void PuzzleSolutionPart2()
	{
		var answer = new AoC_2022_1b().CalculatePuzzleSolution();
		Assert.That( answer, Is.EqualTo( "193697" ));
	}
}