using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//[RequireComponent(typeof(SpriteRenderer))]
public class JoyChangePicture : MonoBehaviour
{
    public Sprite TalkImage;
    public Sprite AttackImage;
    public Sprite QuestImage;
    private Image buttonImage;
    private PlayerContrloller pcontr;

    // Start is called before the first frame update
    void Start()
    {
        buttonImage = GetComponent<Image>();
        pcontr = FindObjectOfType<PlayerContrloller>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangePictureonButton(string PictureName)
    {
        if ((PictureName == "Attack") || (PictureName==null))
            buttonImage.sprite = AttackImage;
        else if (PictureName == "Talk")
            buttonImage.sprite = TalkImage;
        else if (PictureName == "Quest")
            buttonImage.sprite = QuestImage;
        pcontr.CurrentState = PictureName;
         
    }
}
