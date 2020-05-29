using UnityEngine;
using System;
using System.Collections.Generic;

public static class ArrayExtensions
{
	public static List<T> GetOneLineAsList<T>(this T[,] array, int dimension, int indexOfLine)
	{
		try
		{
			int arrayLengthInDimension = array.GetLength(dimension);
			List<T> line = new List<T>(arrayLengthInDimension);

			for (int i = 0; i < arrayLengthInDimension; i++)
			{
				line.Add(array[dimension == 0 ? i : indexOfLine, dimension == 0 ? indexOfLine : i]);
			}

			return line;
		}
		catch(Exception e)
		{
			Debug.LogError($"Exception occured in GetOneLineAsList method: {e}. Returning empty list.");
			return new List<T>();
		}
	}

	public static List<T> TransformToSingleList<T>(this T[,] array)
	{
		try
		{
			List<T> list = new List<T>(array.Length);

			for (int i = 0; i < array.GetLength(0); i++)
			{
				for (int j = 0; j < array.GetLength(1); j++)
				{
					list.Add(array[i, j]);
				}
			}

			return list;
		}
		catch (Exception e)
		{
			Debug.LogError($"Exception occured in TransformToSingleList method: {e}. Returning empty list.");
			return new List<T>();
		}
	}
}
