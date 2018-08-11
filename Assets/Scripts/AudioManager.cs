using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public static AudioManager instance = null;

    public enum AudioSFX
    {
        Jump,
        Land,
        Splat,
        Click,
        Hover
    }

    public AudioSource audioSource;

    public AudioClip jump;
    public AudioClip splat;
    public AudioClip[] landings;
    public AudioClip click;
    public AudioClip hover;

    void Start () {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    public void PlaySFX(AudioSFX sfx, float volume = 1f)
    {
        AudioClip clipToPlay = null;
        switch(sfx)
        {
            case AudioSFX.Jump:
                clipToPlay = jump;
                break;
            case AudioSFX.Splat:
                clipToPlay = splat;
                break;
            case AudioSFX.Click:
                clipToPlay = click;
                break;
            case AudioSFX.Hover:
                clipToPlay = hover;
                break;
            case AudioSFX.Land:
                clipToPlay = landings[Random.Range(0, landings.Length)];
                break;
        }

        audioSource.volume = volume;
        audioSource.PlayOneShot(clipToPlay);
    }

}
