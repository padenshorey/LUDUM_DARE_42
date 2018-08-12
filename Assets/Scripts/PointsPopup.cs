using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsPopup : MonoBehaviour {

    public Sprite[] digits;
    public SpriteRenderer[] currentDigits;

    private void Awake()
    {
        ResetScore();
        Destroy(this.gameObject, 0.6f);
    }

    public void SetCurrentScore(int points, GameManager.ItemColor color)
    {
        int digitCounter = 1;
        int value = points;
        while (value > 0)
        {
            int digit = value % 10;

            currentDigits[currentDigits.Length - digitCounter].sprite = digits[digit];
            digitCounter++;
            value /= 10;
        }

        Color textColor;
        switch(color)
        {
            case GameManager.ItemColor.BLACK:
                textColor = Color.black;
                break;
            case GameManager.ItemColor.BLUE:
                textColor = new Color(0f / 255f, 52f / 255f, 157f / 255f);
                break;
            case GameManager.ItemColor.RED:
                textColor = new Color(137f/255f, 0f/ 255f, 11f/ 255f);
                break;
            default:
                textColor = Color.black;
                break;
        }

        for (int i = 0; i < currentDigits.Length; i++)
        {
            currentDigits[i].color = textColor;
        }
    }

    public void ResetScore()
    {
        for (int i = 0; i < currentDigits.Length; i++)
        {
            currentDigits[i].sprite = digits[0];
        }
    }
}
