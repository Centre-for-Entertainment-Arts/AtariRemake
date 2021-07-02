using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class TextGenerator : MonoBehaviour
{
    public string Message;

    [SerializeField]
    private TMP_Text _text;

    [SerializeField]
    private AudioClip _textSFX;

    private AudioSource _source;

    [SerializeField]
    private GameObject _parent;

    private bool canGetInput = false;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        _source = GetComponent<AudioSource>();
        _source.clip = _textSFX;
        _text.text = "";
        StartCoroutine(PlayText());
    }

    public void PlayTextAnimation(string message)
    {
        // Message = message;
        _text.text = "";
        StartCoroutine(PlayText());
    }

    IEnumerator PlayText()
    {
        foreach (char c in Message)
        {
            _source.Play();
            _text.text += c;
            yield return new WaitForSeconds(0.060f);
        }
        canGetInput = true;
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if (canGetInput)
        {
            if (Input.anyKey)
            {
                GameObject.FindObjectOfType<StartSequence>().CountDown();
                _parent.SetActive(false);
            }
        }
    }
}