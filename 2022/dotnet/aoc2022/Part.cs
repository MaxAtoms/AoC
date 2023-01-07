namespace aoc2022;

public static class PartExtensions
{
	public static string GetNumber(this Part part ) => 
		part == Part.Part1 ? "1" : "2";
}

public enum Part
{
	Part1,
	Part2
}