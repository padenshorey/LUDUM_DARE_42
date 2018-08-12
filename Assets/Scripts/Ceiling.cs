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
            if (!treasureStuckInCeiling.Contains(collision.rigidbody.gameObject))
            {
                // new treasure
                collision.gameObject.GetComponent<Treasure>().alarmStart = Time.time;
                collision.gameObject.GetComponent<Treasure>().onBoundry = true;

                treasureStuckInCeiling.Add(collision.rigidbody.gameObject);
                /*
                if (treasureStuckInCeiling.Count <= 1)
                {
                    //Debug.Log("Resetting Timer Stuck");
                    timeStuckInCeiling = Time.time;
                }
                stuckInCeiling = true;
                */
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.rigidbody.gameObject.CompareTag("Treasure"))
        {
            collision.gameObject.GetComponent<Treasure>().onBoundry = false;
            collision.gameObject.GetComponent<Treasure>().isStuck = false;

            treasureStuckInCeiling.Remove(collision.rigidbody.gameObject);

            if(!AnyTreasureStuckInCeiling())
            {
                GameManager.instance.mainCamera.GetComponent<Animator>().SetBool("Shaking", false);
                GameManager.instance.mainCamera.GetComponent<AudioSource>().Pause();
                //AudioManager.instance.PlayFasterSong(false);
            }
        }
    }

    public bool AnyTreasureStuckInCeiling()
    {
        List<GameObject> deadTreasure = new List<GameObject>();
        bool whatToReturn = false;
        foreach(GameObject go in treasureStuckInCeiling)
        {
            if (!go)
            {
                deadTreasure.Add(go);
            }
            else if(go.GetComponent<Treasure>().isStuck)
            {
                whatToReturn = true;
            }
        }

        foreach(GameObject go in deadTreasure)
        {
            treasureStuckInCeiling.Remove(go);
        }

        return whatToReturn;
    }
}
