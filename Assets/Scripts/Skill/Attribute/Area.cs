using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Area
{
	[SerializeField]
	private Vector2Int selfPosition;
	[SerializeField]
	private Vector2Int areaSize;
	[SerializeField]
	private bool[] rawArea;

	public bool[,] GetArea()
	{
		bool[,] area = new bool[areaSize.x, areaSize.y];

		for (int i = 0; i < rawArea.Length; i ++)
		{
			area[i / areaSize.x, i % areaSize.x] = rawArea[i];
		}

		return area;
	}
}
