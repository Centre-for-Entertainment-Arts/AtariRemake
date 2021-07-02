using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoOnApplicationQuit : MonoBehaviour
{
    /// <summary>
    /// Callback sent to all game objects before the application is quit.
    /// </summary>
    void OnApplicationQuit()
    {
        PlayerPrefs.DeleteAll();
    }
}
