namespace aoc2022;

public interface IChallenge
{
	/// <summary>
	///		Calculates the solution and prints the answer on screen.
	/// </summary>
	public void CalculateAndPrintPuzzleSolution()
	{
		var answer = CalculatePuzzleSolution();
		Console.WriteLine(answer);
	}

	/// <summary>
	///		Calculates the solution for the puzzle.
	/// </summary>
	/// <returns></returns>
	internal string CalculatePuzzleSolution();

	/// <summary>
	///		Calculates the answer for a given input.
	/// </summary>
	/// <param name="inputLines">
	///		Puzzle input file.
	/// </param>
	/// <returns>
	///		Answer converted to a string.
	/// </returns>
	internal string SolvePuzzle(IEnumerable<string> inputLines);
}