namespace aoc2022._01;

public sealed class AoC_2022_1a : IPuzzle
{
	public int NumberOfDay => 1;
	
	public Part Part => Part.Part1;

	public string SolvePuzzle( IEnumerable<string> inputLines )
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
		return sums.Max().ToString();
	}
}