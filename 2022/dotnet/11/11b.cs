using System.Text.RegularExpressions;

namespace aoc2022._11;

public static class AoC_2022_11b
{
	private static readonly Regex DigitRegex = new(@"\d+");
	private static readonly Regex SymbolRegex = new(@"(\*|\+)");
	private static readonly Regex OperandRegex = new(@"(\d+|(old)$)");

	public static void Answer()
	{
		var monkeyDescriptions = File.ReadAllLines("./11/input.txt").Chunk(7);
		var monkeys = monkeyDescriptions.Select(CreateMonkeyFromText).ToList();

		foreach (var _ in Enumerable.Range(1, 1000))
		{
			foreach (var monkey in monkeys)
			{
				foreach (var step in monkey.PlayRound())
				{
					monkeys[step.destination].EnqueueItem( step.worryLevel );	
				}	
			}
		}
		
		var topTwoMonkeys = monkeys.Select( m => m.TouchedItems )
			.OrderDescending().Take(2).ToList();
		
		Console.WriteLine( topTwoMonkeys.First() * topTwoMonkeys.Last() );
	}

	private static Monkey CreateMonkeyFromText(string[] description)
	{
		var startItemsAsString = DigitRegex.Matches(description[1]).Select(m => m.Value);
		var startItems = startItemsAsString.Select(ulong.Parse);
		
		var symbol = SymbolRegex.Match(description[2]).Value[0];
		var operand = OperandRegex.Match(description[2]).Value; 
		uint? operand2 = operand != "old" ? uint.Parse(operand) : null;
		
		var divisor = uint.Parse(DigitRegex.Match(description[3]).Value);
		var trueDest = int.Parse(DigitRegex.Match(description[4]).Value);
		var falseDest = int.Parse(DigitRegex.Match(description[5]).Value);

		return new Monkey(new Queue<ulong>(startItems), operand2, symbol, divisor, trueDest, falseDest);
	}
}

public class Monkey
{
	private readonly Queue<ulong> _items;

	private readonly Func<ulong, ulong> _calcFunc;

	private readonly TestOperation _operation;

	public int TouchedItems { get; private set; }

	public Monkey(Queue<ulong> items, uint? operand, char symbol, uint divisor, int trueDest, int falseDest)
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

	public void EnqueueItem(ulong worryLevel)
	{
		_items.Enqueue(worryLevel);
	}

	public IEnumerable<(int destination, ulong worryLevel)> PlayRound()
	{
		while (_items.Any())
		{
			var item = _items.Dequeue();
			TouchedItems++;
			var worryLevel = _calcFunc(item);
			var dest = _operation.ExecuteTestOp(worryLevel);
			yield return (dest, worryLevel);
		}
	}
}

public class TestOperation
{
	private readonly uint _divisor;

	private readonly int _trueDestination;

	private readonly int _falseDestination;

	public TestOperation(uint divisor, int trueDest, int falseDest)
	{
		_divisor = divisor;
		_trueDestination = trueDest;
		_falseDestination = falseDest;
	}

	public int ExecuteTestOp(ulong worryLevel) => worryLevel % _divisor == 0 ? _trueDestination : _falseDestination;
}