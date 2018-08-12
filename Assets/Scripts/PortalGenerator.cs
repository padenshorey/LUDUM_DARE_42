using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalGenerator : MonoBehaviour
{
    public GameObject portalBlack;
    public GameObject portalBlue;
    public GameObject portalRed;
    public GameObject portalAll;

    public List<GameObject> currentPortals = new List<GameObject>();

    [SerializeField]
    private float spawnRate = 2f;
    private float timeOfLastSpawn = 0f;

    private float xMax = 8f;
    private float xMin = -5f;

    private float yMax = 4.14f;
    private float yMin = -3.78f;

    public float minSpawnRate = 2f;
    public float maxSpawnRate = 4f;

    public Transform portalParent;

    public float chanceColor = 10f;
    public float chanceAll = 5f;
    public float chanceNone = 3f;

    private const float randomMax = 10f;

    private float ChanceColor { get { return (chanceColor / (chanceColor + chanceAll + chanceNone)) * randomMax; } }
    private float ChanceAll { get { return (chanceAll / (chanceColor + chanceAll + chanceNone)) * randomMax; } }
    private float ChanceNone { get { return (chanceNone / (chanceColor + chanceAll + chanceNone)) * randomMax; } }

    private bool canSpawnPortal = false;

    private void FixedUpdate()
    {
        if (Time.time > (timeOfLastSpawn + spawnRate) && canSpawnPortal)
        {
            SpawnPortal();
        }
    }

    public void StartPortalGenerator()
    {
        canSpawnPortal = true;
    }

    public void StopPortalGenerator()
    {
        canSpawnPortal = false;
    }

    public void WipePortals()
    {
        foreach (GameObject p in currentPortals)
        {
            Destroy(p);
        }

        currentPortals.Clear();
    }

    public void SpawnPortal()
    {
        float whichPortalToSpawn = Random.Range(0f, randomMax);
        GameObject spawnedPortal;
        if (whichPortalToSpawn < ChanceColor)
        {
            //spawn color
            float randomColor = Random.Range(0f, 1f);
            if (randomColor < 0.33f)
            {
                spawnedPortal = Instantiate(portalBlack);
            }
            else if(randomColor < 0.66f)
            {
                spawnedPortal = Instantiate(portalBlue);
            }
            else
            {
                spawnedPortal = Instantiate(portalRed);
            }
        }
        else if (whichPortalToSpawn < (ChanceColor + ChanceAll))
        {
            //spawn all
            spawnedPortal = Instantiate(portalAll);
        }
        else
        {
            //spawn none
            return;
        }

        spawnedPortal.transform.position = new Vector3(Random.Range(xMin, xMax), Random.Range(yMin, yMax), 0f);
        spawnedPortal.transform.SetParent(portalParent);
        spawnedPortal.transform.localScale = Vector3.one;
        spawnedPortal.GetComponent<Portal>().Setup(Random.Range(5f, 15f));

        float portalsPerMinute = 9f + (GameManager.instance.TimeSinceStartOfRound / 60);
        spawnRate = 60f / portalsPerMinute;
        //spawnRate += Random.Range(minSpawnRate, maxSpawnRate);

        timeOfLastSpawn = Time.time;
        currentPortals.Add(spawnedPortal);
    }

    public void KillPortal(Portal p)
    {
        currentPortals.Remove(p.gameObject);
    }
}
