using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyVisor : MonoBehaviour
{
    private GameObject EnemyMain;
    private SpriteRenderer spriteRenderer;


    void Start()
    {
        EnemyMain = transform.parent.gameObject;
        spriteRenderer = EnemyMain.GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerContrloller>().ChangePicturesOnButton("Attack");
            spriteRenderer.color = Color.magenta;
            EnemyMain.GetComponent<EnemyController>().targetObject = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            spriteRenderer.color = Color.white;
        EnemyMain.GetComponent<EnemyController>().targetObject = null;
    }


    void Update()
    {
        
    }
}
