using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour {

    [SerializeField]
    private float lifespan;

    private void Awake()
    {
        Destroy(gameObject, lifespan);
    }
}
