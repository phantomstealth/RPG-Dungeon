using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStartPoint : MonoBehaviour
{
    private PlayerContrloller thePlayer;
    private CameraController theCamera;
    public Vector2 StartDirection;
    //public bool ChangePosition;

    public string NamePoint;


    // Start is called before the first frame update
    void Start()
    {
        thePlayer = FindObjectOfType<PlayerContrloller>();

        if (thePlayer.startPoint == NamePoint)
        {
            thePlayer.transform.position = transform.position;
            thePlayer.LastMove = StartDirection;

            theCamera = FindObjectOfType<CameraController>();
            theCamera.transform.position = new Vector3(transform.position.x, transform.position.y, theCamera.transform.position.z);
        }
    }

    // Update is called once per frame
    
}
