using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour {

    public GameManager.ItemColor portalColor;

    public float lifeSpan;
    private bool isDying = false;

    public AudioSource audioSource;

    public AudioClip portalAppear;
    public AudioClip portalHum;
    public AudioClip portalUse;
    public AudioClip portalDie;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(portalAppear);
    }

    public void Setup(float life)
    {
        lifeSpan = life;
        StartCoroutine(StartDeath());
    }

    IEnumerator StartDeath()
    {
        yield return new WaitForSeconds(lifeSpan);

        if (isDying) yield return null;

        GetComponent<Collider2D>().enabled = false;

        GetComponent<Animator>().SetTrigger("Die");

        audioSource.PlayOneShot(portalDie);

        GameManager.instance.portalGenerator.KillPortal(this);       
    }

    public void ActivateCollider()
    {
        GetComponent<Collider2D>().enabled = true;
    }

    public void Die()
    {
        Destroy(this.gameObject);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<PlayerController>().SetColor(portalColor);

            isDying = true;

            GetComponent<Collider2D>().enabled = false;

            GetComponent<Animator>().SetTrigger("Use");

            audioSource.PlayOneShot(portalUse);

            GameManager.instance.portalGenerator.KillPortal(this);
        }
    }
}
