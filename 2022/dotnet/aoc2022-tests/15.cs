using aoc2022;
using aoc2022._02;
using aoc2022._05;
using aoc2022._15;

namespace tests;

[TestFixture]
public sealed class AoC_2022_15_Tests : PuzzleTestBase
{
	private static readonly string[] Example =
	{
		"Sensor at x=2, y=18: closest beacon is at x=-2, y=15",
		"Sensor at x=9, y=16: closest beacon is at x=10, y=16",
		"Sensor at x=13, y=2: closest beacon is at x=15, y=3",
		"Sensor at x=12, y=14: closest beacon is at x=10, y=16",
		"Sensor at x=10, y=20: closest beacon is at x=10, y=16",
		"Sensor at x=14, y=17: closest beacon is at x=10, y=16",
		"Sensor at x=8, y=7: closest beacon is at x=2, y=10",
		"Sensor at x=2, y=0: closest beacon is at x=2, y=10",
		"Sensor at x=0, y=11: closest beacon is at x=2, y=10",
		"Sensor at x=20, y=14: closest beacon is at x=25, y=17",
		"Sensor at x=17, y=20: closest beacon is at x=21, y=22",
		"Sensor at x=16, y=7: closest beacon is at x=15, y=3",
		"Sensor at x=14, y=3: closest beacon is at x=15, y=3",
		"Sensor at x=20, y=1: closest beacon is at x=15, y=3"
	};
	
	private static readonly PuzzleTestData Data = new()
	{
		Examples = new PuzzleExamples
		{
			PuzzleExample1 = new AoC_2022_15(10),
			PuzzleExample2 = new AoC_2022_15(10),
			Example = Example,
			Example1ExpectedSolution = "26",
			Example2ExpectedSolution = "56000011"
		},
		Parts = new PuzzleParts
		{
			PuzzlePart1 = new AoC_2022_15(2_000_000),
			PuzzlePart2 = new AoC_2022_15(2_000_000),
			Part1ExpectedSolution = "4725496",
			Part2ExpectedSolution = ""
		}
	};

	public AoC_2022_15_Tests() : base(Data) {}
}