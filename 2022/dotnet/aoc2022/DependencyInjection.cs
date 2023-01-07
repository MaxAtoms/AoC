using aoc2022._01;
using aoc2022._02;
using aoc2022._05;
using Microsoft.Extensions.DependencyInjection;

namespace aoc2022;

internal static class DependencyInjection
{
	internal static ServiceProvider BuildServiceProvider()
	{
		var collection = new ServiceCollection();
		collection.AddSingleton<IPuzzle, AoC_2022_1a>();
		collection.AddSingleton<IPuzzle, AoC_2022_1a>();

		collection.AddSingleton<IPuzzle, AoC_2022_2a>();
		collection.AddSingleton<IPuzzle, AoC_2022_2a>();

		collection.AddSingleton<IPuzzle, AoC_2022_5a>();
		collection.AddSingleton<IPuzzle, AoC_2022_5b>();

		return collection.BuildServiceProvider();
	}	
}