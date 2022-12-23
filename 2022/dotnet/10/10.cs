namespace aoc2022._10;

public static class AoC_2022_10
{
	private static readonly List<int> RelevantRegisterValues = new();
	
	private static readonly List<bool> ScreenContent = new();

	private static void c_StateChange(object sender, Cpu.CpuStateChangeEventArgs args )
	{
		var relevantCycles = new[] { 20, 60, 100, 140, 180, 220 };

		if (relevantCycles.Contains(args.CycleCount))
		{
			RelevantRegisterValues.Add(args.RegisterX * args.CycleCount);
		}
	}

	private static void c_StateChange2(object sender, Cpu.CpuStateChangeEventArgs args)
	{
		var posInLine = args.CycleCount % 40;
		
		if ( args.RegisterX <= posInLine && args.RegisterX + 2 >= posInLine )
		{
			ScreenContent.Add( true );	
		}
		else
		{
			ScreenContent.Add( false );
		}
	}

	public static void Answer()
	{
		var instructions = File.ReadAllLines("./10/input.txt");
		var cpu = new Cpu(registerXInitValue: 1);
		cpu.StateChangeEvent += c_StateChange;
		cpu.StateChangeEvent += c_StateChange2;

		foreach (var instruction in instructions)
		{
			cpu.InterpretInstruction(instruction);
		}

		Console.WriteLine(RelevantRegisterValues.Sum());

		var pixels = ScreenContent.Select(p => p ? '#' : '.');
		var lines = pixels.Chunk(40);

		foreach (var line in lines)
		{
			Console.WriteLine( line );	
		}
	}
}

public class Cpu
{
	private int _registerX;
	private int _cycleCount;

	public Cpu(int registerXInitValue)
	{
		_registerX = registerXInitValue;
	}

	public event EventHandler<CpuStateChangeEventArgs> StateChangeEvent;

	private void IncrementCycles()
	{
		_cycleCount++;
		StateChangeEvent(this, new CpuStateChangeEventArgs(_cycleCount, _registerX));
	}

	public void InterpretInstruction(string instruction)
	{
		if (instruction == "noop")
		{
			IncrementCycles();
		}
		else
		{
			var argument = int.Parse(instruction.Split(' ')[1]);
			IncrementCycles();
			IncrementCycles();
			_registerX += argument;
		}
	}

	public class CpuStateChangeEventArgs : EventArgs
	{
		public CpuStateChangeEventArgs(int cycleCount, int registerX)
		{
			CycleCount = cycleCount;
			RegisterX = registerX;
		}

		public int CycleCount { get; }
		public int RegisterX { get; }
	}
}