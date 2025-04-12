using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldPickUp : MonoBehaviour
{
    public int countGold;
    private MoneyManager theMM;

    // Start is called before the first frame update
    void Start()
    {
        theMM = FindObjectOfType<MoneyManager>();    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            theMM.AddMoney(countGold);
            Destroy(gameObject);
        }
    }
}
