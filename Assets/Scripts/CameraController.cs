using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject followTarget;
    private Vector3 TargetPos;
    public float moveSpeed;

    private static bool CameraExists;

    public BoxCollider2D boundBox;
    public Vector3 minBounds;
    public Vector3 maxBounds;

    private Camera theCamera;
    public float halfWidth;
    public float halfHeight;

    public string NameCurretBounds;
    public float x;
    public float y;

    // Start is called before the first frame update
    void Start()
    {
        if (!CameraExists)
        {
            CameraExists = true;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        if (boundBox == null)
        {
            boundBox = FindObjectOfType<BorderCamera>().GetComponent<BoxCollider2D>();
            //ChangeSizeBox();
        }

        minBounds = boundBox.bounds.min;
        maxBounds = boundBox.bounds.max;

        theCamera = GetComponent<Camera>();
        halfHeight = theCamera.orthographicSize;
        halfWidth = halfHeight * Screen.width / Screen.height;
    }

    // Update is called once per frame
    void Update()
    {
        TargetPos = new Vector3(followTarget.transform.position.x, followTarget.transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, TargetPos, moveSpeed * Time.deltaTime);
        //Vector3 velocity = TargetPos - transform.position;
        //transform.position = Vector3.SmoothDamp(transform.position, TargetPos, ref velocity, 1.0f, moveSpeed * Time.deltaTime);


        if (boundBox == null)
        {
            boundBox = FindObjectOfType<BorderCamera>().GetComponent<BoxCollider2D>();
            //ChangeSizeBox();
            minBounds = boundBox.bounds.min;
            maxBounds = boundBox.bounds.max;
        }

        halfHeight = theCamera.orthographicSize;
        halfWidth = halfHeight * Screen.width / Screen.height;

        CorrectBoundsPlusWidth();


        //if ((minBounds.x + halfWidth) > (maxBounds.x - halfWidth)) //если 4+8>19-8
        //{
        //    maxBounds.x= (minBounds.x + 2*halfWidth);                  //19=4+8*2=20
        //}
        float clampedX = Mathf.Clamp(transform.position.x, minBounds.x + halfWidth, maxBounds.x - halfWidth);
        float clampedY = Mathf.Clamp(transform.position.y, minBounds.y + halfHeight, maxBounds.y - halfHeight);

        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
        x = clampedX;
        y = clampedX;
    }

    private void CorrectBoundsPlusWidth()
    {
        float widthbound = maxBounds.x - minBounds.x;
        float heightbound = maxBounds.y - minBounds.y;
        if (widthbound < halfWidth * 2)
        {
            float changeValue = (halfWidth * 2 - widthbound) / 2;
            minBounds.x -= changeValue;
            maxBounds.x += changeValue;
        }
        if (heightbound < halfHeight * 2)
        {
            float changeValue = (halfHeight * 2 - heightbound) / 2;
            minBounds.y -= changeValue;
            maxBounds.y += changeValue;
        }
    }

    private void ChangeSizeBox()
    {
        if (boundBox.size.x < 23)
            boundBox.size=new Vector3(23,boundBox.size.y);
        if (boundBox.size.y<10)
            boundBox.size = new Vector3(boundBox.size.x,10);
    }

    public void SetBounds(BoxCollider2D newBounds)
    {
        boundBox = newBounds;
        minBounds = boundBox.bounds.min;
        maxBounds = boundBox.bounds.max;
    }

    public void SetNameBound(string nameBounds)
    {
        NameCurretBounds = nameBounds;
    }
}
