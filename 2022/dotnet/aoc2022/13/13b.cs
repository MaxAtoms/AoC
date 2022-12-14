namespace aoc2022._13;

public static class AoC_2022_13b
{
	public static void Answer()
	{
		var signalStrings = File.ReadAllLines("./13/input.txt")
			.Where(p => p != "")
			.Append( "[[2]]" )
			.Append( "[[6]]");

		var signals = signalStrings.Select(p => new Signal(ParseSignal(p[1..].ToCharArray()).list, p));
		var orderedSignals = signals.OrderDescending().ToList();
		
		var relevantIndex1 = orderedSignals.Select((signal, index) => (signal, index)).First(x => x.signal.OriginalString == "[[2]]").index;
		var relevantIndex2 = orderedSignals.Select((signal, index) => (signal, index)).First(x => x.signal.OriginalString == "[[6]]").index;

		var result = (relevantIndex1 + 1) * (relevantIndex2 + 1);
		Console.WriteLine(result);
	}

	private static (List<object> list, int moveAhead) ParseSignal(char[] signal)
	{
		var list = new List<object>();

		for (var i = 0; i < signal.Length; i++)
		{
			if (signal[i] == '[')
			{
				var result = ParseSignal(signal[(i + 1)..]);
				i += result.moveAhead;
				list.Add(result.list);
			}
			else if (char.IsDigit(signal[i]))
			{
				int digit;

				// Dirty hack to account for the number '10' in input signal
				if (signal[i + 1] == '0')
				{
					digit = 10;
					i++;
				}
				else
				{
					digit = int.Parse(signal[i].ToString());
				}

				list.Add(digit);
			}
			else if (signal[i] == ']')
			{
				return (list, i + 1);
			}
		}

		return (list, 0);
	}
}

public class Signal : IComparable<Signal>
{
	public string OriginalString { get; }
	
	private readonly List<object> _signal;

	public Signal(List<object> signal, string originalString)
	{
		_signal = signal;
		OriginalString = originalString;
	}

	public int CompareTo(Signal? other)
	{
		return DetermineOrder(_signal, other._signal) == true ? 1 : -1;
	}

	private static bool? DetermineOrder(object l, object r)
	{
		switch (l, r)
		{
			// Both are numbers
			case { l: int left, r: int right } when left < right:
				return true;
			case { l: int left, r: int right } when left > right:
				return false;
			case { l: int, r: int }:
				return null;

			// One or both are lists, call recursively
			case { l: int left, r: List<object> right }:
			{
				return DetermineOrder(new List<object> { left }, right) ?? null;
			}
			case { l: List<object> left, r: int right }:
			{
				return DetermineOrder(left, new List<object> { right }) ?? null;
			}
			case { l: List<object> left, r: List<object> right }:
			{
				var i = 0;
				while (i < left.Count && i < right.Count)
				{
					var result = DetermineOrder(left[i], right[i]);
					if (result is not null)
					{
						return result;
					}

					i++;
				}

				// List exhausted
				return left.Count < right.Count ? true : left.Count > right.Count ? false : null;
			}
		}

		return null;
	}
}