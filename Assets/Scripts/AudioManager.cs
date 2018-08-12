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
        Hit,
        Explode,
        GameOver,
        Bomb,
        HighScore
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
    public AudioClip[] explosions;
    public AudioClip gameover;
    public AudioClip bomb;
    public AudioClip highScore;

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
            case AudioSFX.HighScore:
                clipToPlay = highScore;
                break;
            case AudioSFX.Bomb:
                clipToPlay = bomb;
                break;
            case AudioSFX.GameOver:
                clipToPlay = gameover;
                break;
            case AudioSFX.Land:
                clipToPlay = landings[Random.Range(0, landings.Length)];
                break;
            case AudioSFX.Hit:
                clipToPlay = hits[Random.Range(0, hits.Length)];
                break;
            case AudioSFX.Explode:
                clipToPlay = explosions[Random.Range(0, explosions.Length)];
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

    /*
    public AudioClip songFast;
    public AudioClip songNormal;

    public void PlayFasterSong(bool fast)
    {
        musicSource.Stop();
        float timeToStart = 0;
        if (fast && musicSource.clip != songFast)
        {
            timeToStart = musicSource.time * (2f / 3f);
            musicSource.clip = songFast;
            musicSource.time = timeToStart;
        }
        else if(musicSource.clip != songNormal)
        {
            timeToStart = musicSource.time * (3f / 2f);
            musicSource.clip = songNormal;
            musicSource.time = timeToStart;
        }
        musicSource.Play();
    }
    */



}
