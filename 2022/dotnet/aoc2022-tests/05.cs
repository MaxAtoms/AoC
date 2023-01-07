using aoc2022;
using aoc2022._05;

namespace tests;

public class AoC_2022_5_Tests
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
	
	[Test]
	public void ExamplePart1()
	{
		var answer = new AoC_2022_5().SolvePuzzle(Example);
		Assert.That( answer, Is.EqualTo( "CMZ" ) );
	}

	[Test]
	public void ExamplePart2()
	{
		var answer = new AoC_2022_5(true).SolvePuzzle(Example);
		Assert.That( answer, Is.EqualTo( "MCD" ) );
	}

	[Test]
	public void PuzzleSolutionPart1()
	{
		IPuzzle puzzle = new AoC_2022_5();
		var answer = puzzle.CalculatePuzzleSolution();
		Assert.That( answer, Is.EqualTo( "FRDSQRRCD" ));
	}
	
	[Test]
	public void PuzzleSolutionPart2()
	{
		IPuzzle puzzle = new AoC_2022_5(true);
		var answer = puzzle.CalculatePuzzleSolution();
		Assert.That( answer, Is.EqualTo( "HRFTQVWNN" ));
	}
}