using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour {

    public Sprite[] digits;
    public SpriteRenderer[] currentDigits;
    public SpriteRenderer[] highScoreDigits;

    int currentScore = 0;
    int highScore = 0;

    public void AddToScore(int pointsToAdd)
    {
        currentScore += pointsToAdd;
        SetCurrentScore();
        AudioManager.instance.PlaySFX(AudioManager.AudioSFX.Score);
    }

    public void SetCurrentScore()
    {
        int value = currentScore;
        int digitCounter = 1;

        while (value > 0)
        {
            int digit = value % 10;

            currentDigits[currentDigits.Length - digitCounter].sprite = digits[digit];
            digitCounter++;
            value /= 10;
        }
    }

    public void ResetScore()
    {
        for(int i = 0; i < currentDigits.Length; i++)
        {
            currentDigits[i].sprite = digits[0];
        }
    }

    public void SetHighScore()
    {

    }
}
