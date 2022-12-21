namespace aoc2022._9;

public static class AoC_2022_9b
{
	private record Knot
	{
		public int X { get; set; }
		public int Y { get; set; }
	}

	// Answer 9b) 2604
	public static void Answer()
	{
		var motions = File.ReadAllLines("./9/input.txt");
		var rope = Enumerable.Range(0, 10).Select(_ => new Knot()).ToArray();
		var visitedPositions = new HashSet<Knot>();

		foreach (var motion in motions)
		{
			ExecuteMotion(motion, rope, visitedPositions);
		}

		Console.WriteLine(visitedPositions.Count);
	}

	private static void ExecuteMotion(
		string motion,
		IReadOnlyList<Knot> rope,
		ISet<Knot> visitedPositions)
	{
		var splitMotion = motion.Split(' ');
		var direction = splitMotion.First()[0];
		var count = int.Parse(splitMotion.Last());

		for (var i = 0; i < count; i++)
		{
			UpdateHeadPosition(rope.First(), direction);

			for (var j = 1; j < rope.Count; j++)
			{
				UpdateTailPosition(rope[j - 1], rope[j]);
			}

			visitedPositions.Add(rope.Last() with { });
		}
	}

	private static void UpdateHeadPosition(Knot head, char direction)
	{
		switch (direction)
		{
			case 'U':
				head.X -= 1;
				break;
			case 'D':
				head.X += 1;
				break;
			case 'L':
				head.Y -= 1;
				break;
			case 'R':
				head.Y += 1;
				break;
		}
	}

	private static void UpdateTailPosition(Knot head, Knot tail)
	{
		// TODO Clean up this mess
		if ( head.X - tail.X == -2 && head.Y - tail.Y == -2)
		{
			tail.X -= 1;
			tail.Y -= 1;
			return;
		}
		
		if ( head.X - tail.X == 2 && head.Y - tail.Y == -2)
		{
			tail.X += 1;
			tail.Y -= 1;
			return;
		}
		
		if ( head.X - tail.X == 2 && head.Y - tail.Y == 2)
		{
			tail.X += 1;
			tail.Y += 1;
			return;
		}
		
		if ( head.X - tail.X == -2 && head.Y - tail.Y == 2)
		{
			tail.X -= 1;
			tail.Y += 1;
			return;
		}
		
		switch (head.X - tail.X)
		{
			case 2:
				tail.X += 1;
				tail.Y = head.Y;
				break;
			case -2:
				tail.X -= 1;
				tail.Y = head.Y;
				break;
		}

		switch (head.Y - tail.Y)
		{
			case 2:
				tail.Y += 1;
				tail.X = head.X;
				break;
			case -2:
				tail.Y -= 1;
				tail.X = head.X;
				return;
		}
	}
}