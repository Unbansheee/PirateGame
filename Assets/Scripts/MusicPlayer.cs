using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum MusicState
{
    Playing,
    Paused,
    Stopped
}

public class MusicPlayer : MonoBehaviour
{
    public AudioClip[] songs;
    public AudioSource audioSource;
    public int currentSong;
    MusicState musicState;
    public bool isLooping;
    public bool isFading;
    public float fadeTime = 1f;
    public float fadeTimer;
    public float fadeVolume;
    public float fadeVolumeDelta;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        //set mixer for audio source

        PlayRandomSong(); 
        
        
    }

    // Update is called once per frame
    void Update()
    {

        //if song is 1 seconds from the end fade out
        if (audioSource.time >= audioSource.clip.length - 1)
        {
            PlayNextSong();
        }
    }
    
    //crossfade to next song
    public void PlayNextSong()
    {
        if (musicState == MusicState.Playing)
        {
            StopCoroutine(FadeOut());
            StartCoroutine(FadeOut());
        }
        else if (musicState == MusicState.Paused)
        {
            StopCoroutine(FadeOut());
            StartCoroutine(FadeOut());
        }
        else if (musicState == MusicState.Stopped)
        {
            PlayRandomSong();
        }
    }
    
    //fade music coroutine
    IEnumerator FadeOut()
    {
        musicState = MusicState.Paused;
        float timer = 0;
        while (timer < fadeTime)
        {
            timer += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(1, 0, timer / fadeTime);
            yield return null;
        }
        musicState = MusicState.Stopped;
        audioSource.Stop();
        PlayRandomSong();
        
    }


    void PlayRandomSong()
    {
        //play a random song from the array
        currentSong = Random.Range(0, songs.Length);
        audioSource.clip = songs[currentSong];
        audioSource.Play();
        musicState = MusicState.Playing;

    }

    void PlaySong(int index)
    {
            //play a song from the array
        currentSong = index;
        audioSource.clip = songs[currentSong];
        audioSource.Play();
        musicState = MusicState.Playing;
    }

    void NextSong()
    {
        //play the next song in the array
        currentSong++;
        if (currentSong >= songs.Length)
        {
            currentSong = 0;
        }
        audioSource.clip = songs[currentSong];
        audioSource.Play();
        musicState = MusicState.Playing;
    }

    void Pause()
    {
        audioSource.Pause();
        musicState = MusicState.Paused;
    }
    
    void Stop()
    {
        audioSource.Stop();
        musicState = MusicState.Stopped;
    }
    
    
}
