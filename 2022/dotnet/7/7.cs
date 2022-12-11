namespace aoc2022._7;

internal static class AoC_2022_7
{
	// Answer 7a) 1454188
	// Answer 7b) 4183246
	public static void Answer()
	{
		var terminalOutput = File.ReadAllLines("./7/input.txt");
		var rootDir = Interpreter.Interpret(terminalOutput);
		DetermineDirectorySizes(rootDir);
		var dirList = CollectDirectories(rootDir).ToList();
		
		var resultList = dirList.Where(d => d.Size <= 100_000)
			.Select(d => d.Size);
		
		Console.WriteLine( "Answer 7a) " + resultList.Sum());

		const int totalSpace = 70_000_000; 
		const int requiredUpdateSpace = 30_000_000;
		var usedSpace = rootDir.Size;
		var freeSpace = totalSpace - usedSpace;
		var missingSpace = requiredUpdateSpace - freeSpace;
		
		var smallestDir = dirList.Where(d => d.Size > missingSpace).OrderBy( d => d.Size ).First().Size;

		Console.WriteLine( "Answer 7b) " + smallestDir);
	}

	private static IEnumerable<DirectoryNode> CollectDirectories(DirectoryNode dir)
	{
		var directoryList = new List<DirectoryNode>();

		foreach (var childDir in dir.ChildDirs)
		{
			directoryList.AddRange(CollectDirectories(childDir));
			directoryList.Add(childDir);
		}

		return directoryList;
	}

	private static void DetermineDirectorySizes(DirectoryNode dir)
	{
		foreach (var childDir in dir.ChildDirs)
		{
			DetermineDirectorySizes(childDir);
			dir.Size += childDir.Size;
		}

		dir.Size += dir.Files.Aggregate(0, (acc, f) => acc + f.Size);
	}

}

internal static class Interpreter
{
	public static DirectoryNode Interpret(string[] terminalOutput)
	{
		DirectoryNode rootDir = null;
		DirectoryNode currentDir = null;

		for (var i = 0; i < terminalOutput.Length; i++)
		{
			var currentCommand = ParseCommand(terminalOutput[i]);

			switch (currentCommand)
			{
				case CdCommand command:
					currentDir = InterpretCdCommand(command, currentDir);
					if (command.arg == "/")
					{
						rootDir = currentDir;
					}
					break;

				case LsCommand:
					var lsCommandOutput = new List<string>();
					for (i += 1; i < terminalOutput.Length; i++)
					{
						if (terminalOutput[i].StartsWith("$ "))
						{
							i--;
							break;
						}
						
						lsCommandOutput.Add( terminalOutput[i] );
					}
					InterpretLsCommand(lsCommandOutput, currentDir);
					break;
			}
		}

		return rootDir;
	}

	private static void InterpretLsCommand(IReadOnlyList<string> terminalOutput, DirectoryNode? currentDir)
	{
		foreach (var line in terminalOutput)
		{
			if (line.StartsWith("dir "))
			{
				var dirName = line.Replace("dir ", "");
				var dirNodeLs = new DirectoryNode(dirName, currentDir, new List<DirectoryNode>(),
					new List<FileNode>());
				currentDir.ChildDirs.Add(dirNodeLs);
			}
			else
			{
				var fileOutput = line.Split(' ');
				var file = new FileNode(fileOutput[1], int.Parse(fileOutput[0]));
				currentDir.Files.Add(file);
			}
		}
	}

	private static DirectoryNode InterpretCdCommand(CdCommand command, DirectoryNode? currentDir )
	{
		if (command.arg == "..")
		{
			currentDir = currentDir.ParentDir;
			return currentDir;
		}

		var existingDir = currentDir?.ChildDirs.FirstOrDefault(c => c.Name == command.arg);
		currentDir = existingDir ?? new DirectoryNode(command.arg, currentDir, new List<DirectoryNode>(),
			new List<FileNode>());

		return currentDir;
	}

	private static Command ParseCommand(string s)
	{
		if (!s.StartsWith("$ "))
		{
			throw new Exception("Could not interpret command");
		}

		var tokens = s.Split(' ');

		return tokens[1] switch
		{
			"cd" => new CdCommand(tokens[2]),
			"ls" => new LsCommand(),
			_ => throw new Exception($"Unknown command: {tokens[1]}")
		};
	}

	private record Command;

	private record CdCommand(string arg) : Command;

	private record LsCommand : Command;
}


internal record FileSystemNode(string Name);

internal record FileNode(string Name, int Size) : FileSystemNode(Name);

internal record DirectoryNode
	(string Name, DirectoryNode ParentDir, List<DirectoryNode> ChildDirs, List<FileNode> Files) : FileSystemNode(Name)

{
	public int Size { get; set; }
}