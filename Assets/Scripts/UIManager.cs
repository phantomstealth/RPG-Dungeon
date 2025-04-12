using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Slider HealthBar;
    public PlayerHealthManager PlayerHealth;
    public Text HealthText;
    public Text LevelText;
    private static bool UIExists;
    private PlayerStats thePlayerStats;

    // Start is called before the first frame update
    void Start()
    {
        if (!UIExists)
        {
            UIExists = true;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        thePlayerStats = GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        HealthBar.maxValue = PlayerHealth.PlayerMaxHealth;
        HealthBar.value = PlayerHealth.PlayerCurrentHealth;
        HealthText.text = "HP: " + PlayerHealth.PlayerCurrentHealth + "/" + PlayerHealth.PlayerMaxHealth;
        LevelText.text = "Lvl: " + thePlayerStats.currentLevel;
    }
}
