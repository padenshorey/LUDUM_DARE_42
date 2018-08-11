using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {

    public GameObject arrowPlay;
    public GameObject arrowStuff;
    public GameObject arrowCredits;

    public AudioClip musicMain;
    public AudioClip musicGame;

    public GameObject game;

    private bool introComplete = false;

    public void Start()
    {
        Setup();
    }

    private void Setup()
    {
        arrowPlay.SetActive(false);
        arrowStuff.SetActive(false);
        arrowCredits.SetActive(false);
        introComplete = false;

        StartCoroutine(FinishIntro());
    }

    private IEnumerator FinishIntro()
    {
        yield return new WaitForSeconds(7.3f);
        introComplete = true;
    }

    public void ClickStart()
    {
        if (introComplete)
        {
            introComplete = false;
            GetComponentInChildren<Animator>().SetTrigger("Leave");
            game.SetActive(true);
        }
    }

    public void ClickStuff()
    {
        if (introComplete)
        {
            introComplete = false;
        }
    }

    public void ClickCredits()
    {
        if (introComplete)
        {
            introComplete = false;
        }
    }

    public void ButtonClick()
    {
        if (introComplete)
        {
            AudioManager.instance.PlaySFX(AudioManager.AudioSFX.Click);
        }
    }

    public void HoverPlay()
    {
        if (introComplete)
        {
            arrowPlay.SetActive(true);
        }
    }

    public void ButtonHover()
    {
        if (introComplete)
        {
            AudioManager.instance.PlaySFX(AudioManager.AudioSFX.Hover, 0.25f);
        }
    }
}
