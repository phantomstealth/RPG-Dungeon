using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestItem : MonoBehaviour
{
    private QuestManager theQM;
    public string nameItem;
    public int questNumber;
    // Start is called before the first frame update
    void Start()
    {
        theQM = FindObjectOfType<QuestManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name=="Player")
            if (!theQM.QuestCompleted[questNumber] && theQM.quests[questNumber].gameObject.activeSelf)
                if (nameItem == theQM.quests[questNumber].itemName)
                {
                    theQM.ItemCollected = nameItem;
                    gameObject.SetActive(false);
                }
    }
}
