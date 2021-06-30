using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Bounds : MonoBehaviour
{
    [SerializeField]
    private GameLogic.Player _player;

    /// <summary>
    /// Sent when an incoming collider makes contact with this object's
    /// collider (2D physics only).
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<Ball>()._player == _player) //Respawn only if the ball falls back towards the player side
        {
            Debug.Log("RESPAWN");
            other.gameObject.GetComponent<Ball>().Respawn();
        }
    }
}
