using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleSpawner : MonoBehaviour
{
    public GameObject Collectible;
    public int SpawnAmount;
    public float SpawnAreaX;
    public float SpawnAreaY;

    GameObject[] spawnedCollectibles = new GameObject[0];

    public void Spawn()
    {
        spawnedCollectibles = new GameObject[SpawnAmount];

        for (int i = 0; i < SpawnAmount; i++)
        {
            Vector3 position = transform.position + new Vector3(
                Random.Range(-SpawnAreaX, SpawnAreaX),
                1,
                Random.Range(-SpawnAreaY, SpawnAreaY));

            spawnedCollectibles[i] = Instantiate(Collectible, position, Quaternion.Euler(45, UnityEngine.Random.Range(0, 360), 0));
        }
    }

    public void Despawn()
    {
        if (spawnedCollectibles.Length != 0)
        {
            for (int i = 0; i < spawnedCollectibles.Length; i++)
            {
                if (spawnedCollectibles[i] != null)
                {
                    Destroy(spawnedCollectibles[i]);
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(SpawnAreaX * 2, 1, SpawnAreaY * 2));
    }
}
