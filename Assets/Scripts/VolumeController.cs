using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeController : MonoBehaviour
{
    private AudioSource theAudio;

    private float audioLevel;
    public float defaultAudio=1;

    void Start()
    {
        theAudio = GetComponent<AudioSource>();   
    }

    public void SetAudioLevel(float Volume)
    {
        if (theAudio == null)
        {
            theAudio = GetComponent<AudioSource>();
        }
        audioLevel = defaultAudio * Volume;
        theAudio.volume = audioLevel;
    }
}
