using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNewArea : MonoBehaviour
{
    private PlayerStartPoint thePlayerStartPoint;
    public string LevelToLoad;
    public string ExitPoint;
    private JoyChangePicture buttonA;

    void Start()
    {
        buttonA = GameObject.FindGameObjectWithTag("JoyButtonA").GetComponent<JoyChangePicture>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerContrloller>().startPoint = ExitPoint;

            //thePlayerStartPoint = FindObjectOfType<PlayerStartPoint>();
            //if (thePlayerStartPoint.ChangePosition)
            //    thePlayerStartPoint.transform.position = collision.gameObject.transform.position;
            SceneManager.LoadScene(LevelToLoad);
            buttonA.ChangePictureonButton("Attack");
            //MobCon.Zero();
        }
    }
}
