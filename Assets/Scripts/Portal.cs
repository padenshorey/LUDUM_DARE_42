using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour {

    public GameManager.ItemColor portalColor;

    public float lifeSpan;
    private bool isDying = false;

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

            GameManager.instance.portalGenerator.KillPortal(this);
        }
    }
}
