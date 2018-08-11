using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour {

    public List<GameObject> smallPlatforms = new List<GameObject>();
    public List<GameObject> mediumPlatforms = new List<GameObject>();
    public List<GameObject> largePlatforms = new List<GameObject>();

    public List<GameObject> currentPlatforms = new List<GameObject>();

    [SerializeField]
    private float spawnRate = 2f;
    private float timeOfLastSpawn = 0f;

    private float spawnY = 5.65f;
    [SerializeField]
    private float platformSpeed = 0.5f;

    private float xMax = 8f;
    private float xMin = -5f;

    private float yMin = -6f;

    public Transform platformParent;

    public float chanceBig = 10f;
    public float chanceMedium = 5f;
    public float chanceSmall = 3f;

    private const float randomMax = 10f;

    private float ChanceBig { get { return (chanceBig / (chanceBig + chanceMedium + chanceSmall)) * randomMax; } }
    private float ChanceMedium { get { return (chanceMedium / (chanceBig + chanceMedium + chanceSmall)) * randomMax; } }
    private float ChanceSmall { get { return (chanceSmall / (chanceBig + chanceMedium + chanceSmall)) * randomMax; } }

    private void FixedUpdate()
    {
        List<GameObject> expiredPlatforms = new List<GameObject>();
        foreach(GameObject go in currentPlatforms)
        {
            if(IsOffScreen(go.transform))
            {
                Debug.Log("Platform Off Screen");
                expiredPlatforms.Add(go);
            }
            else
            {
                go.transform.position = Vector3.MoveTowards(go.transform.position, new Vector3(go.transform.position.x, yMin, 0f), Time.deltaTime * platformSpeed);
            }
        }

        // remove the platforms that are off screen
        foreach(GameObject go in expiredPlatforms)
        {
            currentPlatforms.Remove(go);
            Destroy(go);
        }

        if(Time.time > (timeOfLastSpawn + spawnRate))
        {
            SpawnPlatform();
        }
    }

    public bool IsOffScreen(Transform t)
    {
        if(t.localPosition.y < yMin)
        {
            return true;
        }

        return false;
    }

    public void SpawnPlatform()
    {
        float whichPlatformToSpawn = Random.Range(0f, randomMax);
        GameObject spawnedPlatform;
        if(whichPlatformToSpawn < ChanceBig)
        {
            //spawn big
            spawnedPlatform = Instantiate(largePlatforms[Random.Range(0, largePlatforms.Count)]);            
        }
        else if(whichPlatformToSpawn < (ChanceBig + ChanceMedium))
        {
            //spawn medium
            spawnedPlatform = Instantiate(mediumPlatforms[Random.Range(0, mediumPlatforms.Count)]);
        }
        else
        {
            //spawn small
            spawnedPlatform = Instantiate(smallPlatforms[Random.Range(0, smallPlatforms.Count)]);
        }

        spawnedPlatform.transform.position = new Vector3(Random.Range(xMin, xMax), spawnY, 0f);
        spawnedPlatform.transform.SetParent(platformParent);
        spawnedPlatform.transform.localScale = Vector3.one;

        timeOfLastSpawn = Time.time;
        currentPlatforms.Add(spawnedPlatform);
    }

}
