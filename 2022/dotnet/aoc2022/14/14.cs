namespace aoc2022._14;

public static class AoC_2022_14
{
	private static int _grainOfSandCount;

	// Answer 14a) 692
	// Answer 14b) 31706
	// ReSharper disable once FunctionNeverReturns
	public static void Answer()
	{
		var paths = File.ReadAllLines("./14/input.txt");
		var parsedPaths = paths.Select(ParsePath).ToList();

		var allXCoords = parsedPaths.Select(p => p.Select(tuple => tuple.x))
			.SelectMany(x => x).ToList();
		var allYCoords = parsedPaths.Select(p => p.Select(tuple => tuple.y))
			.SelectMany(y => y).ToList();

		var maxX = allXCoords.Max();
		var maxY = allYCoords.Max();

		// b) Larger canvas
		//maxY = maxY + 2;
		//maxX = maxX + 200;
		
		var caveSlice = new char[maxY + 1, maxX + 1];

		foreach (var path in parsedPaths)
		{
			path.ForEachNext((first, second) => DrawSinglePath(caveSlice, first, second));
		}

		// b) Draw floor
		//for (var i = 0; i < caveSlice.GetLength(1); i++)
		//{
		//	caveSlice[caveSlice.GetLength(0)-1, i] = '#';
		//}

		while (true)
		{
			InjectSand(caveSlice, maxY);
		}
	}

	private static void InjectSand(char[,] caveSlice, int maxY)
	{
		var y = 0;
		var x = 500;
		var blocking = new[] { '#', 'o' };
		
		while (y < maxY + 1)
		{
			if (blocking.Contains(caveSlice[y, x]) && blocking.Contains(caveSlice[y, x - 1]) && blocking.Contains(caveSlice[y, x + 1]))
			{
				_grainOfSandCount++;
				caveSlice[y - 1, x] = 'o';
				
				// Solution b)
				//if (y-1 == 0 && x == 500)
				//{
				//	DrawCanvas(caveSlice);
				//	throw new SuccessException($"Flow stopped at {_grainOfSandCount} grains of sand");
				//}
				break;
			}

			if (blocking.Contains(caveSlice[y, x]) && blocking.Contains(caveSlice[y, x - 1]))
			{
				x++;
			}

			if (blocking.Contains(caveSlice[y, x]))
			{
				x--;
			}

			y++;
		}

		// a)
		if (y == maxY + 1)
		{
			DrawCanvas(caveSlice);
			throw new SuccessException($"Flow stopped at {_grainOfSandCount} grains of sand.");
		}
	}

	private static void DrawSinglePath(char[,] canvas, (int x, int y) first, (int x, int y) second)
	{
		if (first.x != second.x)
		{
			var lower = first.x < second.x ? first.x : second.x;
			var upper = first.x < second.x ? second.x : first.x;

			for (var i = lower; i <= upper; i++)
			{
				canvas[first.y, i] = '#';
			}
		}
		else if (first.y != second.y)
		{
			var lower = first.y < second.y ? first.y : second.y;
			var upper = first.y < second.y ? second.y : first.y;

			for (var i = lower; i <= upper; i++)
			{
				canvas[i, first.x] = '#';
			}
		}
	}

	private static List<(int x, int y)> ParsePath(string s)
	{
		var numbers = s.Split(" -> ");
		var points = numbers.Select(n => n.Split(','))
			.Select(n => (int.Parse(n[0]), int.Parse(n[1])));
		return points.ToList();
	}
	
	private static void DrawCanvas(char[,] caveSlice)
	{
		for (var i = 0; i < caveSlice.GetLength(0); i++)
		{
			for (var j = 0; j < caveSlice.GetLength(1); j++)
			{
				var charAtPos = caveSlice[i, j] == '\0' ? '.' : caveSlice[i, j];
				Console.Write(charAtPos);
			}

			Console.Write('\n');
		}
	}
	
}

public static class EnumerableExtensions
{
	public static void ForEachNext<T>(this IList<T> collection, Action<T, T> func)
	{
		for (var i = 0; i < collection.Count - 1; i++)
		{
			func(collection[i], collection[i + 1]);
		}
	}
}

public class SuccessException : Exception
{
	public SuccessException( string message ) : base(message)
	{
	}
}