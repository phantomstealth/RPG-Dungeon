using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{
    private Rigidbody2D myRigidbody;
    private bool moving;
    public Vector3 directionMove;

    public float moveSpeed;
    public float timeBeetweenMove;
    public float timeToMove;

    private float timeBeetweenMoveCounter;
    private float timeToMoveCounter;

    public GameObject targetObject;

    public Vector2 directionPush;
    public float pushForce;
    public float pushTimeCounter;
    public float pushTime = 2f;


    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        //TimeBeetweenMoveCounter = TimeBeetweenMove;
        //TimeToMoveCounter = TimeToMove;
        timeBeetweenMoveCounter = Random.Range(timeBeetweenMove * 0.75f, timeBeetweenMove * 1.25f);
        timeToMoveCounter = Random.Range(timeToMove * 0.75f, timeToMove * 1.25f);
    }

    // Update is called once per frame

    public void LoadData(Save.EnemySaveData save)
    {
        transform.position = new Vector3(save.Position.x, save.Position.y, save.Position.z);
        directionMove = new Vector3(save.Direction.x, save.Direction.y, save.Direction.z);
        GetComponent<EnemyHealthManager>().CurrentHealth = save.curHealth;
    }


    void Update()
    {
        if (pushTimeCounter > 0)
        {
            myRigidbody.velocity = new Vector2(directionPush.x * pushForce, directionPush.y * pushForce);
            //Debug.Log(myRigidbody.velocity);
            pushTimeCounter -= Time.deltaTime;
            return;
        }

        if (moving)
        {
            timeToMoveCounter -= Time.deltaTime;
            if (targetObject != null)
            {
                directionMove =new Vector2(targetObject.transform.position.x-transform.position.x, targetObject.transform.position.y-transform.position.y);
            }
            myRigidbody.velocity = directionMove;
            if (timeToMoveCounter < 0f)
            {
                moving = false;
                //TimeBeetweenMoveCounter = TimeBeetweenMove;
                timeBeetweenMoveCounter = Random.Range(timeBeetweenMove * 0.75f, timeBeetweenMove * 1.25f);
            }
        }
        else
        {
            myRigidbody.velocity = Vector2.zero;
            timeBeetweenMoveCounter -= Time.deltaTime;
            if (timeBeetweenMoveCounter < 0f)
            {
                moving = true;
                //TimeToMoveCounter = TimeToMove;
                timeToMoveCounter = Random.Range(timeToMove * 0.75f, timeToMove * 1.25f);
                directionMove = new Vector3(Random.Range(-1f, 1f)*moveSpeed, Random.Range(-1f, 1f)*moveSpeed, 0);
            }
        }
    }
}
