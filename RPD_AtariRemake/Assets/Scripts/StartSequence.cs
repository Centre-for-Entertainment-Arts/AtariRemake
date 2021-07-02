using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StartSequence : MonoBehaviour
{
    [SerializeField]
    TMP_Text _Text;

    private AudioSource _source;

    [SerializeField]
    private GameObject _startScreen;

    int TutorialPlayed;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {

        _source = GetComponent<AudioSource>();
        TutorialPlayed = PlayerPrefs.GetInt("TutorialPlayed", 0);
        StartScreen();
    }

    public void StartScreen()
    {
        if (TutorialPlayed == 0) //If tutorial has not been played. play it now
        {
            _startScreen.SetActive(true);
            PlayerPrefs.SetInt("TutorialPlayed", 1);
        }
        else
        {
            CountDown();
        }
    }

    public void CountDown()
    {
        StartCoroutine(countDown());
    }

    IEnumerator countDown()
    {
        _Text.text = "3";
        _source.pitch = 1.0f;
        _source.Play();
        yield return new WaitForSeconds(1.0f);

        _Text.text = "2";
        _source.pitch = 2.0f;
        _source.volume = 2.0f;
        _source.Play();

        yield return new WaitForSeconds(1.0f);

        _Text.text = "1";
        _source.pitch = 3.0f;
        _source.volume = 3.0f;
        _source.Play();

        yield return new WaitForSeconds(1.0f);

        _Text.text = "GO";
        _source.pitch = 4.0f;
        _source.volume = 4.0f;
        _source.Play();

        yield return new WaitForSeconds(1.0f);

        _source.pitch = 1.0f;
        _source.volume = 1.0f;
        _Text.text = "";

        var spawners = GameObject.FindObjectsOfType<SpawnBlock>();

        foreach (var item in spawners)
        {
            item.SpawnSingleBlock();
        }
    }
}
