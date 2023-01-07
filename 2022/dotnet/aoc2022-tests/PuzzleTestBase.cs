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
		var answer = _data.PuzzlePart1.SolvePuzzle(_data.Example);
		Assert.That(answer, Is.EqualTo(_data.Example1ExpectedSolution));
	}

	[Test]
	public void TestExample2()
	{
		var answer = _data.PuzzlePart2.SolvePuzzle(_data.Example);
		Assert.That(answer, Is.EqualTo(_data.Example2ExpectedSolution));
	}

	[Test]
	public void TestSolutionPart1()
	{
		var answer = _data.PuzzlePart1.CalculatePuzzleSolution();
		Assert.That(answer, Is.EqualTo(_data.Part1ExpectedSolution));
	}

	[Test]
	public void TestSolutionPart2()
	{
		var answer = _data.PuzzlePart2.CalculatePuzzleSolution();
		Assert.That(answer, Is.EqualTo(_data.Part2ExpectedSolution));
	}
}