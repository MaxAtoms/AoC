namespace tests;

public abstract class PuzzleTestBase
{
	private readonly PuzzleTestData _data;

	protected PuzzleTestBase( PuzzleTestData data )
	{
		_data = data;
	}
	
	[Test]
	public void TestExample1()
	{
		var answer = _data.Examples.PuzzleExample1.SolvePuzzle(_data.Examples.Example);
		Assert.That(answer, Is.EqualTo(_data.Examples.Example1ExpectedSolution));
	}

	[Test]
	public void TestExample2()
	{
		var answer = _data.Examples.PuzzleExample2.SolvePuzzle(_data.Examples.Example);
		Assert.That(answer, Is.EqualTo(_data.Examples.Example2ExpectedSolution));
	}

	[Test]
	public void TestSolutionPart1()
	{
		var answer = _data.Parts.PuzzlePart1.CalculatePuzzleSolution();
		Assert.That(answer, Is.EqualTo(_data.Parts.Part1ExpectedSolution));
	}

	[Test]
	public void TestSolutionPart2()
	{
		var answer = _data.Parts.PuzzlePart2.CalculatePuzzleSolution();
		Assert.That(answer, Is.EqualTo(_data.Parts.Part2ExpectedSolution));
	}
}