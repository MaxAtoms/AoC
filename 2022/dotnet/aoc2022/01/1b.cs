namespace aoc2022._1;

internal static class AoC_2022_1b
{
	public static void Answer()
	{
		var counter = 0;
		var elfInventory = new List<List<int>>()
		{
			new()
		};

		foreach (var line in File.ReadLines("./1/input.txt"))
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

		Console.WriteLine(topThreeSum);
	}
}