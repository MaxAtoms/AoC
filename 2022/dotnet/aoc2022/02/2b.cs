namespace aoc2022._02;

public class AoC_2022_2b : IChallenge
{
	public string CalculatePuzzleSolution()
	{
		var fileLines = File.ReadLines("./02/input.txt");
		return SolvePuzzle( fileLines );
	}
	
	private enum Play
	{
		Rock,
		Paper,
		Scissors
	}

	private enum DesiredResult
	{
		Loose,
		Draw,
		Win
	}

	private record Round(Play OpponentPlay, DesiredResult result);

	public string SolvePuzzle(IEnumerable<string> inputLines)
	{
		var roundsAsLetters = inputLines.Select(s => s.Split(" ", 2))
			.Select(s => new { OpponentLetter = s[0], OwnLetter = s[1] });

		var rounds = roundsAsLetters.Select(r => new Round(
			MatchOpponentPlay(r.OpponentLetter),
			MatchDesiredResult(r.OwnLetter))
		);

		var roundScores = rounds.Select(GetRoundScore);
		
		return roundScores.Sum().ToString();
	}

	private static int GetRoundScore(Round round)
	{
		var play = round switch
		{
			{ OpponentPlay: Play.Rock, result: DesiredResult.Loose } => Play.Scissors,	
			{ OpponentPlay: Play.Rock, result: DesiredResult.Win } => Play.Paper,	
			
			{ OpponentPlay: Play.Paper, result: DesiredResult.Loose } => Play.Rock,	
			{ OpponentPlay: Play.Paper, result: DesiredResult.Win } => Play.Scissors,	
			
			{ OpponentPlay: Play.Scissors, result: DesiredResult.Loose } => Play.Paper,	
			{ OpponentPlay: Play.Scissors, result: DesiredResult.Win } => Play.Rock,	
			
			{ result: DesiredResult.Draw } => round.OpponentPlay	
		};
		
		var playScore = play switch 
		{
			Play.Rock => 1, 	
			Play.Paper => 2, 	
			Play.Scissors => 3, 	
		};

		var resultScore = round.result switch
		{
			DesiredResult.Loose => 0,
			DesiredResult.Draw => 3,
			DesiredResult.Win => 6,
		};
		
		return playScore + resultScore;
	}

	private static Play MatchOpponentPlay(string letter) => letter switch
	{
		"A" => Play.Rock,
		"B" => Play.Paper,
		"C" => Play.Scissors,
		_ => throw new Exception($" Unknown opponent play: {letter}")
	};

	private static DesiredResult MatchDesiredResult(string letter) => letter switch
	{
		"X" => DesiredResult.Loose,
		"Y" => DesiredResult.Draw,
		"Z" => DesiredResult.Win,
		_ => throw new Exception($" Unknown desired result: {letter}")
	};
}