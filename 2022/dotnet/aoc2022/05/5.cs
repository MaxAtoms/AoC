using System.Text.RegularExpressions;

namespace aoc2022._05;

public sealed class AoC_2022_5 : IPuzzle
{
	public int NumberOfDay => 5;
	
	private readonly bool _part2;

	public AoC_2022_5( bool part2 = false )
	{
		_part2 = part2;
	}

	private record Command(int Count, int OriginPos, int EndPos);

	public string SolvePuzzle(IEnumerable<string> inputLines)
	{
		var lines = inputLines.ToArray();
		var separationIndex = lines.Select((v, i) => (v, i))
			.First(l => l.v == "").i;

		var stackLines = lines[..separationIndex];
		var commandLines = lines[(separationIndex + 1)..];

		var stacks = ParseStacks(stackLines.ToList());
		ParseAndInterpretCommands(commandLines, stacks);

		WriteStacks(stacks, "");
		var topCrates = stacks.Select(i => i.Last());
		
		return string.Join("", topCrates);
	}

	private void ParseAndInterpretCommands(IEnumerable<string> commandLines, IReadOnlyList<List<char>> stacks)
	{
		foreach (var line in commandLines)
		{
			WriteStacks(stacks, line);
			var command = ParseCommand(line);
			ExecuteCommand(command, stacks);
		}
	}

	private static void WriteStacks(IReadOnlyList<List<char>> readOnlyList, string line)
	{
		Console.WriteLine(line);
		
		for (var i = 0; i < readOnlyList.Count; i++)
		{
			Console.WriteLine($"Stack {i + 1}: {string.Join(" ", readOnlyList[i])}");
		}

		Console.WriteLine("\n\n");
	}

	private static Command ParseCommand(string line)
	{
		var matches = GeneratedRegex.GetArgumentsFromCommand().Matches(line);
		var groups = matches.First().Groups;
		var m = new Func<int, int>(i => int.Parse(groups[i].Value));
		return new Command(m(1), m(2) - 1, m(3) - 1);
	}

	private void ExecuteCommand(Command command, IReadOnlyList<List<char>> stacks)
	{
		var originStack = stacks[command.OriginPos];
		
		var crates = originStack.TakeLast(command.Count);

		if (!_part2)
		{
			crates = crates.Reverse();
		}
		
		stacks[command.EndPos].AddRange(crates);
		originStack.RemoveRange(originStack.Count - command.Count, command.Count);
	}

	private static IReadOnlyList<List<char>> ParseStacks(ICollection<string> stackLines)
	{
		var numOfStacks = stackLines.Last().ToCharArray()
			.Where(char.IsDigit)
			.Select(i => int.Parse(i.ToString()))
			.Max();

		var stacks = new List<List<char>>();

		for (var i = 0; i < numOfStacks; i++)
		{
			var stack = stackLines.Take(stackLines.Count - 1)
				.Select(line => line.Skip(1 + i * 4).Take(1).First())
				.Where(c => c is not ' ')
				.Reverse()
				.ToList();
			stacks = stacks.Append(stack).ToList();
		}

		return stacks;
	}
}

internal static partial class GeneratedRegex
{
	[GeneratedRegex(@"move (\d+) from (\d+) to (\d+)")]
	internal static partial Regex GetArgumentsFromCommand();
}