using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkManager : MonoBehaviour
{
    [Header("Element")]
    [SerializeField] private Chunk[] chunkPrefabs;
    [SerializeField] private Chunk[] chunkLevels;
    // Start is called before the first frame update
    void Start()
    {
        CreateRandomLevels();
    }

    private void CreateOrderedLevels()
    {
        Vector3 chunkPosition = Vector3.zero;
        for (int i = 0; i < chunkLevels.Length; i++)
        {
            Chunk chunkToCreate = chunkLevels[i];

            if (i > 0)
                chunkPosition.z += chunkToCreate.GetLenght() / 2;

            Chunk chunkInstance = Instantiate(chunkToCreate, chunkPosition, Quaternion.identity, transform);
            chunkPosition.z += chunkInstance.GetLenght() / 2;
        }
    }

    private void CreateRandomLevels()
    {
        Vector3 chunkPosition = Vector3.zero;
        for (int i = 0; i < chunkPrefabs.Length; i++)
        {
            Chunk chunkToCreate = chunkPrefabs[Random.Range(0, chunkPrefabs.Length)];

            if (i > 0)
                chunkPosition.z += chunkToCreate.GetLenght() / 2;

            Chunk chunkInstance = Instantiate(chunkToCreate, chunkPosition, Quaternion.identity, transform);
            chunkPosition.z += chunkInstance.GetLenght() / 2;
        }

        Chunk chunkToFinish = chunkLevels[chunkLevels.Length - 1];
        chunkPosition.z += chunkToFinish.GetLenght() / 2;
        Instantiate(chunkToFinish, chunkPosition, Quaternion.identity, transform);
    }
}
