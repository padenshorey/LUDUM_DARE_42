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
        Hover,
        Score,
        Hit
    }

    public AudioSource audioSource;

    public AudioSource musicSource;

    public AudioClip jump;
    public AudioClip splat;
    public AudioClip[] landings;
    public AudioClip[] hits;
    public AudioClip click;
    public AudioClip hover;
    public AudioClip score;

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
            case AudioSFX.Score:
                clipToPlay = score;
                break;
            case AudioSFX.Land:
                clipToPlay = landings[Random.Range(0, landings.Length)];
                break;
            case AudioSFX.Hit:
                clipToPlay = hits[Random.Range(0, hits.Length)];
                break;
        }

        audioSource.volume = volume;
        audioSource.PlayOneShot(clipToPlay);
    }

    public void PlayMusic(AudioClip music)
    {
        musicSource.Stop();
        musicSource.clip = music;
        musicSource.Play();
    }

}
