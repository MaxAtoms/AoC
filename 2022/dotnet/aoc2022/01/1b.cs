namespace aoc2022._01;

public class AoC_2022_1b : IChallenge
{
	public string CalculatePuzzleSolution()
	{
		var fileLines = File.ReadLines("./01/input.txt");
		return SolvePuzzle( fileLines );
	}
	
	public string SolvePuzzle(IEnumerable<string> inputLines)
	{
		var counter = 0;
		var elfInventory = new List<List<int>>
		{
			new()
		};

		foreach (var line in inputLines)
		{
			if (line != "")
			{
				var value = int.Parse(line);
				elfInventory[counter] = elfInventory[counter].Append(value).ToList();
			}
			else
			{
				counter++;
				elfInventory = elfInventory.Append(new List<int>()).ToList();
			}
		}

		var sums = elfInventory.Select(singleElfInventory => singleElfInventory.Sum());

		var orderedSums = sums.OrderDescending();

		var topThreeElfs = orderedSums.Take(3);

		var topThreeSum = topThreeElfs.Sum();

		return topThreeSum.ToString();
	}
}