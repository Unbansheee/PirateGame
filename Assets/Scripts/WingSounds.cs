using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WingSounds : MonoBehaviour
{
    ShipControls shipControls;

    public AudioClip idle;
    public AudioClip flap;
    public AudioSource audioSource;

    private int gear;
    // Start is called before the first frame update
    void Start()
    {
        UpdateAudioClip();
        audioSource.Play();
    }
    
    void Awake()
    {
        shipControls = transform.parent.GetComponent<ShipControls>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (shipControls.getGear() != gear)
        {
            UpdateAudioClip();

            audioSource.Play();

        }
    }

    void UpdateAudioClip()
    {
        switch (shipControls.getGear())
        {
            case 0:
                audioSource.clip = idle;
                audioSource.pitch = 1;
                audioSource.volume = 1;
                break;

            case 1:
                audioSource.clip = flap;
                audioSource.pitch = 1;
                audioSource.volume = 0.7f;
                break;
            case -1:
                audioSource.clip = flap;
                audioSource.pitch = 0.95f;
                audioSource.volume = 0.7f;
                break;
            case 2:
                audioSource.clip = flap;
                audioSource.pitch = 1.1f;
                audioSource.volume = 0.8f;
                break;
            case 3:
                audioSource.clip = flap;
                audioSource.pitch = 1.2f;
                audioSource.volume = 0.9f;
                break;
        }
        gear = shipControls.getGear();
    }
}
