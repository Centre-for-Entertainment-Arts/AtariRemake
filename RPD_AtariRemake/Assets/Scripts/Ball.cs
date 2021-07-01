using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5f;

    private Vector3 _startPoint;

    public GameLogic.Player _player;

    private Rigidbody2D _rb;

    private Vector2[] p1VelocityDir = { new Vector2(-1, 1), new Vector2(-1, -1) };
    private Vector2[] p2VelocityDir = { new Vector2(1, 1), new Vector2(1, -1) };

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    void Awake()
    {
        _startPoint = gameObject.transform.localPosition;
        _rb = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    void OnEnable()
    {
        Respawn();
    }


    public void Respawn()
    {
        StartCoroutine(PauseAndShoot());
    }

    IEnumerator PauseAndShoot()
    {
        _rb.velocity = Vector2.zero;

        transform.localPosition = _startPoint;

        yield return new WaitForSeconds(2.0f);

        float guess = Random.Range(0, 1f);
        guess *= _player == GameLogic.Player.P1 ? p1VelocityDir.Length : p2VelocityDir.Length;

        var chosenDir = _player == GameLogic.Player.P1 ? p1VelocityDir[Mathf.FloorToInt(guess)] : p2VelocityDir[Mathf.FloorToInt(guess)];
        _rb.velocity = chosenDir.normalized * _speed;
    }
}
