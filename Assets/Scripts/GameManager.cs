using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public enum ItemColor{
        BLACK = 0,
        BLUE = 1,
        RED = 2,
        MULTI = 3,
        NONE
    }

    public float timeToDie = 5f;

    public Score scoreBoard;
    public TreasureGenerator treasureGenerator;
    public PortalGenerator portalGenerator;
    public PlayerController player;

    public bool inGame = false;

    private float roundStartTime;
    public float TimeSinceStartOfRound { get { return Time.time - roundStartTime; }}


    public static GameManager instance = null;

    void Awake()
    {
        Screen.fullScreen = false;
    }

    void Start()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        scoreBoard.SetHighScore();
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return) && !inGame)
        {
            NewGame(true);
        }
    }

    public void StartGame()
    {
        treasureGenerator.StartTreasureGenerator();
        portalGenerator.StartPortalGenerator();
        roundStartTime = Time.time;
        inGame = true;
    }

    public EndGame endGame;

    public void EndGame()
    {
        inGame = false;
        treasureGenerator.StopTreasureGenerator();
        portalGenerator.StopPortalGenerator();

        bool isHighScore = false;
        if(PlayerPrefs.GetInt("HighScore", 0) < scoreBoard.currentScore)
        {
            Debug.Log("<color=purple>NEW HIGH SCORE: " + scoreBoard.currentScore.ToString() + "</color>");
            PlayerPrefs.SetInt("HighScore", scoreBoard.currentScore);
            isHighScore = true;
        }

        treasureGenerator.WipeTreasure();
        portalGenerator.WipePortals();

        // SHOW GAME OVER SCREEN
        endGame.gameObject.SetActive(true);
        endGame.SetCurrentScore(scoreBoard.currentScore, isHighScore);
    }

    public void NewGame(bool startGame)
    {
        treasureGenerator.WipeTreasure();
        portalGenerator.WipePortals();
        player.RespawnPlayer();
        scoreBoard.ResetScore();
        scoreBoard.SetHighScore();
        endGame.gameObject.SetActive(false);

        if (startGame) StartGame();
    }


    private ItemColor currentColor = ItemColor.NONE;
    private int currentColorStreak = 0;

    public GameObject pointsPopup;
    private GameObject ppInstance;

    public void AddPoints(int pointsToAdd, ItemColor color)
    {
        
        if (color == currentColor)
        {
            currentColorStreak++;

        }
        else
        {
            currentColorStreak = 1;
            currentColor = color;
        }


        int pointsTotal = pointsToAdd + currentColorStreak - 1;

        ppInstance = Instantiate(pointsPopup, player.transform.position, Quaternion.identity, player.transform);
        ppInstance.transform.SetParent(null);
        ppInstance.transform.localScale = new Vector3(Mathf.Abs(ppInstance.transform.localScale.x), ppInstance.transform.localScale.y, ppInstance.transform.localScale.z);
        ppInstance.GetComponent<PointsPopup>().SetCurrentScore(pointsTotal, color);
        scoreBoard.AddToScore(pointsTotal);
    }
}

public static class Rigidbody2DExtension
{
    public static void AddExplosionForce(this Rigidbody2D body, float explosionForce, Vector3 explosionPosition, float explosionRadius)
    {
        var dir = (body.transform.position - explosionPosition);
        float wearoff = 1 - (dir.magnitude / explosionRadius);
        body.AddForce(dir.normalized * (wearoff <= 0f ? 0f : explosionForce) * wearoff);
    }

    public static void AddExplosionForce(this Rigidbody2D body, float explosionForce, Vector3 explosionPosition, float explosionRadius, float upliftModifier)
    {
        var dir = (body.transform.position - explosionPosition);
        float wearoff = 1 - (dir.magnitude / explosionRadius);
        Vector3 baseForce = dir.normalized * (wearoff <= 0f ? 0f : explosionForce) * wearoff;
        body.AddForce(baseForce);

        float upliftWearoff = 1 - upliftModifier / explosionRadius;
        Vector3 upliftForce = Vector2.up * explosionForce * upliftWearoff;
        body.AddForce(upliftForce);
    }
}