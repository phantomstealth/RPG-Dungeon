using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicContoller : MonoBehaviour
{
    public AudioSource[] MusicTracks;

    public int currentTrack;

    public float curVolume;

    public bool musicCanPlay;

    // Update is called once per frame
    void Update()
    {
        if (musicCanPlay)
        {
            if (!MusicTracks[currentTrack].isPlaying)
            {
                MusicTracks[currentTrack].Play();
            }
        }
        else
        {
            MusicTracks[currentTrack].Stop();
        }

    }

    public void ChangeVolumeMusic(float Volume)
    {
        curVolume = Volume;
        MusicTracks[currentTrack].volume = curVolume;
    }

    public void SwitchTrack(int newTrack)
    {
        MusicTracks[currentTrack].Stop();
        currentTrack = newTrack;
        MusicTracks[currentTrack].volume = curVolume;
        MusicTracks[currentTrack].Play();
    }
}
