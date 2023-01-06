namespace aoc2022._4;

internal static class AoC_2022_4a
{
	private record Range(int start, int end);

	public static void Answer()
	{
		var lines = File.ReadAllLines("./4/input.txt");
		var ranges = lines.Select(l => l.Split(",", 2))
			.Select(l => (l[0], l[1]))
			.Select(pair =>
			{
				var (elf1, elf2) = pair;
				var elf1Values = elf1.Split("-", 2);
				var range1 = new Range(int.Parse(elf1Values[0]), int.Parse(elf1Values[1]));

				var elf2Values = elf2.Split("-", 2);
				var range2 = new Range(int.Parse(elf2Values[0]), int.Parse(elf2Values[1]));

				return (range1, range2);
			});

		var doFullyOverlap = ranges.Select(r => CheckIfFullyOverlap(r.range1, r.range2));
		Console.WriteLine(doFullyOverlap.Count(l => l));
	}

	private static bool CheckIfFullyOverlap(Range range1, Range range2)
	{
		var range1ContainedInRange2 = range1.start >= range2.start && range1.end <= range2.end;
		var range2ContainedInRange1 = range2.start >= range1.start && range2.end <= range1.end;

		return range1ContainedInRange2 || range2ContainedInRange1;
	}
}