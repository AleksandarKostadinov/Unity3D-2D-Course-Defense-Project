using UnityEngine;
using System.Collections.Generic;
using System;
using Assets.Scripts;
using Random = UnityEngine.Random;
public class BourdManager : MonoBehaviour
{
    public int columns = 8;
    public int rows = 8;
    public Count wallCount = new Count(5, 9);
    public Count foodCount = new Count(1, 5);
    public GameObject exit;
    public GameObject[] floorTiles; 
    public GameObject[] foodTiles; 
    public GameObject[] wallTiles; 
    public GameObject[] outerWallTiles; 
    public GameObject[] enemyTiles;

    private Transform bourdHolder;
    private List<Vector3> gridPositions = new List<Vector3>();

    // Use this for initialization
    public void SetupScene(int level)
    {
        this.BourdSetup();
        this.InitListPositions();
        this.LayoutObjectAtRandom(this.wallTiles, this.wallCount.Minimum, this.wallCount.Maximum);
        this.LayoutObjectAtRandom(this.foodTiles, this.foodCount.Minimum, this.foodCount.Maximum);

        int enemyCount = (int) Mathf.Log(level, 2f);
        this.LayoutObjectAtRandom(this.enemyTiles, enemyCount, enemyCount);

        Instantiate(this.exit, new Vector3(this.columns - 1, this.rows - 1, 0f), Quaternion.identity);
    }

    private void BourdSetup()
    {
        this.bourdHolder = new GameObject("Bourd").transform;

        for (int x = -1; x < this.columns + 1; x++)
        {
            for (int y = -1; y < this.rows + 1; y++)
            {
                GameObject toInstantiat = this.floorTiles[Random.Range(0, this.floorTiles.Length)];
                if (this.InOuterWalls(x, y))
                {
                    toInstantiat = this.wallTiles[Random.Range(0, this.wallTiles.Length)];
                }

                GameObject instance =(GameObject) Instantiate(toInstantiat, new Vector3(x, y, 0f), Quaternion.identity);
                instance.transform.SetParent(this.bourdHolder);
            }
        }
    }

    private Vector3 GetRandomPosition()
    {
        int randomIndex =Random.Range(0, this.gridPositions.Count);

        Vector3 randomPosition = this.gridPositions[randomIndex];

        this.gridPositions.RemoveAt(randomIndex);

        return randomPosition;
    }

    private void LayoutObjectAtRandom(GameObject[] tileArray, int minimum , int maximum)
    {
        int objectCount = Random.Range(minimum, maximum + 1);

        for (int i = 0; i < objectCount; i++)
        {
            Vector3 randomPosition = this.GetRandomPosition();
            GameObject randomTile = tileArray[Random.Range(0, tileArray.Length)];
            Instantiate(randomTile, randomPosition, Quaternion.identity);
        }
    }

    private bool InOuterWalls(int x, int y)
    {
        return x == -1 || x == this.columns || y == -1 || y == this.rows;
    }

    private void InitListPositions()
    {
        this.gridPositions.Clear();

        for (int x = 1; x < this.columns - 1; x++)
        {
            for (int y = 1; y < this.rows - 1; y++)
            {
                this.gridPositions.Add(new Vector3(x, y, 0f));
            }
        }
    }
}
