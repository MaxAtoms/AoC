using System.Text.RegularExpressions;

namespace aoc2022._11;

// Use datatype long and modulo trick to keep numbers in check
// Without the modulo trick the numbers get so large that the program grinds to a halt after around 120 rounds using BigInteger
public static class AoC_2022_11b
{
	private static readonly Regex DigitRegex = new(@"\d+");
	private static readonly Regex SymbolRegex = new(@"(\*|\+)");
	private static readonly Regex OperandRegex = new(@"(\d+|(old)$)");
	
	private static int _moduloConstant = 1;

	// Answer 11b) 13954061248
	public static void Answer()
	{
		const int numberOfRounds = 10_000;
		
		var monkeyDescriptions = File.ReadAllLines("./11/input.txt").Chunk(7);
		var monkeys = monkeyDescriptions.Select(CreateMonkeyFromText).ToList();

		foreach (var _ in Enumerable.Range(1, numberOfRounds))
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

		Console.WriteLine((long)topTwoMonkeys.First() * topTwoMonkeys.Last());
	}

	private static Monkey CreateMonkeyFromText(string[] description)
	{
		var startItemsAsString = DigitRegex.Matches(description[1]).Select(m => m.Value);
		var startItems = startItemsAsString.Select(long.Parse);

		var symbol = SymbolRegex.Match(description[2]).Value[0];
		var operand = OperandRegex.Match(description[2]).Value;
		int? operand2 = operand != "old" ? int.Parse(operand) : null;

		var divisor = int.Parse(DigitRegex.Match(description[3]).Value);
		
		_moduloConstant *= divisor;
		
		var trueDest = int.Parse(DigitRegex.Match(description[4]).Value);
		var falseDest = int.Parse(DigitRegex.Match(description[5]).Value);

		return new Monkey(new Queue<long>(startItems), operand2, symbol, divisor, trueDest, falseDest);
	}

	private class Monkey
	{
		private readonly Queue<long> _items;

		private readonly Func<long, long> _calcFunc;

		private readonly TestOperation _operation;

		public int TouchedItems { get; private set; }

		public Monkey(Queue<long> items, int? operand, char symbol, int divisor, int trueDest, int falseDest)
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

		public void EnqueueItem(long worryLevel)
		{
			_items.Enqueue(worryLevel);
		}

		public IEnumerable<(int destination, long worryLevel)> PlayRound()
		{
			while (_items.Any())
			{
				var item = _items.Dequeue();
				TouchedItems++;
				var worryLevel = _calcFunc(item);
				
				var boredLevel = worryLevel % _moduloConstant;
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

		public int ExecuteTestOp(long worryLevel) =>
			worryLevel % _divisor == 0 ? _trueDestination : _falseDestination;
	}
}