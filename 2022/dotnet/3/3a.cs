namespace aoc2022;

internal static class AoC_2022_3a
{
	public static void Answer()
	{
		var lines = File.ReadAllLines("./3/input.txt");
		var rucksacks = lines.Select(l => l.Trim())
			.Select(l => new List<char[]>
			{
				l[..(l.Length / 2)].ToCharArray(),
				l[(l.Length / 2)..].ToCharArray()
			});
		var duplicateItems = rucksacks.Select(r => r[0].Intersect(r[1]));
		var flattened = duplicateItems.SelectMany(l => l);

		var priorities = flattened.Select(c => c > 96 ? c - 96 :c - 64 + 26);
		
		Console.WriteLine(priorities.Sum());
	}
}