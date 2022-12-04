namespace aoc2022;

internal static class AoC_2022_1a
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

		Console.WriteLine(sums.Max());
	}
}