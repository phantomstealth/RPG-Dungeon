using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponData : MonoBehaviour
{
    public int minDamage;
    public int maxDamage;
    public float pushDistance;
    public float intervalStrike;
    public float multiplierCriticalStrike;
    public int minCriticalRoll;
    public int maxAttackRoll;

    public int attackRoll;
    public int randomDamage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public int AttackRoll()
    {
        attackRoll=Random.Range(0,maxAttackRoll);
        return attackRoll;
    }

    public int RandomDamage()
    {
        randomDamage = Random.Range(minDamage, maxDamage);
        return randomDamage;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
