using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tetrisBlock : MonoBehaviour
{
    private SpawnBlock _spawner;

    [SerializeField]
    private Transform _rotationPoint;

    private float _previousTime;
    [SerializeField]
    private float _fallTime = 5f; //0.8f is the original tetris' speed

    [SerializeField]
    private float _stepSize = 1f;

    private bool _finished = false;

    GameLogic _gamelogic;

    [SerializeField]
    private GameObject[] _childblocks;

    public GameLogic.Player _player;

    public GameManager Manager;
    public bool lastBlock;

    //AUDIO
    [SerializeField]
    private AudioClip _registerBlockSFX;
    [SerializeField]
    private AudioClip _rotateBlockSFX;
    private AudioSource _source;


    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    void OnEnable()
    {
        _spawner = GameObject.FindObjectOfType<SpawnBlock>();
        _gamelogic = GameObject.FindObjectOfType<GameLogic>();
        _source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_finished) return;
        MoveTetris();
        FallTetris();
        RotateTetris();
    }

    private void RotateTetris()
    {
        if (_player == GameLogic.Player.P1)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                _rotationPoint.eulerAngles += new Vector3(0, 0, 90);
                if (!ValidMove())
                {
                    _rotationPoint.eulerAngles -= new Vector3(0, 0, 90);
                    return;
                }
                _source.PlayOneShot(_rotateBlockSFX);
            }

        }
        else
        if (_player == GameLogic.Player.P2)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                _rotationPoint.eulerAngles += new Vector3(0, 0, 90);
                if (!ValidMove())
                {
                    _rotationPoint.eulerAngles -= new Vector3(0, 0, 90);
                    return;
                }
                _source.PlayOneShot(_rotateBlockSFX);
            }
        }
    }

    private void RegisterBlock()
    {
        _finished = true;
        Manager.ReduceBlockCount(_player);
        _source.PlayOneShot(_registerBlockSFX);

        int roundedX = Mathf.FloorToInt(gameObject.transform.position.x);
        int roundedY = Mathf.FloorToInt(gameObject.transform.position.y);
        if (_player == GameLogic.Player.P1)
        {
            if (roundedY > GameLogic.p1StartPos + 2 || roundedY <= GameLogic.p1EndPos)
            {
                Destroy(gameObject); //If the current block is out of bounds dont's register it and destroy it.
                return;
            }
        }
        if (_player == GameLogic.Player.P2)
        {
            if (roundedY >= GameLogic.p2EndPos || roundedY < GameLogic.p2StartPos)
            {
                Destroy(gameObject); //If the current block is out of bounds dont's register it and destroy it.
                return;
            }
        }

        foreach (GameObject child in _childblocks)
        {
            if (child == null) continue;
            GameLogic.Grid[Mathf.FloorToInt(child.transform.position.x), Mathf.FloorToInt(child.transform.position.y)] = child.transform;
            // Debug.Log($"Registered at: {Mathf.FloorToInt(child.transform.position.x)},{Mathf.FloorToInt(child.transform.position.y)}");
        }
    }

    private void FallTetris()
    {
        if (_player == GameLogic.Player.P1) //MOVE DOWN IF PLAYER 1
        {
            if (Time.time - _previousTime > (Input.GetKey(KeyCode.S) ? _fallTime / 5 : _fallTime))
            {
                transform.position += new Vector3(0, -_stepSize, 0); //Move the tetris down
                if (!ValidMove())
                {
                    transform.position += new Vector3(0, _stepSize, 0);
                    RegisterBlock();
                    _spawner.SpawnSingleBlock();
                }
                _previousTime = Time.time;
            }
        }
        else
        if (_player == GameLogic.Player.P2) //MOVE UP IF PLAYER 2
        {
            if (Time.time - _previousTime > (Input.GetKey(KeyCode.UpArrow) ? _fallTime / 5 : _fallTime))
            {
                transform.position -= new Vector3(0, -_stepSize, 0); //Move the tetris down
                if (!ValidMove())
                {
                    transform.position += new Vector3(0, -_stepSize, 0);
                    RegisterBlock();
                    _spawner.SpawnSingleBlock();

                }
                _previousTime = Time.time;
            }
        }
    }

    private void MoveTetris()
    {
        //USE WASD IF PLAYER 1
        if (_player == GameLogic.Player.P1)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                transform.position += new Vector3(_stepSize, 0, 0); //Move the tetris Right
                if (!ValidMove())
                {
                    transform.position -= new Vector3(_stepSize, 0, 0);

                }

            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                transform.position += new Vector3(-_stepSize, 0, 0); //Move the tetris Left
                if (!ValidMove())
                {
                    transform.position -= new Vector3(-_stepSize, 0, 0);

                }

            }
        }
        //USE ARROW IF PLAYER 2
        else if (_player == GameLogic.Player.P2)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                transform.position += new Vector3(_stepSize, 0, 0); //Move the tetris Right
                if (!ValidMove())
                {
                    transform.position -= new Vector3(_stepSize, 0, 0);
                    //  RegisterBlock();
                    //  _spawner.SpawnSingleBlock();
                }

            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                transform.position += new Vector3(-_stepSize, 0, 0); //Move the tetris Left
                if (!ValidMove())
                {
                    transform.position -= new Vector3(-_stepSize, 0, 0);
                    // RegisterBlock();
                    // _spawner.SpawnSingleBlock();

                }

            }
        }

    }

    bool ValidMove()
    {
        //Player 1 Valid Move
        if (_player == GameLogic.Player.P1)
        {
            foreach (GameObject child in _childblocks)
            {

                if (child == null) continue;
                int roundedX = Mathf.FloorToInt(child.transform.position.x);
                int roundedY = Mathf.FloorToInt(child.transform.position.y);

                if (roundedX < GameLogic.p1MinWidth || roundedX >= GameLogic.p1MaxWidth || roundedY > GameLogic.p1StartPos + 2 || roundedY <= GameLogic.p1EndPos) //1 is added to maxHeight to prevent spawn bug
                {
                    return false;
                }

                if (child.transform.position.y > GameLogic.p1EndPos && GameLogic.Grid[Mathf.FloorToInt(child.transform.position.x), Mathf.FloorToInt(child.transform.position.y)] != null)
                    return false;
            }

            return true;
        }
        if (_player == GameLogic.Player.P2)
        {
            foreach (GameObject child in _childblocks)
            {
                if (child == null) continue;
                int roundedX = Mathf.FloorToInt(child.transform.position.x);
                int roundedY = Mathf.FloorToInt(child.transform.position.y);

                if (roundedX < GameLogic.p2MinWidth || roundedX >= GameLogic.p2MaxWidth || roundedY >= GameLogic.p2EndPos || roundedY < GameLogic.p2StartPos) //2 is reduced from minHeight to prevent spawn bug
                {
                    return false;
                }
                if (child.transform.position.y < GameLogic.p2EndPos && GameLogic.Grid[Mathf.FloorToInt(child.transform.position.x), Mathf.FloorToInt(child.transform.position.y)] != null)
                    return false;
            }

            return true;
        }
        return false;
    }

    public void SetupPlayerNumberAndColor(GameLogic.Player player, SpawnBlock spawner)
    {
        _player = player;
        _spawner = spawner;

        //Choose the block color
        var playerColors = _gamelogic.P1Colors;
        if (player == GameLogic.Player.P2)
        {
            playerColors = _gamelogic.P2Colors;
        }
        float guess = UnityEngine.Random.Range(0, 1f);
        guess *= playerColors.Length;

        var chosenColor = playerColors[Mathf.FloorToInt(guess)];
        chosenColor.a = 1;
        //Apply the color accordingly
        foreach (var item in _childblocks)
        {
            item.GetComponent<SpriteRenderer>().color = chosenColor;
        }
    }


}
