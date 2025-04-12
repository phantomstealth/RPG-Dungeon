using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static bool AudioExists;

    void Start()
    {
        if (!AudioExists)
        {
            AudioExists = true;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    void Update()
    {
        
    }
}
