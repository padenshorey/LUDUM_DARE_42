using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour {

    private AudioSource audioSource;
    public GameManager.ItemColor color;
    public int pointValue = 1;

    public bool explodingAlready = false;


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        audioSource.clip = AudioManager.instance.hits[Random.Range(0, AudioManager.instance.hits.Length)];

        if(Random.Range(0f, 1f) < 0.1f)
            audioSource.Play();
    }

    public void CashIn()
    {
        GameManager.instance.AddPoints(pointValue);
        Destroy(gameObject);
    }

    public void Explode()
    {
        explodingAlready = true;
        Destroy(gameObject);
    }
}
