using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public QuestObject[] quests;
    public bool[] QuestCompleted;
    public DialogueManager theDM;

    public string ItemCollected;
    public string EnemyKilled;

    // Start is called before the first frame update
    void Start()
    {
        QuestCompleted = new bool[quests.Length];
    }

    public void ShowTextQuest(string questText)
    {
        theDM.dialogLines = new string[1];
        theDM.dialogLines[0] = questText;
        theDM.ShowDialog();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
