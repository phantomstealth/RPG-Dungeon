using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyManager : MonoBehaviour
{
    public Text moneyText;
    public int currentGold=0;
    private SFXManager SFXMan;

    // Start is called before the first frame update
    void Start()
    {
        SFXMan = FindObjectOfType<SFXManager>();
        Refresh();
    }

    public void Refresh()
    {
        moneyText.text = "Gold: " + currentGold;
    }

    public void AddMoney(int goldToAdd)
    {
        currentGold += goldToAdd;
        PlayerPrefs.SetInt("CurrentMoney", currentGold);
        moneyText.text = "Gold: " + currentGold;
        SFXMan.Coin_Take.Play();

    }
}
