using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public bool mainGame = true;
    [SerializeField]
    private GameObject _P1BallBreakerAssets;

    [SerializeField]
    private GameObject _P2BallBreakerAssets;

    [SerializeField]
    private GameObject _TetrisAssets;

    [SerializeField]
    private GameObject _P1TetrisUI;

    [SerializeField]
    private GameObject _P1BallBreakerUI;

    [SerializeField]
    private GameObject _P2TetrisUI;

    [SerializeField]
    private GameObject _P2BallBreakerUI;

    [SerializeField]
    public static int player1BlockCount = 11;

    [SerializeField]
    public static int player2BlockCount = 11;

    private bool p1IsBall;

    private bool p2IsBall;

    public static int p1Life = 2;
    public static int p2Life = 2;

    [SerializeField]
    TMP_Text GameOverText;

    [SerializeField]
    private GameObject _GameOverUI;


    [Header("WINNER")]

    public GameObject p1WinnerAssets;
    public GameObject p2WinnerAssets;

    [Header("AUDIO")]
    public AudioSource levelMusic;
    public AudioClip TetrisMusic;
    public AudioClip BBMusic;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        ResetValues();
        if (mainGame == false)
        {
            int winner = PlayerPrefs.GetInt("Winner", 0);
            switch (winner)
            {
                case 1:
                    p1WinnerAssets.SetActive(true);
                    Debug.Log("PLAYER 1 WINNER");
                    break;
                case 2:
                    p2WinnerAssets.SetActive(true);
                    Debug.Log("PLAYER 2 WINNER");
                    break;
                case 0:
                    Debug.Log("ERROR NOBODY WON");
                    break;

                default:
                    p1WinnerAssets.SetActive(true);
                    break;
            }
        }
        else
        {

            levelMusic.clip = TetrisMusic;
            DisableBallBreakerAssets();
            UpdateBlockCount();
            UpdateLiveCount();
            _GameOverUI.SetActive(false);
        }
    }

    private static void ResetValues()
    {
        player1BlockCount = 11;
        player2BlockCount = 11;
        p1Life = 2;
        p2Life = 2;
    }

    public void ReduceHealth(GameLogic.Player player)
    {
        if (player == GameLogic.Player.P1)
        {
            p1Life = p1Life - 1 < 0 ? 0 : p1Life - 1;
        }
        else
        if (player == GameLogic.Player.P2)
        {
            p2Life = p2Life - 1 < 0 ? 0 : p2Life - 1;
        }

        Debug.Log("p1:" + p1Life);
        Debug.Log("p2:" + p2Life);

        UpdateLiveCount();

        if (p1Life <= 0 || p2Life <= 0)
            GameOver(0);
    }

    public void DisableBallBreakerAssets()
    {
        _P1BallBreakerAssets.SetActive(false);
        _P2BallBreakerAssets.SetActive(false);
        _P1BallBreakerUI.SetActive(false);
        _P2BallBreakerUI.SetActive(false);
    }

    public void SetupBallBreakerP1()
    {
        SwitchMusicToBB();
        p1IsBall = true;
        _P1BallBreakerAssets.SetActive(true);
        _P1TetrisUI.SetActive(false);
        _P1BallBreakerUI.SetActive(true);

    }

    public void SetupBallBreakerP2()
    {
        SwitchMusicToBB();
        p2IsBall = true;
        _P2BallBreakerAssets.SetActive(true);
        _P2TetrisUI.SetActive(false);
        _P2BallBreakerUI.SetActive(true);
    }

    public void SetupBallBreakerCompletely()
    {
        _TetrisAssets.SetActive(false);
    }

    public void ReduceBlockCount(GameLogic.Player player)
    {
        if (player == GameLogic.Player.P1)
        {
            player1BlockCount--;
        }
        else if (player == GameLogic.Player.P2)
        {
            player2BlockCount--;
        }
        // Debug.Log($"{player1BlockCount}&&{player2BlockCount}");

        if (player1BlockCount <= 0)
        {
            if (p1IsBall == false)
            {
                SetupBallBreakerP1();
            }
        }
        if (player2BlockCount <= 0)
        {
            if (p2IsBall == false)
            {
                SetupBallBreakerP2();
            }
        }
        if (p1IsBall && p2IsBall)
        {
            SetupBallBreakerCompletely();
        }

        UpdateBlockCount();
    }

    public void UpdateBlockCount()
    {
        _P1TetrisUI.GetComponentInChildren<TMP_Text>().text = player1BlockCount.ToString();
        _P2TetrisUI.GetComponentInChildren<TMP_Text>().text = player2BlockCount.ToString();
    }

    public void UpdateLiveCount()
    {
        _P1BallBreakerUI.GetComponentInChildren<TMP_Text>().text = "LIVES: " + p1Life.ToString();
        _P2BallBreakerUI.GetComponentInChildren<TMP_Text>().text = "LIVES: " + p2Life.ToString();
    }

    public void GameOver(int condition, GameLogic.Player player = GameLogic.Player.P1)
    {
        //0 -  by losing health
        //1 - by player moving to other side

        Time.timeScale = 0;
        if (condition == 0)
        {
            if (p1Life > 0)
            {
                PlayerPrefs.SetInt("Winner", 1);
                Debug.Log("SAVED PLAYER 1");
            }
            else
            {
                PlayerPrefs.SetInt("Winner", 2);
                Debug.Log("SAVED PLAYER 2");
            }
        }
        else if (condition == 1)
        {
            if (player == GameLogic.Player.P1)
            {
                PlayerPrefs.SetInt("Winner", 1);
                Debug.Log("SAVED PLAYER 1");
            }
            else
            {
                PlayerPrefs.SetInt("Winner", 2);
                Debug.Log("SAVED PLAYER 2");
            }
        }
        SceneManager.LoadScene("WinScreen");
    }

    public void SwitchMusicToBB()
    {
        if (levelMusic.clip != BBMusic)
        {
            levelMusic.clip = BBMusic;
            levelMusic.Play();
            levelMusic.volume = 0.2f;
            levelMusic.loop = true;
        }
    }


}
