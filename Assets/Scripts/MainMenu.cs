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
    private bool inMenu = true;

    public void Start()
    {
        Setup();
    }

    public void ReturnToMenu()
    {
        GetComponentInChildren<Animator>().SetTrigger("Return");

        introComplete = true;
        inMenu = true;

        StartCoroutine(HideGame());
    }

    IEnumerator HideGame()
    {
        yield return new WaitForSeconds(1.5f);

        if(inMenu) game.SetActive(false);
    }

    private void Update()
    {
        if((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) && !introComplete && inMenu)
        {
            introComplete = true;
            GetComponentInChildren<Animator>().SetTrigger("Skip");
        }
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
            inMenu = false;
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
            Application.OpenURL("http://www.padenshorey.com");
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
