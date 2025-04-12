using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExistObject : MonoBehaviour
{
    private static bool JoyExists;

    // Start is called before the first frame update
    void Start()
    {
        if (!JoyExists)
        {
            JoyExists = true;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
