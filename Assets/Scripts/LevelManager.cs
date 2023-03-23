using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    #region Fields
    //Inspector Fields
    [SerializeField]
    GameManager gameManager;
    [SerializeField]
    GameObject restartMenu;             //Menu will pop up for user to choose to play again or not
    [SerializeField]
    Text message;                       //UI Text to show messages like you win/lose etc...
    [SerializeField]
    float pauseTimer;                   //Countdown pauseTimer then start the game
    [SerializeField]
    PlayerControl playerControl;
    [SerializeField]
    Spinner spinner;

    //Private Fields
    bool isStopped;
    bool isPaused;
    #endregion

    #region Unity Methods
    private void Start()
    {
        playerControl.PayerMaterials.color = gameManager.Colors[gameManager.SaveManager.PlayerColor];
        isStopped = true;
        StopGame();
        StartCountdown();
    }
    private void OnEnable()
    {
        Rods.OnThrowPlayer += OnGameLost;
    }
    private void OnDisable()
    {
        Rods.OnThrowPlayer -= OnGameLost;
    }
    private void Update()
    {
        if(spinner.Speed >= spinner.MaxSpeed){
            OnGameWon();
        }
    }
    #endregion
    #region Public Methods
    public void StartCountdown()
    {
        StartCoroutine(Countdown());
    }
    public void ChangeColor(int selectedColorIndex)
    {
        gameManager.ChangeColor(selectedColorIndex);
        playerControl.PayerMaterials.color = gameManager.Colors[gameManager.SaveManager.PlayerColor]; 
    }
    public void PauseButton()
    {
        if (isStopped)
        {
            if (pauseTimer > 0)
            {
                StopAllCoroutines();
                pauseTimer = 0;
                return;
            }
            pauseTimer = 4;
            StartCountdown();
            return;
        }
        isStopped = true;
        StopGame();
    }
    #endregion
    #region Private Methods
    IEnumerator Countdown()
    {
        while (isStopped)
        {
            if (pauseTimer <= 0) 
            { 
                isStopped = false;
                StartGame(); 
                message.text = string.Empty;
            }
            else
            {
                DisplayCountdown();

                yield return null;
            }
        }
    }

    void DisplayCountdown()
    {
        pauseTimer -= Time.deltaTime;
        int timeInt = (int)pauseTimer;
        message.text = timeInt.ToString();
    }

    void StopGame()
    {
        spinner.Stop = true;
        playerControl.Stop = true;
    }
    void StartGame()
    {
        spinner.Stop = false;
        playerControl.Stop = false;
    }
    void OnGameLost()
    {
        if (message.text == string.Empty)
        {
            message.text = "Try Again";
        }
        isStopped = true;
        StopGame();
        restartMenu.SetActive(true);
    }
    void OnGameWon()
    {
        if (message.text == string.Empty)
        {
            message.text = "You Won!";
        }
        isStopped = true;
        StopGame();
        restartMenu.SetActive(true);
    }
    #endregion
}
