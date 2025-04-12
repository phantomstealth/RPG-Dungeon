using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayer : MonoBehaviour
{
    public int damageToGive;
    private int currentDamage;
    private PlayerStats thePS;
    public float strengthHurt=2f;
    private EnemyController sController;
    // Start is called before the first frame update
    void Start()
    {
        thePS = FindObjectOfType<PlayerStats>();
        sController = GetComponent<EnemyController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            currentDamage = damageToGive - thePS.currentDefence;
            if (currentDamage < 1) currentDamage = 1;
            collision.gameObject.GetComponent<PlayerHealthManager>().HurtPlayer(currentDamage, sController.directionMove, strengthHurt);
        }
    }
}
