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
    }

    public void CashInTreasure(Treasure t)
    {
        currentTreasure.Remove(t);
        t.CashIn();
    }
}
