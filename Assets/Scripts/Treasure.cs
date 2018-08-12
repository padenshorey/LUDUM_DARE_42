using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour {

    private AudioSource audioSource;
    public GameManager.ItemColor color;
    public int pointValue = 1;

    public bool explodingAlready = false;

    public float alarmStart;
    public bool onBoundry = false;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if(onBoundry)
        {
            if(Time.time > (alarmStart + GameManager.instance.timeToDie) && GameManager.instance.inGame)
            {
                // GAME OVER
                GameManager.instance.EndGame();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            audioSource.clip = AudioManager.instance.hits[Random.Range(0, AudioManager.instance.hits.Length)];
            audioSource.Play();
        }
    }

    public void CashIn()
    {
        GameManager.instance.AddPoints(pointValue, color);
        Spawner.instance.SpawnParticleEffect(transform.position, color);
        Destroy(gameObject);
    }

    public void Explode()
    {
        explodingAlready = true;
        Destroy(gameObject);
    }

    public void FixedUpdate()
    {
        // just in case they break through the barriers
        if(transform.position.y < -10f)
        {
            GameManager.instance.treasureGenerator.currentTreasure.Remove(this);
            Explode();
        }
    }
}
