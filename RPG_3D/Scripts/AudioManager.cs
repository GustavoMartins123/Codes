using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [SerializeField] AudioSource audioPlayer;
    [SerializeField] AudioClip[] ambientMusics, soundsEffects;
    public bool canPlay = false;
    [Range(0, 1f)]
    [SerializeField] float volume;
    WaitForSeconds wait = new WaitForSeconds(0.12f);
    public bool inBattle;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        audioPlayer = GetComponent<AudioSource>();
        StartCoroutine(Active());
    }

    private void LateUpdate()
    {
        audioPlayer.volume = volume;
    }
    public void Music(int musicState, bool music)
    {
        if (music)
        {
            music = false;
            audioPlayer.clip = ambientMusics[musicState];
            StartCoroutine(Corroutine());
        }

    }
    public void Sounds(int musicState)
    {
        audioPlayer.PlayOneShot(soundsEffects[musicState]);
    }

    IEnumerator Corroutine()
    {
        yield return wait;
        audioPlayer.Play();
    }
    IEnumerator Active()
    {
        yield return wait;
        audioPlayer.mute = false;
    }
}
