using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5f;

    [SerializeField]
    private Transform _startPoint;

    public GameLogic.Player _player;

    private Rigidbody2D _rb;
    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    void OnEnable()
    {
        _startPoint = gameObject.transform;
        _rb = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        Respawn();
    }

    public void Respawn()
    {
        transform.position = _startPoint.transform.position;
        _rb.velocity = Random.insideUnitCircle.normalized * _speed;
    }
}
