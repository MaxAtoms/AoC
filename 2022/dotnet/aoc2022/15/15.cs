using System.Text.RegularExpressions;

namespace aoc2022._15;

public record Range(int Left, int Right);

public record Coord(int X, int Y);

public record Pair(Coord Sensor, Coord Beacon);

public class AoC_2022_15 : IPuzzle
{
	private readonly int _rowNumber;

	public AoC_2022_15( int rowNumber )
	{
		_rowNumber = rowNumber;
	}
	
	public int NumberOfDay => 15;
	
	public Part Part => Part.Part1;

	// Look Ma, no for-loops!
	public string SolvePuzzle(IEnumerable<string> inputLines)
	{
		var coords = inputLines.Select(GetCoords).ToList();
		var radii = coords.Select(CalcManhattanDistance).ToList();
		var distances = coords.Select(p => Math.Abs(p.Sensor.Y - _rowNumber));

		var sensors = coords.Select(p => p.Sensor)
			.Zip(distances, radii)
			.Select(t => (coords: t.First, distance: t.Second, radius: t.Third))
			.ToList();

		// Filter radii that do not intersect the row
		var relevantSensors = sensors.Where(s => s.distance <= s.radius).ToList();

		var rowIntersections = relevantSensors.Select(t => CalcIntersection(t.coords.X, t.distance, t.radius));
		var positions = rowIntersections
			.Select(r => Enumerable.Range(r.Left, Math.Abs(r.Left - r.Right) + 1))
			.SelectMany(l => l);
		var uniquePositions = new HashSet<int>(positions);

		var positionsWithBeaconPresent = coords.Select(c => c.Beacon)
			.Where(b => b.Y == _rowNumber)
			.Where(b => uniquePositions.Contains(b.X))
			.Select(b => b.X);
		var uniquePositionsWithBeaconPresent = new HashSet<int>(positionsWithBeaconPresent);

		var positionCount = uniquePositions.Count - uniquePositionsWithBeaconPresent.Count;

		return positionCount.ToString();
	}

	private static Range CalcIntersection(int x, int distance, int radius)
	{
		var xDistanceFromCenter = radius - distance;
		var left = x - xDistanceFromCenter;
		var right = x + xDistanceFromCenter;

		return new Range(left, right);
	}

	private static int CalcManhattanDistance(Pair arg)
	{
		var x = Math.Abs(arg.Sensor.X - arg.Beacon.X);
		var y = Math.Abs(arg.Sensor.Y - arg.Beacon.Y);
		return x + y;
	}

	private static Pair GetCoords(string s)
	{
		var matches = GeneratedRegex.ParseSensorReading().Matches(s);
		var groups = matches.First().Groups;

		var m = new Func<int, int>(i => int.Parse(groups[i].Value));

		var sensor = new Coord(m(2), m(5));
		var beacon = new Coord(m(8), m(11));

		return new Pair(sensor, beacon);
	}
}

internal static partial class GeneratedRegex
	{
		[GeneratedRegex(@"(Sensor at x=)((-*)\d+)(, y=)((-*)\d+)(: closest beacon is at x=)((-*)\d+)(, y=)((-*)\d+)")]
		internal static partial Regex ParseSensorReading();
	}