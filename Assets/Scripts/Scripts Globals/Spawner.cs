using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform chunksContainer;
    [SerializeField] private List<ChunkData> chunks;
    private int lastChunkID = -1;

    /// <summary>
    /// Called when an object exits the trigger collider attached to this game object.
    /// </summary>
    /// <param name="other">The collider of the object that exited the trigger.</param>
    private void OnTriggerExit(Collider other)
    {
        Transform lastChunkEnd = other.gameObject.transform.Find("End");
        if (lastChunkEnd != null)
        {
            Instantiate(PickRandomChunk(), lastChunkEnd.position, Quaternion.identity, chunksContainer);
        }
        else
        {
            if (other.tag != "asteroid")
            {
                Debug.LogWarning("No 'End' transform found on " + other.gameObject.name);
            }
        }
    }


    /// <summary>
    /// Picks a random chunk based on their spawn rates and returns its prefab.
    /// </summary>
    /// <returns>The prefab of the randomly picked chunk.</returns>
    private GameObject PickRandomChunk()
    {
        float totalSpawnRate = 0;
        foreach (var chunk in chunks)
            totalSpawnRate += chunk.spawnRate;

        float randomValue = Random.Range(0f, totalSpawnRate);
        float cumulative = 0f;

        for (int i = 0; i < chunks.Count; i++)
        {
            cumulative += chunks[i].spawnRate;

            if (randomValue <= cumulative)
            {
                if (i == lastChunkID)
                    return PickRandomChunk();
                lastChunkID = i;
                return chunks[i].prefab;
            }
        }
        return chunks[0].prefab;
    }
}