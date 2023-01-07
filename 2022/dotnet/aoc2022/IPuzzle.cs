namespace aoc2022;

public interface IPuzzle
{
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
	///		Denotes the number of the puzzle (i.e. day 1 - 25).
	/// </summary>
	internal int NumberOfDay { get; }

	/// <summary>
	///		Denotes the part of the puzzle (i.e. part 1 or part 2).
	/// </summary>
	internal abstract Part Part { get; }
	
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