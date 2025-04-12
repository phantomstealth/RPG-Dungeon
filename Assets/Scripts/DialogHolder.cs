using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogHolder : MonoBehaviour
{
    public string[] dialogLine;
    private DialogueManager dManager;
    private SFXManager SFXMan;
    private bool inTrigger;
    private JoyChangePicture buttonA;
    public GameObject objectNPC;

    // Start is called before the first frame update
    void Start()
    {
        dManager = FindObjectOfType<DialogueManager>();
        SFXMan = FindObjectOfType<SFXManager>();
        buttonA = GameObject.FindGameObjectWithTag("JoyButtonA").GetComponent<JoyChangePicture>();
        objectNPC = gameObject.transform.parent.gameObject;
    }

    // Update is called once per frame

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            inTrigger = true;
            buttonA.ChangePictureonButton("Talk");
            objectNPC.GetComponent<SpriteRenderer>().color=Color.yellow;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            inTrigger = false;
            buttonA.ChangePictureonButton("Attack");
            objectNPC.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    void Update()
    {
        if (inTrigger && Input.GetButtonDown("Fire1"))
        {
            //dManager.ShowBox(dialogue);
            if (!dManager.dialogActive)
            {
                if (transform.parent.GetComponent<VillageMovement>() != null) transform.parent.GetComponent<VillageMovement>().canMove = false;
                inTrigger = false;
                dManager.dialogLines = dialogLine;
                dManager.currentLine = 0;
                dManager.ShowDialog();
                SFXMan.Blabla.Play();
            }
        }
    }
}
