using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int currentLevel;
    public int currentExp;
    public int[] toLevelUp;

    public int[] HPLevels;
    public int[] AttackLevels;
    public int[] DefenceLevels;

    public int currentHP;
    public int currentAttack;
    public int currentDefence;

    private PlayerHealthManager thePlayerHealth;

    // Start is called before the first frame update
    void Start()
    {
        thePlayerHealth = FindObjectOfType<PlayerHealthManager>(); 

        currentHP = HPLevels[currentLevel];
        currentAttack = AttackLevels[currentLevel];
        currentDefence = DefenceLevels[currentLevel];
    }

    // Update is called once per frame
    void Update()
    {
        if (currentExp >= toLevelUp[currentLevel])
        {
            LevelUp();
        }
    }

    public void UpExperience(int expToGive)
    {
        currentExp += expToGive;
    }

    public void LevelUp()
    {
        currentLevel++;
        currentHP = HPLevels[currentLevel];
        thePlayerHealth.PlayerMaxHealth = currentHP;
        thePlayerHealth.PlayerCurrentHealth += currentHP - HPLevels[currentLevel - 1];

        currentAttack = AttackLevels[currentLevel];
        currentDefence = DefenceLevels[currentLevel];
    }
}
