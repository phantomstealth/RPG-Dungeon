using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingNumbers : MonoBehaviour
{
    public float moveSpeed;
    public int damageNumber;
    public string textFloat;
    public Text DisplayNumber;
    public Color color;

    // Start is called before the first frame update
    void Awake()
    {
        textFloat = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (textFloat!="")
            DisplayNumber.text = textFloat + " " + damageNumber+" damage";
        else
            DisplayNumber.text = "" + damageNumber;
        transform.position = new Vector3(transform.position.x,transform.position.y+moveSpeed*Time.deltaTime,transform.position.z);
        DisplayNumber.color = color;
    }
}
