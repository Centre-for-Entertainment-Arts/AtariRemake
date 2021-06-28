using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBlock : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _blocks;

    [SerializeField]
    private GameLogic.Player _player; //Spawn for which player;

    void Start()
    {
        SpawnSingleBlock();
    }

    public void SpawnSingleBlock()
    {
        float guess = Random.Range(0, 1f);
        guess *= _blocks.Length;
        var clone = Instantiate(_blocks[Mathf.FloorToInt(guess)], transform.position, Quaternion.identity);
        clone.GetComponent<tetrisBlock>().SetupPlayerNumberAndColor(_player, this);
    }
}
