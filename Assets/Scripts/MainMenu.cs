using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {

    public GameObject arrowPlay;
    public GameObject arrowStuff;
    public GameObject arrowCredits;

    public void Awake()
    {
        arrowPlay.SetActive(false);
        arrowStuff.SetActive(false);
        arrowCredits.SetActive(false);
    }

    public void ClickStart()
    {
        GetComponentInChildren<Animator>().SetTrigger("Leave");
    }

    public void ClickStuff()
    {

    }

    public void ClickCredits()
    {

    }

    public void ButtonClick()
    {
        AudioManager.instance.PlaySFX(AudioManager.AudioSFX.Click);
    }

    public void HoverPlay()
    {
        arrowPlay.SetActive(true);
    }

    public void ButtonHover()
    {
        AudioManager.instance.PlaySFX(AudioManager.AudioSFX.Hover, 0.25f);
    }
}
