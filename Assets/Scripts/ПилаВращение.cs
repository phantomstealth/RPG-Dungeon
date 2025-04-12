using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ПилаВращение : MonoBehaviour
{
    public int RotationPerMinute;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, RotationPerMinute * Time.deltaTime, Space.Self);
    }
}
