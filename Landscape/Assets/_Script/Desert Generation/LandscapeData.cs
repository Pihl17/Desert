using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LandscapeData", menuName = "Configs/Landscape Data", order = 0)]
public class LandscapeData : ScriptableObject
{

	[Header("Tile")]
	[SerializeField] int[] tileSize = new int[2] { 10, 8 };
	public int[] TileSize { get { return tileSize; } }

	[SerializeField] float resolution = 0.5f;
	public float Resolution { get { return resolution; } }
	[SerializeField] float height = 1f;
	public float Height { get { return height; } }
	[SerializeField] float normalRes = 0.01f;
	public float NormalRes { get { return normalRes; } }

	[Header("X Axis")]
	[SerializeField] float sinFreq = 0.18f;
	public float SinFreq { get { return sinFreq; } }
	[Header("Z Axis")]
	[SerializeField] float cosFreq = 0.2f;
	public float CosFreq { get { return cosFreq; } }


	[Header("Procedural")]
	[SerializeField] float minDis = 50;
	public float MinDis { get { return minDis; } }
	[SerializeField] float maxDis = 55;
	public float MaxDis { get { return maxDis; } }

}
