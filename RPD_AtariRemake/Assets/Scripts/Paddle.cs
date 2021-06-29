using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public float _speed = 5f;

    private float input;

    private Rigidbody2D _rb;

    public GameLogic.Player _player;


    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    void OnEnable()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        //Really bad input system
        if (_player == GameLogic.Player.P1)
        {
            input = Input.GetAxisRaw("VerticalP1");
        }
        else
        if (_player == GameLogic.Player.P2)
        {
            input = Input.GetAxisRaw("VerticalP2");

        }
    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate()
    {
        _rb.velocity = Vector2.up * input * _speed;
    }
}
