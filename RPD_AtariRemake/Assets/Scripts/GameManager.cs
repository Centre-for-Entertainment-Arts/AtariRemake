using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    private GameObject _BallBreakerAssets;

    [SerializeField]
    private GameObject _TetrisAssets;

    [SerializeField]
    public static int player1BlockCount = 10;

    [SerializeField]
    public static int player2BlockCount = 10;

    // Start is called before the first frame update
    void Start()
    {
        _BallBreakerAssets.SetActive(false);

    }

    public void SetupBallBreaker()
    {
        _TetrisAssets.SetActive(false);
        _BallBreakerAssets.SetActive(true);
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

        if (player1BlockCount <= 0 && player2BlockCount <= 0)
        {
            SetupBallBreaker();
        }
    }
}
