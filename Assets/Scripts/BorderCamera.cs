using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LeoLuz.PlugAndPlayJoystick;

public class BorderCamera : MonoBehaviour
{
    //Увеличвать boxcollider не нужно, только, Transform, иначе будет глючить
    private BoxCollider2D bounds;
    private CameraController theCamera;
    public AnalogicKnob AnalogKnob;
    public string NameBoundsForMaps;
    public string Comments="Ширина не меньше 23, высота не меньше 10";

    void Start()
    {
        AnalogKnob = FindObjectOfType<AnalogicKnob>();
        AnalogKnob.TouchEnd();
        theCamera = FindObjectOfType<CameraController>();
        bounds = GetComponent<BoxCollider2D>();
        theCamera.SetBounds(bounds);
        theCamera.SetNameBound(NameBoundsForMaps);
    }
}
