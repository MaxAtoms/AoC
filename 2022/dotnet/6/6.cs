namespace aoc2022._6;

internal static class AoC_2022_6
{
	// Answer 6a) windowSize 4 -> 1361
	// Answer 6b) windowsSize 14 -> 3263
	public static void Answer()
	{
		const byte windowSize = 4;
		var signal = File.ReadAllLines("./6/input.txt")[0];

		for (var i = 0; i < signal.Length - windowSize; i++)
		{
			var (start, advance) = CheckWindow(windowSize, signal, i);
			if (!start)
			{
				// move the cursor past the
				// first duplicate character of the current window
				i += advance;
				continue;
			}

			Console.WriteLine(i + windowSize);
			break;
		}
	}

	private static (bool isStart, int advanceCursor) CheckWindow(byte windowSize, string signal, int i)
	{
		for (var j = 0; j < windowSize; j++)
		{
			for (var k = j + 1; k < windowSize; k++)
			{
				// stop if there is a duplication in window
				if (signal[i + j] == signal[i + k])
				{
					return (false, j);
				}
			}
		}

		return (true, -1);
	}
}