using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{

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

    // Start is called before the first frame update
    void Start()
    {
        DisableBallBreakerAssets();
        UpdateBlockCount();
        UpdateLiveCount();
        Time.timeScale = 1;
        _GameOverUI.SetActive(false);
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
            GameOver();
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
        p1IsBall = true;
        _P1BallBreakerAssets.SetActive(true);
        _P1TetrisUI.SetActive(false);
        _P1BallBreakerUI.SetActive(true);

    }

    public void SetupBallBreakerP2()
    {
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
        _P1TetrisUI.GetComponentInChildren<TMP_Text>().text = "BLOCK COUNT: " + player1BlockCount.ToString();
        _P2TetrisUI.GetComponentInChildren<TMP_Text>().text = "BLOCK COUNT: " + player2BlockCount.ToString();
    }

    public void UpdateLiveCount()
    {
        _P1BallBreakerUI.GetComponentInChildren<TMP_Text>().text = "LIVES: " + p1Life.ToString();
        _P2BallBreakerUI.GetComponentInChildren<TMP_Text>().text = "LIVES: " + p2Life.ToString();
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        Debug.Log("GAME OVER");
        _GameOverUI.SetActive(true);

        string endgameText = "DEFAULT";
        if (p1Life > 0)
            endgameText = "PLAYER 1 WINS";
        else
        {
            endgameText = "PLAYER 2 WINS";
        }
        GameOverText.text = endgameText;
    }
}
