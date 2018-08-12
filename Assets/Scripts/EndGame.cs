using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour {

    public Sprite[] digits;
    public SpriteRenderer[] currentDigits;
    public GameObject highScoreSign;
    private int finalScore;

    public void SetCurrentScore(int points, bool highScore)
    {
        finalScore = points;
        int digitCounter = 1;
        int value = points;
        while (value > 0)
        {
            int digit = value % 10;

            currentDigits[currentDigits.Length - digitCounter].sprite = digits[digit];
            digitCounter++;
            value /= 10;
        }

        highScoreSign.SetActive(highScore);
        if (highScore) AudioManager.instance.PlaySFX(AudioManager.AudioSFX.HighScore);
    }

    public void ResetScore()
    {
        for (int i = 0; i < currentDigits.Length; i++)
        {
            currentDigits[i].sprite = digits[0];
        }

        finalScore = 0;
    }
}
