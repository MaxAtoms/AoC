namespace aoc2022._8;

internal static class AoC_2022_8b
{
	private record Tree(int Height)
	{
		public bool Visible { get; set; }

		public int ScenicScore { get; set; }
	}

	// Answer 8b) 268800
	public static void Answer()
	{
		var treeMap = File.ReadAllLines("./8/input.txt");

		var intMap = treeMap.Select(l => l.ToCharArray())
			.Select(l => l.Select(d => new Tree(int.Parse(d.ToString()))));

		var mapWithScores = DetermineScenicScorePerTree(intMap);

		var maxPerRow = mapWithScores.Select( r => r.Select( t => t.ScenicScore ).Max() );
		var globalMax = maxPerRow.Max();

		Console.WriteLine(globalMax);
	}

	private static List<List<Tree>> DetermineScenicScorePerTree(IEnumerable<IEnumerable<Tree>> treeMap)
	{
		var treeMapAsList = treeMap.Select(l => l.ToList()).ToList();

		// Scenic scores for outer trees are always zero, skip them
		for (var i = 1; i < treeMapAsList.Count - 1; i++)
		{
			for (var j = 1; j < treeMapAsList[i].Count - 1; j++)
			{
				treeMapAsList[i][j].ScenicScore = DetermineScenicScoreForTree(treeMapAsList, i, j);
			}
		}

		return treeMapAsList;
	}

	// There is probably a better algorithm that is faster than O(n^2).
	// For n = 10,000 it is still reasonably fast, though.
	private static int DetermineScenicScoreForTree(List<List<Tree>> treeMapAsList, int y, int x)
	{
		var currentTreeHeight = treeMapAsList[y][x].Height;

		// to left
		var leftScore = 0;
		for (var i = x - 1; i >= 0; i--)
		{
			leftScore++;
			
			if (treeMapAsList[y][i].Height >= currentTreeHeight)
			{
				break;
			}
		}

		// to right
		var rightScore = 0;
		for (var i = x + 1; i < treeMapAsList[y].Count; i++)
		{
			rightScore++;
			
			if (treeMapAsList[y][i].Height >= currentTreeHeight)
			{
				break;
			}
		}

		// top
		var topScore = 0;
		for (var i = y - 1; i >= 0; i--)
		{
			topScore++;
			
			if (treeMapAsList[i][x].Height >= currentTreeHeight)
			{
				break;
			}
		}

		// bottom
		var bottomScore = 0;
		for (var i = y + 1; i < treeMapAsList.Count; i++)
		{
			bottomScore++;
			
			if (treeMapAsList[i][x].Height >= currentTreeHeight)
			{
				break;
			}
		}

		return leftScore * rightScore * topScore * bottomScore;
	}
}