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
}
