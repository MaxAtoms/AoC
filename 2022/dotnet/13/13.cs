namespace aoc2022._13;

public static class AoC_2022_13
{
	public static void Answer()
	{
		var signalPairs = File.ReadAllLines("./13/input.txt")
			.Chunk(3)
			.Select(p => (p[0], p[1]));

		var isOrderCorrect = signalPairs.Select(ParseAndDetermineOrder);
		var sum = isOrderCorrect.Select((result, zeroBasedIndex) =>
			{
				var index = zeroBasedIndex + 1;
				return new { result, index };
			}
		).Where(r => r.result).Sum(r => r.index);
		Console.WriteLine(sum);
	}

	private static bool ParseAndDetermineOrder((string leftSignal, string rightSignal) signalPair)
	{
		var leftSignal = ParseSignal(signalPair.leftSignal[1..].ToCharArray());
		var rightSignal = ParseSignal(signalPair.rightSignal[1..].ToCharArray());
		return DetermineOrder(leftSignal.list, rightSignal.list) ?? false;
	}

	private static bool? DetermineOrder(object l, object r)
	{
		switch (l,r)
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