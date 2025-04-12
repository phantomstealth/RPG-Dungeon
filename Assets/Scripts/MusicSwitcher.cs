using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSwitcher : MonoBehaviour
{

    private MusicContoller theMC;

    public bool switchOnStart;

    public int newTrack;
    // Start is called before the first frame update
    void Start()
    {
        theMC = FindObjectOfType<MusicContoller>();
        if (switchOnStart)
        {
            theMC.SwitchTrack(newTrack);
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            theMC.SwitchTrack(newTrack);
            gameObject.SetActive(false);
        }
    }
}
