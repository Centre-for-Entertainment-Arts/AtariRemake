using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBlock : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _blocks;

    void Start()
    {
        SpawnSingleBlock();
    }

    public void SpawnSingleBlock()
    {
        float guess = Random.Range(0, 1f);
        guess *= _blocks.Length;
        Instantiate(_blocks[Mathf.FloorToInt(guess)], transform.position, Quaternion.identity);
    }
}
