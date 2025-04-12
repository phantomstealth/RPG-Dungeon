using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public GameObject DBox;
    public Text dText;
    public bool dialogActive;
    public string[] dialogLines;
    public int currentLine;
    private PlayerContrloller thePlayer;

    // Start is called before the first frame update
    void Start()
    {
        thePlayer = FindObjectOfType<PlayerContrloller>();
    }

    // Update is called once per frame
    void Update()
    {
        if (dialogActive && (Input.GetKeyDown(KeyCode.Space)||Input.GetButtonDown("Fire1")))
        {
            //DBox.SetActive(false);
            //dialogActive = false;
            currentLine++;
            if (currentLine >= dialogLines.Length)
            {
                DBox.SetActive(false);
                dialogActive = false;
                currentLine = 0;
                thePlayer.canMove = true;
            }
        }
        dText.text = dialogLines[currentLine];
    }

    public void ShowBox(string dialogue)
    {
        DBox.SetActive(true);
        dialogActive = true;
        dText.text = dialogue;
    }

    public void ShowDialog()
    {

        DBox.SetActive(true);
        dialogActive = true;
        thePlayer.canMove = false;
    }
}
