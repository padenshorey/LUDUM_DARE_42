using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public static Spawner instance = null;
    void Start()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    public GameObject landEffect;

    public void SpawnLandEffect(Vector3 position)
    {
        GameObject le = Instantiate(landEffect, position, Quaternion.identity);
        Destroy(le, 3f);
    }

    public GameObject particleBlack;
    public GameObject particleBlue;
    public GameObject particleRed;

    public void SpawnParticleEffect(Vector3 position, GameManager.ItemColor color)
    {
        Vector3 newSpawn = new Vector3(position.x, position.y, -1.5f);
        switch(color)
        {
            case GameManager.ItemColor.BLACK:
                Instantiate(particleBlack, newSpawn, Quaternion.identity);
                break;
            case GameManager.ItemColor.BLUE:
                Instantiate(particleBlue, newSpawn, Quaternion.identity);
                break;
            case GameManager.ItemColor.RED:
                Instantiate(particleRed, newSpawn, Quaternion.identity);
                break;
            case GameManager.ItemColor.MULTI:
                Instantiate(landEffect, newSpawn, Quaternion.identity);
                Instantiate(particleBlue, newSpawn, Quaternion.identity);
                Instantiate(particleRed, newSpawn, Quaternion.identity);
                break;
        }        
    }
}
