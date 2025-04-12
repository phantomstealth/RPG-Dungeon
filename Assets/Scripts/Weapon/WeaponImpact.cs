using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponImpact : MonoBehaviour
{
    public GameObject targetForWeapon;
    public Vector2 pointTarget;
    public float offset=270;
    public Vector2 directMain;

    void ChangeSwordPosition()
    {
        float angle = Mathf.Atan2(directMain.y, directMain.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + offset));
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            //gameObject.transform.GetChild(0).gameObject.SetActive(true);
            targetForWeapon = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            //gameObject.transform.GetChild(0).gameObject.SetActive(false);
            targetForWeapon = null;
        }
    }

    void Update()
    {
        if (targetForWeapon != null)
        {
            pointTarget = new Vector2(targetForWeapon.transform.position.x, targetForWeapon.transform.position.y);
            directMain = new Vector2(pointTarget.x - transform.position.x, pointTarget.y - transform.position.y);
            ChangeSwordPosition();
        }
        else
        {
            directMain = transform.parent.gameObject.GetComponent<PlayerContrloller>().LastMove;
            ChangeSwordPosition();
        }
    }


}
