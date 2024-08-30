using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkManager : MonoBehaviour
{
    public static ChunkManager instance;

    [Header("Element")]
    [SerializeField] private LevelSO[] levels;
    // [SerializeField] private Chunk[] chunkPrefabs;
    // [SerializeField] private Chunk[] chunkLevels;
    private GameObject finishLine;

    void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        GenerateLevels();
        finishLine = GameObject.FindWithTag("Finish");
    }

    private void GenerateLevels()
    {
        int runnerLevel = GetLevels();
        runnerLevel %= levels.Length;
        LevelSO level = levels[runnerLevel];

        CreatLevels(level.chunks);
    }

    private void CreatLevels(Chunk[] _levelChunks)
    {
        Vector3 chunkPosition = Vector3.zero;
        for (int i = 0; i < _levelChunks.Length; i++)
        {
            Chunk chunkToCreate = _levelChunks[i];

            if (i > 0)
                chunkPosition.z += chunkToCreate.GetLenght() / 2;

            Chunk chunkInstance = Instantiate(chunkToCreate, chunkPosition, Quaternion.identity, transform);
            chunkPosition.z += chunkInstance.GetLenght() / 2;
        }
    }

    // private void CreateOrderedLevels()
    // {
    //     Vector3 chunkPosition = Vector3.zero;
    //     for (int i = 0; i < chunkLevels.Length; i++)
    //     {
    //         Chunk chunkToCreate = chunkLevels[i];

    //         if (i > 0)
    //             chunkPosition.z += chunkToCreate.GetLenght() / 2;

    //         Chunk chunkInstance = Instantiate(chunkToCreate, chunkPosition, Quaternion.identity, transform);
    //         chunkPosition.z += chunkInstance.GetLenght() / 2;
    //     }
    // }

    // private void CreateRandomLevels()
    // {
    //     Vector3 chunkPosition = Vector3.zero;
    //     for (int i = 0; i < chunkPrefabs.Length; i++)
    //     {
    //         Chunk chunkToCreate = chunkPrefabs[Random.Range(0, chunkPrefabs.Length)];

    //         if (i > 0)
    //             chunkPosition.z += chunkToCreate.GetLenght() / 2;

    //         Chunk chunkInstance = Instantiate(chunkToCreate, chunkPosition, Quaternion.identity, transform);
    //         chunkPosition.z += chunkInstance.GetLenght() / 2;
    //     }

    //     Chunk chunkToFinish = chunkLevels[chunkLevels.Length - 1];
    //     chunkPosition.z += chunkToFinish.GetLenght() / 2;
    //     Instantiate(chunkToFinish, chunkPosition, Quaternion.identity, transform);
    // }

    public float GetFinishLinePosZ()
    {
        return finishLine.transform.position.z;
    }

    public int GetLevels()
    {
        return PlayerPrefs.GetInt(PlayerPrefsTag.LEVEL, 0);
    }
}
