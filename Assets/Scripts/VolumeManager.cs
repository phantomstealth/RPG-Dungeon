using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeManager : MonoBehaviour
{
    public VolumeController[] theVCobjects;

    public float MaxVolumeLevel=1.0f;
    public float CurVolumeLevel;

    // Start is called before the first frame update
    void Start()
    {
        theVCobjects = FindObjectsOfType<VolumeController>();
        if (CurVolumeLevel > MaxVolumeLevel) CurVolumeLevel = MaxVolumeLevel;
        for (int i = 0; i < theVCobjects.Length; i++)
        {
            theVCobjects[i].SetAudioLevel(CurVolumeLevel);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
