using aoc2022._02;

namespace tests;

public class AoC_2022_2_Tests
{
	private static readonly string[] Example = {
		"A Y",
		"B X",
		"C Z"
	};

	[Test]
	public void ExamplePart1()
	{
		var example = Example;

		var answer = new AoC_2022_2a().SolvePuzzle(example);
		Assert.That( answer, Is.EqualTo( "15" ) );
	}

	[Test]
	public void ExamplePart2()
	{
		var example = Example;

		var answer = new AoC_2022_2b().SolvePuzzle(example);
		Assert.That( answer, Is.EqualTo( "12" ) );
	}

	[Test]
	public void PuzzleSolutionPart1()
	{
		var answer = new AoC_2022_2a().CalculatePuzzleSolution();
		Assert.That( answer, Is.EqualTo( "15691" ));
	}
	
	[Test]
	public void PuzzleSolutionPart2()
	{
		var answer = new AoC_2022_2b().CalculatePuzzleSolution();
		Assert.That( answer, Is.EqualTo( "12989" ));
	}
}