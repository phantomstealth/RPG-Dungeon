using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MobileControllerButton : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    public bool PressButton=false;
    public bool DownButton = false;

    public virtual void OnPointerDown(PointerEventData ped)
    {
        print(tag);
        PressButton = true;
        DownButton = true;
    }
    public virtual void OnPointerUp(PointerEventData ped)
    {
        print(tag);
        PressButton = false;
        DownButton = false;
    }

    public bool GetKeyPress()
    {
        return PressButton;
    }

    public bool GetKeyDown()
    {
        if (DownButton)
        {
            DownButton = false;
            return true;
        }
        return false;
        
    }

    public bool GetKeyUp()
    {
        return !PressButton;
    }

    // Use this for initialization
    void Start () {
		
	}
	
}
