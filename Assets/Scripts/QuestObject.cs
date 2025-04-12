using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestObject : MonoBehaviour
{
    public QuestManager theQM;
    public int QuestNumber;
    public string startText;
    public string endText;

    public bool itemQuest;
    public string itemName;

    public bool EnemyKilledQuest;
    public string NameEnemy;
    public int EnemyNumKill;
    public int EnemyCountKill;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (itemQuest)
        {
            if (itemName == theQM.ItemCollected)
            {
                theQM.ItemCollected = null;
                theQM.quests[QuestNumber].EndQuest();
            }        
        }
        if (EnemyKilledQuest)
        {
            if (NameEnemy == theQM.EnemyKilled)
            {
                theQM.EnemyKilled = null;
                EnemyCountKill++;
                if (EnemyCountKill >= EnemyNumKill)
                {
                    theQM.quests[QuestNumber].EndQuest();
                }
            }
        }
    }

    public void StartQuest()
    {
        theQM.ShowTextQuest(startText);
    }

    public void EndQuest()
    {
        theQM.QuestCompleted[QuestNumber] = true;
        gameObject.SetActive(false);
        theQM.ShowTextQuest(endText);

    }
}
