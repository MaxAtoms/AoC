using System.Text.RegularExpressions;

namespace aoc2022._5;

internal static class AoC_2022_5
{
	private record Command(int Count, int OriginPos, int EndPos);

	// Answer 5a) FRDSQRRCD
	// Answer 5b) HRFTQVWNN
	public static void Answer()
	{
		var lines = File.ReadAllLines("./5/input.txt");
		var separationIndex = lines.Select((v, i) => (v, i))
			.First(l => l.v == "").i;

		var stackLines = lines[..separationIndex];
		var commandLines = lines[(separationIndex + 1)..];

		var stacks = ParseStacks(stackLines.ToList());
		ParseAndInterpretCommands(commandLines, stacks);

		WriteStacks(stacks, "");
		var topCrates = stacks.Select(i => i.Last());
		Console.WriteLine(string.Join("", topCrates));
	}

	private static void ParseAndInterpretCommands(IEnumerable<string> commandLines, IReadOnlyList<List<char>> stacks)
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

	private static void ExecuteCommand(Command command, IReadOnlyList<List<char>> stacks)
	{
		var originStack = stacks[command.OriginPos];
		
		var crates = originStack.TakeLast(command.Count);
		
		// 5a
		//crates = crates.Reverse();
		
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