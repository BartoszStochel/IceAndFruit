using System;
using System.Collections.Generic;
using UnityEngine;

public class TSVReader
{
	private readonly string[] lineSeparator = { "\n" };
	private readonly string[] valueSeparator = { "\t" };

	public List<List<string>> ReadText(TextAsset textAsset)
	{
		List<List<string>> linesOfCells = new List<List<string>>();
		List<string> lines = new List<string>(textAsset.text.Split(lineSeparator, StringSplitOptions.None));

		for (int i = 0; i < lines.Count; i++)
		{
			linesOfCells.Add(new List<string>(lines[i].Split(valueSeparator, StringSplitOptions.None)));
		}

		return linesOfCells;
	}
}
