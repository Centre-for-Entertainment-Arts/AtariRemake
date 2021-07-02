using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBlock : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _blocks;

    [SerializeField]
    private GameLogic.Player _player; //Spawn for which player;

    public GameManager Manager;

    void Start()
    {
        //   SpawnSingleBlock();
    }

    public void SpawnSingleBlock()
    {
        if (_player == GameLogic.Player.P1)
        {
            if (GameManager.player1BlockCount <= 0)
            {
                return;
            }

        }
        else if (_player == GameLogic.Player.P2)
        {
            if (GameManager.player2BlockCount <= 0)
            {
                return;
            }
        }
        float guess = Random.Range(0, 1f);
        guess *= _blocks.Length;
        var clone = Instantiate(_blocks[Mathf.FloorToInt(guess)], transform.position, Quaternion.identity);
        clone.GetComponent<tetrisBlock>().SetupPlayerNumberAndColor(_player, this);
        clone.GetComponent<tetrisBlock>().Manager = Manager;
    }
}
