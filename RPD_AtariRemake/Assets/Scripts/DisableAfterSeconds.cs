using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableAfterSeconds : MonoBehaviour
{

    public float seconds = 1.0f;
    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    void OnEnable()
    {
        Invoke("DisableThis", seconds);
    }

    void DisableThis()
    {
        this.gameObject.SetActive(false);
    }
}
