namespace aoc2022;

public interface IPuzzle
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
	/// <returns>
	///		Solution as string.
	/// </returns>
	internal string CalculatePuzzleSolution()
	{
		var leadingZero = NumberOfDay < 10 ? "0" : "";
		var fileLines = File.ReadLines($"./{leadingZero}{NumberOfDay}/input.txt");
		return SolvePuzzle( fileLines );
	}

	/// <summary>
	///		Denotes the number of the puzzle.
	/// </summary>
	internal int NumberOfDay { get; }

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