using UnityEngine;

public class ValuesManager : MonoBehaviour
{
    public static int allCoins;
    public static int currentCoins;
    public static int score;
    public static int highScore;

    private int startPosX;
    private UIManager ui;
    private Transform playerPos;

    public void Start()
    {
        this.ui = GameObject.FindGameObjectWithTag("MainCanvas").GetComponent<UIManager>();
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        this.playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        this.startPosX = (int)this.playerPos.position.x;
        score = 0;
        allCoins = PlayerPrefs.GetInt("Coins");
    }

    public void Update()
    {
        if (!UIManager.isPaused)
        {
            score = (int)(this.playerPos.position.x) - this.startPosX;
            this.ui.UpdateScoreText();
        }
    }

    public static void SaveScore()
    {
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
        }
    }

    public static void SaveCoins()
    {
        PlayerPrefs.SetInt("Coins", allCoins);
        PlayerPrefs.Save();
    }

    public static void AddCoin()
    {
        currentCoins++;
        allCoins++;
    }

    public static void PayReviveCost(int reviveCost)
    {
        allCoins -= reviveCost;
        if (currentCoins > allCoins)
        {
            currentCoins = allCoins;
        }
    }
}
