namespace aoc2022._9;

public static class AoC_2022_9a
{
	private record End
	{
		public int X { get; set; }
		public int Y { get; set; }
	}

	private record Head : End;
	private record Tail : End;


	// Answer 9a) 6464
	public static void Answer()
	{
		var motions = File.ReadAllLines("./9/input.txt");
		
		var head = new Head();
		var tail = new Tail();
		var visitedPositions = new HashSet<End>();

		foreach (var motion in motions)
		{
			ExecuteMotion(motion, head, tail, visitedPositions);
		}

		Console.WriteLine( visitedPositions.Count );
	}

	private static void ExecuteMotion(
		string motion,
		Head head,
		Tail tail,
		ISet<End> visitedPositions)
	{
		var splitMotion = motion.Split(' ');
		var direction = splitMotion.First()[0];
		var count = int.Parse(splitMotion.Last());

		for (var i = 0; i < count; i++)
		{
			UpdateHeadPosition(head, direction);
			UpdateTailPosition(head, tail);
			visitedPositions.Add(tail with { });
		}
	}

	private static void UpdateHeadPosition(Head head, char direction)
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

	private static void UpdateTailPosition(Head head, Tail tail) 
	{
		switch (head.X - tail.X)
		{
			case 2:
				tail.X += 1;
				tail.Y = head.Y;
				return;
			case -2:
				tail.X -= 1;
				tail.Y = head.Y;
				return;
		}

		switch (head.Y - tail.Y)
		{
			case 2:
				tail.Y += 1;
				tail.X = head.X;
				return;
			case -2:
				tail.Y -= 1;
				tail.X = head.X;
				return;
		}
	}
}