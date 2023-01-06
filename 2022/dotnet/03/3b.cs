namespace aoc2022._3;

internal static class AoC_2022_3b
{
	public static void Answer()
	{
		var lines = File.ReadAllLines("./3/input.txt");

		var groups = lines.Chunk(3);

		var commonItemsPerGroup = groups.Select(g => g[0].Intersect(g[1]).Intersect(g[2]));

		if (commonItemsPerGroup.Any(items => items.Count() != 1))
		{
			throw new Exception("Found multiple common items in group");
		}

		var commonItemPerGroup = commonItemsPerGroup.Select(g => g.First());
		
		var priorities = commonItemPerGroup.Select(c => c > 96 ? c - 96 :c - 64 + 26);
		
		Console.WriteLine(priorities.Sum());
	}
}