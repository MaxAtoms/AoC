using System.Text.RegularExpressions;

namespace aoc2022._11;

public static class AoC_2022_11a
{
	private static readonly Regex DigitRegex = new(@"\d+");
	private static readonly Regex SymbolRegex = new(@"(\*|\+)");
	private static readonly Regex OperandRegex = new(@"(\d+|(old)$)");

	// Answer 11a) 56350
	public static void Answer()
	{
		var monkeyDescriptions = File.ReadAllLines("./11/input.txt").Chunk(7);
		var monkeys = monkeyDescriptions.Select(CreateMonkeyFromText).ToList();

		foreach (var _ in Enumerable.Range(1, 20))
		{
			foreach (var monkey in monkeys)
			{
				foreach (var step in monkey.PlayRound())
				{
					monkeys[step.destination].EnqueueItem(step.worryLevel);
				}
			}
		}

		var topTwoMonkeys = monkeys.Select(m => m.TouchedItems)
			.OrderDescending().Take(2).ToList();

		Console.WriteLine(topTwoMonkeys.First() * topTwoMonkeys.Last());
	}

	private static Monkey CreateMonkeyFromText(string[] description)
	{
		var startItemsAsString = DigitRegex.Matches(description[1]).Select(m => m.Value);
		var startItems = startItemsAsString.Select(int.Parse);

		var symbol = SymbolRegex.Match(description[2]).Value[0];
		var operand = OperandRegex.Match(description[2]).Value;
		int? operand2 = operand != "old" ? int.Parse(operand) : null;

		var divisor = int.Parse(DigitRegex.Match(description[3]).Value);
		var trueDest = int.Parse(DigitRegex.Match(description[4]).Value);
		var falseDest = int.Parse(DigitRegex.Match(description[5]).Value);

		return new Monkey(new Queue<int>(startItems), operand2, symbol, divisor, trueDest, falseDest);
	}

	private class Monkey
	{
		private readonly Queue<int> _items;

		private readonly Func<int, int> _calcFunc;

		private readonly TestOperation _operation;

		public int TouchedItems { get; private set; }

		public Monkey(Queue<int> items, int? operand, char symbol, int divisor, int trueDest, int falseDest)
		{
			_items = items;
			_operation = new TestOperation(divisor, trueDest, falseDest);

			if (operand.HasValue)
			{
				if (symbol == '*')
				{
					_calcFunc = i => i * operand.Value;
				}
				else
				{
					_calcFunc = i => i + operand.Value;
				}
			}
			else
			{
				if (symbol == '*')
				{
					_calcFunc = i => i * i;
				}
				else
				{
					_calcFunc = i => i + i;
				}
			}
		}

		public void EnqueueItem(int worryLevel)
		{
			_items.Enqueue(worryLevel);
		}

		public IEnumerable<(int destination, int worryLevel)> PlayRound()
		{
			while (_items.Any())
			{
				var item = _items.Dequeue();
				TouchedItems++;
				var worryLevel = _calcFunc(item);
				var boredLevel = worryLevel / 3;
				var dest = _operation.ExecuteTestOp(boredLevel);
				yield return (dest, boredLevel);
			}
		}
	}

	private class TestOperation
	{
		private readonly int _divisor;

		private readonly int _trueDestination;

		private readonly int _falseDestination;

		public TestOperation(int divisor, int trueDest, int falseDest)
		{
			_divisor = divisor;
			_trueDestination = trueDest;
			_falseDestination = falseDest;
		}

		public int ExecuteTestOp(int worryLevel) =>
			worryLevel % _divisor == 0 ? _trueDestination : _falseDestination;
	}
}