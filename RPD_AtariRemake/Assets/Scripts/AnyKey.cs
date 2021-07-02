using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnyKey : MonoBehaviour
{
    bool GetInput;

    [SerializeField]
    private float DelayDuration = 3.0f;

    [SerializeField]
    private string MapName = "MainMenu";

    // Start is called before the first frame update
    void Start()
    {
        Invoke("SetInputToTrue", DelayDuration);
    }

    // Update is called once per frame
    void Update()
    {
        if (GetInput == false) return;
        if (Input.anyKey)
        {
            SceneManager.LoadScene(MapName);
        }
    }

    void SetInputToTrue()
    {
        GetInput = true;
    }
}
