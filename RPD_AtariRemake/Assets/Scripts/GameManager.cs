using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    private GameObject _P1BallBreakerAssets;

    [SerializeField]
    private GameObject _P2BallBreakerAssets;

    [SerializeField]
    private GameObject _TetrisAssets;

    [SerializeField]
    public static int player1BlockCount = 11;

    [SerializeField]
    public static int player2BlockCount = 11;

    private bool p1IsBall;

    private bool p2IsBall;

    // Start is called before the first frame update
    void Start()
    {
        DisableBallBreakerAssets();
    }

    public void DisableBallBreakerAssets()
    {
        _P1BallBreakerAssets.SetActive(false);
        _P2BallBreakerAssets.SetActive(false);

    }
    public void SetupBallBreakerP1()
    {
        p1IsBall = true;
        _P1BallBreakerAssets.SetActive(true);

    }

    public void SetupBallBreakerP2()
    {
        p2IsBall = true;
        _P2BallBreakerAssets.SetActive(true);
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
        Debug.Log($"{player1BlockCount}&&{player2BlockCount}");

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
    }
}
