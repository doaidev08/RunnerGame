using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> tilesPrefabs;
    [SerializeField] private List<GameObject> activeTiles;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private int lengthOfTile = 30;
    [SerializeField] private int totalOfTiles = 8;
    [SerializeField] private int numberTileSpawn = 3;
    [SerializeField] private int zSpawn = 0;

    

    // Start is called before the first frame update
    void Start()
    {
        activeTiles = new List<GameObject>();
        for (int i = 0; i < numberTileSpawn; i++)
        {
            if (i == 0)
            {
                SpawnTiles(1);
            }
            else
            {
                /* SpawnTiles(Random.Range(0, totalOfTiles));*/
                SpawnTiles((1));
            }
        }
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTransform.position.z - 30 >= zSpawn - (numberTileSpawn * lengthOfTile))
        {
            /*  SpawnTiles(Random.Range(0, totalOfTiles));*/
            SpawnTiles(1);
            DeleteTiles();
        }
    }
    //Spawn Tiles
    private void SpawnTiles(int index)
    {
        GameObject tileInit = Instantiate(tilesPrefabs[index], transform.forward * zSpawn, transform.rotation);
        activeTiles.Add(tileInit);
        zSpawn += lengthOfTile;
        
    }
    //Delete Tiles
    private void DeleteTiles()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);

    }


}
