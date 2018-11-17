using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;

public class UIManager : MonoBehaviour
{
    public BlurOptimized blur;
    public Button pauseButton;
    public Button restartButton;
    public GameObject reviveButton;
    public Canvas mainMenu;
    public AudioSource mainMusic;
    public GameObject startResumeObj;
    public Text reviveText;
    public Text hishcoreText;
    public Text scoreText;
    public Text allCoinsText;
    public Text coinsText;
    public Slider soundFXSlider;
    public Slider backgroundMusicSlider;

    public static bool isPaused = true;

    private bool isReviveCostIncreased;
    private Button reviveButtonComponent;
    private int reviveCost;
    private float timeCounter;
    private Text startResumeText;
    private PlayerController player;

    void Start()
    {
        this.reviveButtonComponent = this.reviveButton.GetComponent<Button>();
        this.player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        this.startResumeText = this.startResumeObj.GetComponentInChildren<Text>();
        this.soundFXSlider.value = PlayerPrefs.GetFloat("SoundFXVolume", 1f);
        this.backgroundMusicSlider.value = PlayerPrefs.GetFloat("BackgroundMusicVolume", 0.5f);
        this.restartButton.interactable = false;
        this.reviveButton.SetActive(false);
        this.DisableEnableMenu(true);
        Time.timeScale = 0;
    }

    public void Update()
    {
        if (this.player.isDead)
        {
            if (!this.isReviveCostIncreased)
            {
                this.reviveCost += 120;
                this.isReviveCostIncreased = true;
            }

            isPaused = true;
            this.startResumeObj.SetActive(false);
            this.reviveText.text = string.Format("REVIVE FOR {0} COINS" ,this.reviveCost);
            this.timeCounter += Time.deltaTime;
            if (timeCounter > 1.5f)
            {
                this.restartButton.interactable = true;
                DisableEnableMenu(true);
            }

        }
        else
        {
            this.timeCounter = 0;
            this.isReviveCostIncreased = false;
        }

        if (isPaused)
        {
            this.pauseButton.enabled = false;
        }
        else
        {
            this.pauseButton.enabled = true;
        }
    }

    public void UpdateCoinsText()
    {
        this.coinsText.text = ValuesManager.currentCoins.ToString();
    }

    public void UpdateScoreText()
    {
        this.scoreText.text = "SCORE: " + ValuesManager.score;
    }

    public void OnStartResumePress()
    {
        Time.timeScale = 1;
        if (!this.mainMusic.isPlaying)
        {
            this.mainMusic.Play();
        }
        DisableEnableMenu(false);
        isPaused = false;
        this.startResumeText.text = "Resume";
    }

    public void OnPausePress()
    {
        Time.timeScale = 0;
        this.mainMusic.Pause();
        isPaused = true;
        this.startResumeObj.SetActive(true);
        this.restartButton.interactable = true;
        ValuesManager.SaveScore();
        DisableEnableMenu(true);
    }

    public void OnRestartPress()
    {
        ValuesManager.SaveCoins();
        ValuesManager.currentCoins = 0;
        Application.LoadLevel(Application.loadedLevel);
    }

    public void OnExitPress()
    {
        if (!this.player.isDead)
        {
            ValuesManager.SaveCoins();
        }
        Application.Quit();
    }

    public void OnReviveClick()
    {
        isPaused = false;
        this.mainMenu.enabled = false;
        this.blur.enabled = false;
        this.player.isDead = false;
        this.player.forwardSpeed = this.player.initialForwardSpeed;
        this.player.jumpSpeed = this.player.initialJumpSpeed;
        ValuesManager.PayReviveCost(this.reviveCost);

    }

    public void AdjustSoundFX(float volume)
    {
        this.player.footStepAudio.volume = volume;
        this.player.jetpackAudio.volume = volume;
        this.player.mainAudioSrc.volume = volume;
        PlayerPrefs.SetFloat("SoundFXVolume", volume);
    }

    public void AdjustBackgroundMusic(float volume)
    {
        this.mainMusic.volume = volume;
        PlayerPrefs.SetFloat("BackgroundMusicVolume", volume);
    }

    private void DisableEnableMenu(bool isEnabled)
    {
        if (isEnabled)
        {
            this.hishcoreText.text = "HIGHSCORE: " + ValuesManager.highScore;
            this.allCoinsText.text = "ALL COINS: " + ValuesManager.allCoins;
        }
        this.mainMenu.enabled = isEnabled;
        this.blur.enabled = isEnabled;
        if (this.player.isDead)
        {
            if (this.reviveCost > ValuesManager.allCoins)
            {
                this.reviveButtonComponent.interactable = false;
            }
            else
            {
                this.reviveButtonComponent.interactable = true;
            }
            this.reviveButton.SetActive(true);
        }
        else
        {
            this.reviveButton.SetActive(false);
        }

    }

}
