using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HurtEnemy : MonoBehaviour
{
    public int damageToGive;
    public float pushForce=2f;
    private int currentDamage;
    private PlayerContrloller pController;

    public GameObject damageBurst;
    public Transform hitpoint;
    public GameObject damageNumber;
    private PlayerStats thePS;
    private Transform swordTransform;


    // Start is called before the first frame update
    int ReturnIntChild(Transform parentObject,string nameObject)
    {
        int numChild=0;
        for (int i = 0; i < parentObject.childCount; i++)
        {
            if (parentObject.GetChild(i).name == nameObject) numChild = i;
        }
        return numChild;
    }

    void Awake()
    {
        thePS = FindObjectOfType<PlayerStats>();
        pController = FindObjectOfType<PlayerContrloller>();
        swordTransform = transform.parent.transform.GetChild(ReturnIntChild(transform.parent,"Sword"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bool blnCritical = false;
        if (collision.gameObject.tag == "Enemy")
        {
            int randomDamage;
            int roll = swordTransform.GetComponent<WeaponData>().AttackRoll();
            if (roll >= swordTransform.GetComponent<WeaponData>().minCriticalRoll)
            {
                blnCritical = true;
                randomDamage = (int)Mathf.CeilToInt(swordTransform.GetComponent<WeaponData>().maxDamage * swordTransform.GetComponent<WeaponData>().multiplierCriticalStrike);
            }
            else
                randomDamage = swordTransform.GetComponent<WeaponData>().RandomDamage();

            currentDamage = randomDamage + thePS.currentAttack;
            collision.gameObject.GetComponent<EnemyHealthManager>().HurtEnemy(currentDamage,pController.LastMove,pushForce);
            Instantiate(damageBurst, hitpoint.position, hitpoint.rotation);
            var clone = Instantiate(damageNumber, hitpoint.position, Quaternion.Euler(Vector2.zero));
            if (blnCritical)
            {
                clone.GetComponent<FloatingNumbers>().textFloat = "Critical";
                clone.GetComponent<FloatingNumbers>().color = Color.red;
                clone.transform.GetChild(0).GetComponent<Text>().fontSize = 2;
            }
            else
                clone.GetComponent<FloatingNumbers>().color = Color.white;
            clone.GetComponent<FloatingNumbers>().damageNumber = currentDamage;
        }
    }
}
