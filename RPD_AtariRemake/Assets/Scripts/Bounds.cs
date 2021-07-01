using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Bounds : MonoBehaviour
{
    [SerializeField]
    private GameLogic.Player _player;

    private GameManager _manager;

    bool doneOnce;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        _manager = GameObject.FindObjectOfType<GameManager>();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log(other.gameObject.name);
        if (doneOnce) return;
        doneOnce = true;
        if (other.gameObject.GetComponent<Ball>()._player == _player) //Respawn only if the ball falls back towards the player side
        {
            Debug.Log("RESPAWN");
            _manager.ReduceHealth(_player);
            if (_player == GameLogic.Player.P1)
                if (GameManager.p1Life > 0)
                    other.gameObject.GetComponent<Ball>().Respawn();
            if (_player == GameLogic.Player.P2)
                if (GameManager.p2Life > 0)
                    other.gameObject.GetComponent<Ball>().Respawn();
        }
        doneOnce = false;
    }
}
