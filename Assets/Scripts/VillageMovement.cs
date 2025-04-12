using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillageMovement : MonoBehaviour
{
    private Rigidbody2D myRigidbody;

    private bool isWalking;
    public float moveSpeed;
    public float waitTime;
    private float waitCounter;
    public float walkTime;
    private float walkCounter;
    private int walkDirection;

    public Collider2D walkZone;
    private bool hasWalkZone;
    private Vector2 minWalkZone;
    private Vector2 maxWalkZone;

    private DialogueManager dManager;
    public bool canMove;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        dManager = FindObjectOfType<DialogueManager>();
        walkCounter = walkTime;
        waitCounter = waitTime;
        canMove = true;

        if (walkZone != null)
        {

            minWalkZone = walkZone.bounds.min;
            maxWalkZone = walkZone.bounds.max;
            hasWalkZone = true;
        }
        ChangeDirection();
    }

    // Update is called once per frame
    void Update()
    {
        if (!dManager.dialogActive)
        {
            canMove = true;
        }
        if (!canMove)
        {
            myRigidbody.velocity = Vector2.zero;
            return;
        }

        if (isWalking)
        {
            walkCounter -= Time.deltaTime;
            switch (walkDirection)
            {
                case 0:
                    myRigidbody.velocity = new Vector2(0, moveSpeed);
                    if (hasWalkZone && transform.position.y+1>minWalkZone.y) walkCounter = 0;
                    break;
                case 1:
                    myRigidbody.velocity = new Vector2(moveSpeed,0);
                    if (hasWalkZone && transform.position.x +1> maxWalkZone.x) walkCounter = 0;
                    break;
                case 2:
                    myRigidbody.velocity = new Vector2(0, -moveSpeed);
                    if (hasWalkZone && transform.position.y-1 < maxWalkZone.y) walkCounter = 0;
                    break;
                case 3:
                    myRigidbody.velocity = new Vector2(-moveSpeed,0);
                    if (hasWalkZone && transform.position.x-1 < minWalkZone.x) walkCounter = 0;
                    break;
            }
            if (walkCounter <= 0)
            {
                isWalking = false;
                waitCounter = waitTime;
            }
        }
        else
        {
            waitCounter -= Time.deltaTime;
            myRigidbody.velocity = Vector2.zero;
            if (waitCounter <= 0)
            {
                ChangeDirection();
            }
        }

    }

    private void ChangeDirection()
    {
        walkDirection=Random.Range(0, 4);
        walkCounter = walkTime;
        isWalking = true;

    }
}
