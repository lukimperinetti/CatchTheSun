using UnityEngine;

/// <summary>
/// Create a menu ChunkData on the rightclick in unity
/// </summary>
[CreateAssetMenu(fileName = "ChunkData", menuName = "Datas/ChunkData")]


public class ChunkData : ScriptableObject
{
    public GameObject prefab;
    public float spawnRate = 1.0f;
}
