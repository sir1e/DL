using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteGroundGenerator : MonoBehaviour
{
    public GameObject groundTilePrefab;
    public int initialGroundTiles = 10;
    public float tileLength = 1f;
    public Transform playerTransform;

    private List<GameObject> groundTiles = new List<GameObject>();
    private float spawnZ = 0f;

    void Start()
    {
        for (int i = 0; i < initialGroundTiles; i++)
        {
            SpawnGroundTile();
        }
    }

    void Update()
    {
        if (playerTransform.position.z > (spawnZ - initialGroundTiles * tileLength))
        {
            SpawnGroundTile();
            DeleteGroundTile();
        }
    }

    void SpawnGroundTile()
    {
        GameObject tile = Instantiate(groundTilePrefab, transform.forward * spawnZ, transform.rotation);
        groundTiles.Add(tile);
        spawnZ += tileLength;
    }

    void DeleteGroundTile()
    {
        Destroy(groundTiles[0]);
        groundTiles.RemoveAt(0);
    }
}
