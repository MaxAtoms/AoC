using aoc2022;

namespace tests;

internal sealed class PuzzleTestHelper
{
	internal required IPuzzle PuzzlePart1 { get; init; }
	internal required IPuzzle PuzzlePart2 { get; init; }

	internal required string[] Example { get; init; }

	internal void TestExample1(string expectedSolution)
	{
		var answer = PuzzlePart1.SolvePuzzle(Example);
		Assert.That(answer, Is.EqualTo(expectedSolution));
	}

	internal void TestExample2(string expectedSolution)
	{
		var answer = PuzzlePart2.SolvePuzzle(Example);
		Assert.That(answer, Is.EqualTo(expectedSolution));
	}

	internal void TestSolutionPart1(string expectedSolution)
	{
		var answer = PuzzlePart1.CalculatePuzzleSolution();
		Assert.That(answer, Is.EqualTo(expectedSolution));
	}

	internal void TestSolutionPart2(string expectedSolution)
	{
		var answer = PuzzlePart2.CalculatePuzzleSolution();
		Assert.That(answer, Is.EqualTo(expectedSolution));
	}
}