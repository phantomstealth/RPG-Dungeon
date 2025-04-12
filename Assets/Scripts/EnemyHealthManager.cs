using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{
    public GameObject healthBar;

    public int MaxHealth;
    int curHealthPush = 0;

    public int CurrentHealth
    {
        get { return curHealthPush; }
    set
        {
            curHealthPush = value;
            if (healthBar == null) return;
            healthBar.GetComponent<HealthBar>().ChangeHealth(MaxHealth, curHealthPush);
        }
    }

    public int expToGive;
    public string QuestNameEnemy;
    private PlayerStats thePlayerStats;
    private EnemyController sController;
    private QuestManager theQM;
    void Awake()
    {
        CurrentHealth = MaxHealth;
    }
    //public float TimeToReload;

    private void Start()
    {
    // Start is called before the first frame update
    thePlayerStats = FindObjectOfType<PlayerStats>();
        sController = GetComponent<EnemyController>();
        theQM = FindObjectOfType<QuestManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentHealth <= 0)
        {
            theQM.EnemyKilled = QuestNameEnemy;
            Destroy(gameObject);
            thePlayerStats.UpExperience(expToGive);
        }

    }

    public void HurtEnemy(int damageToGive,Vector2 directionPush,float pushForce)
    {
        CurrentHealth -= damageToGive;
        sController.directionPush = directionPush;
        sController.pushForce = pushForce;
        sController.pushTimeCounter = sController.pushTime;
    }

    public void SetMaxHealth(float maxHealth,float currentHealth)
    {
        CurrentHealth = MaxHealth;
    }
}
