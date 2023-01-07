namespace aoc2022._02;

public sealed class AoC_2022_2a : IPuzzle
{
	public int NumberOfDay => 2;
	
	public Part Part => Part.Part1;
	
	private enum Play
	{
		Rock,
		Paper,
		Scissors
	}

	private record Round(Play OpponentPlay, Play OwnPlay);

	public string SolvePuzzle(IEnumerable<string> inputLines)
	{
		var roundsAsLetters = inputLines.Select(s => s.Split(" ", 2))
			.Select(s => new { OpponentLetter = s[0], OwnLetter = s[1] });

		var rounds = roundsAsLetters.Select(r => new Round(
			MatchOpponentPlay(r.OpponentLetter),
			MatchOwnPlay(r.OwnLetter))
		);

		var roundScores = rounds.Select(GetRoundScore);
		
		return roundScores.Sum().ToString();
	}

	private static int GetRoundScore(Round round)
	{
		var playScore = round.OwnPlay switch 
		{
			Play.Rock => 1, 	
			Play.Paper => 2, 	
			Play.Scissors => 3, 	
		};

		var isDraw = round.OpponentPlay == round.OwnPlay;

		var resultScore = isDraw ? 3 : round switch
		{
			{ OpponentPlay: Play.Rock, OwnPlay: Play.Paper } => 6,
			{ OpponentPlay: Play.Rock, OwnPlay: Play.Scissors } => 0,
			
			{ OpponentPlay: Play.Paper, OwnPlay: Play.Rock } => 0,
			{ OpponentPlay: Play.Paper, OwnPlay: Play.Scissors } => 6,
			
			{ OpponentPlay: Play.Scissors, OwnPlay: Play.Rock } => 6,
			{ OpponentPlay: Play.Scissors, OwnPlay: Play.Paper } => 0
		};
		
		return playScore + resultScore;
	}

	private static Play MatchOpponentPlay(string playLetter) => playLetter switch
	{
		"A" => Play.Rock,
		"B" => Play.Paper,
		"C" => Play.Scissors,
		_ => throw new Exception($" Unknown opponent play: {playLetter}")
	};

	private static Play MatchOwnPlay(string playLetter) => playLetter switch
	{
		"X" => Play.Rock,
		"Y" => Play.Paper,
		"Z" => Play.Scissors,
		_ => throw new Exception($" Unknown own play: {playLetter}")
	};
}