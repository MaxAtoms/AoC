namespace aoc2022._8;

internal static class AoC_2022_8
{
	private record Tree(int Height)
	{
		public bool Visible { get; set; }
	}

	// Answer 8a) 1736
	public static void Answer()
	{
		var treeMap = File.ReadAllLines("./8/input.txt");

		var intMap = treeMap.Select(l => l.ToCharArray())
			.Select(l => l.Select(d => new Tree(int.Parse(d.ToString()))));

		var sum = DetermineNumOfVisibleTreesInGrid(intMap);
		Console.WriteLine(sum);
	}

	private static object DetermineNumOfVisibleTreesInGrid(IEnumerable<IEnumerable<Tree>> treeMap)
	{
		var treeMapAsList = treeMap.Select( l => l.ToList() ).ToList();

		// from left
		foreach (var treeLine in treeMapAsList)
		{
			MarkVisibleTreesInLine(treeLine);
		}

		// from right
		foreach (var treeLine in treeMapAsList)
		{
			treeLine.Reverse();
			MarkVisibleTreesInLine(treeLine);
		}

		var transposedTreeMap = treeMapAsList.SelectMany(inner => inner.Select((item, index) => new { item, index }))
			.GroupBy(i => i.index, i => i.item)
			.Select(g => g.ToList())
			.ToList();

		// from top
		foreach (var treeLine in transposedTreeMap)
		{
			MarkVisibleTreesInLine(treeLine);
		}

		// from bottom
		foreach (var treeLine in transposedTreeMap)
		{
			treeLine.Reverse();
			MarkVisibleTreesInLine(treeLine);
		}

		return transposedTreeMap.Sum(treeLine => treeLine.Count(tree => tree.Visible));
	}

	private static void MarkVisibleTreesInLine(IList<Tree> treeLine)
	{
		// Attention! Trees can have height 0!
		var currentHighestTreeInLine = -1;

		foreach (var currentTree in treeLine)
		{
			if (currentTree.Height <= currentHighestTreeInLine)
			{
				continue;
			}

			currentTree.Visible = true;
			currentHighestTreeInLine = currentTree.Height;
		}
	}
}