using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ceiling : MonoBehaviour
{
    public bool stuckInCeiling = false;
    public float timeStuckInCeiling = 0;
    private List<GameObject> treasureStuckInCeiling = new List<GameObject>();

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Treasure"))
        {
            if (!treasureStuckInCeiling.Contains(collision.transform.parent.gameObject))
            {
                treasureStuckInCeiling.Add(collision.transform.parent.gameObject);
                if (treasureStuckInCeiling.Count <= 1) timeStuckInCeiling = Time.time;
                stuckInCeiling = true;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.transform.CompareTag("Treasure"))
        {
            treasureStuckInCeiling.Remove(collision.transform.parent.gameObject);
            if (treasureStuckInCeiling.Count < 1)
            {
                stuckInCeiling = false;
            }
        }
    }

    private void Update()
    {
        if (stuckInCeiling)
        {
            if (Time.time > (timeStuckInCeiling + GameManager.instance.timeToDie))
            {
                // GAME OVER
                Debug.Log("<color=orange>YOU LOSE</color>");
                stuckInCeiling = false;
            }
        }
    }
}
