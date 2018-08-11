using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureGenerator : MonoBehaviour
{

    public List<GameObject> redTreasure = new List<GameObject>();
    public List<GameObject> blueTreasure = new List<GameObject>();
    public List<GameObject> blackTreasure = new List<GameObject>();

    public List<Treasure> currentTreasure = new List<Treasure>();

    public bool canSpawnTreasure = false;

    [SerializeField]
    private float spawnRate = 2f;
    private float timeOfLastSpawn = 0f;

    private float spawnY = 5.65f;

    private float xMax = 8f;
    private float xMin = -5f;

    public Transform treasureParent;

    public float chanceBlack = 10f;
    public float chanceBlue = 5f;
    public float chanceRed = 3f;

    private const float randomMax = 10f;

    private float ChanceBlack { get { return (chanceBlack / (chanceBlack + chanceBlue + chanceRed)) * randomMax; } }
    private float ChanceBlue { get { return (chanceBlue / (chanceBlack + chanceBlue + chanceRed)) * randomMax; } }
    private float ChanceRed { get { return (chanceRed / (chanceBlack + chanceBlue + chanceRed)) * randomMax; } }

    public void StartTreasureGenerator()
    {
        canSpawnTreasure = true;
    }

    public void StopTreasureGenerator()
    {
        canSpawnTreasure = false;
    }

    private void FixedUpdate()
    {
        if (Time.time > (timeOfLastSpawn + spawnRate) && canSpawnTreasure)
        {
            SpawnTreasure();
        }
    }

    private int GetSpriteIndex(GameManager.ItemColor color, Sprite sprite)
    {
        int index = 0;
        switch(color)
        {
            case GameManager.ItemColor.BLACK:
                for(int i=0; i < blackTreasure.Count; i++)
                {
                    if(blackTreasure[i].GetComponent<SpriteRenderer>().sprite == sprite)
                    {
                        index = i;
                        return index;
                    }
                }
                break;
            case GameManager.ItemColor.BLUE:
                for (int i = 0; i < blueTreasure.Count; i++)
                {
                    if (blueTreasure[i].GetComponent<SpriteRenderer>().sprite == sprite)
                    {
                        index = i;
                        return index;
                    }
                }
                break;
            case GameManager.ItemColor.RED:
                for (int i = 0; i < redTreasure.Count; i++)
                {
                    if (redTreasure[i].GetComponent<SpriteRenderer>().sprite == sprite)
                    {
                        index = i;
                        return index;
                    }
                }
                break;
        }

        return index;
    }

    public void ExplodeTreasure(Treasure treasure, int pieces, Vector3 playerPosition)
    {
        if((treasure.transform.localScale.x / (float)pieces) + 0.1f < 0.5f)
        {
            // if the pieces will be too small, don't explode
            return;
        }

        for (int i = 0; i < 2; i++)
        {
            GameObject spawnedTreasure;
            switch (treasure.color)
            {
                case GameManager.ItemColor.BLACK:
                    spawnedTreasure = Instantiate(blackTreasure[GetSpriteIndex(GameManager.ItemColor.BLACK, treasure.GetComponent<SpriteRenderer>().sprite)]);
                    break;
                case GameManager.ItemColor.BLUE:
                    spawnedTreasure = Instantiate(blueTreasure[GetSpriteIndex(GameManager.ItemColor.BLUE, treasure.GetComponent<SpriteRenderer>().sprite)]);
                    break;
                case GameManager.ItemColor.RED:
                    spawnedTreasure = Instantiate(redTreasure[GetSpriteIndex(GameManager.ItemColor.RED, treasure.GetComponent<SpriteRenderer>().sprite)]);
                    break;
                default:
                    spawnedTreasure = null;
                    break;
            }

            spawnedTreasure.transform.position = new Vector3(treasure.transform.position.x + (i-0.25f), treasure.transform.position.y + (i - 0.25f), treasure.transform.position.z);
            spawnedTreasure.transform.SetParent(treasureParent);

            float scale = (treasure.transform.localScale.x / (float)pieces) + 0.1f;
            spawnedTreasure.transform.localScale = new Vector3(scale, scale, scale);

            currentTreasure.Add(spawnedTreasure.GetComponent<Treasure>());
            spawnedTreasure.GetComponent<Rigidbody2D>().AddExplosionForce(500f, playerPosition, 20f);
        }

        currentTreasure.Remove(treasure);
        treasure.Explode();
    }

    public void SpawnTreasure()
    {
        float whichTreasureToSpawn = Random.Range(0f, randomMax);
        GameObject spawnedTreasure;
        if (whichTreasureToSpawn < ChanceBlack)
        {
            spawnedTreasure = Instantiate(blackTreasure[Random.Range(0, blackTreasure.Count)]);
        }
        else if (whichTreasureToSpawn < (ChanceBlack + ChanceBlue))
        {
            spawnedTreasure = Instantiate(blueTreasure[Random.Range(0, blueTreasure.Count)]);
        }
        else
        {
            spawnedTreasure = Instantiate(redTreasure[Random.Range(0, redTreasure.Count)]);
        }

        spawnedTreasure.transform.position = new Vector3(Random.Range(xMin, xMax), spawnY, 0f);
        spawnedTreasure.transform.SetParent(treasureParent);

        float randomScale = Random.Range(0.8f, 1f + (Time.time / 100f));
        spawnedTreasure.transform.localScale = new Vector3(randomScale, randomScale, randomScale);

        timeOfLastSpawn = Time.time;
        currentTreasure.Add(spawnedTreasure.GetComponent<Treasure>());

        float treasurePerSecond = 0.25f + (Time.time / 60);
        spawnRate = 1f / treasurePerSecond;
    }

    public void CashInTreasure(Treasure t)
    {
        currentTreasure.Remove(t);
        t.CashIn();
    }
}
