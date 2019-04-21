using UnityEngine;

[CreateAssetMenu]
public class Match3GridParams : ScriptableObject
{
	[Range(4, 10)] public int ROWS = 8;
	[Range(4, 10)] public int COLS = 8;
	[Range(.2f, .5f)] public float CELL_WIDTH = .5f;
	[Range(.025f, .5f)] public float fallSpeed = 0.25f;

}
