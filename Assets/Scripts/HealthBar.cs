using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public float currentHeatlh;
    private Transform foreGround;
    private float sizeFullHealth;
    private float sizeCurHealth;
    // Start is called before the first frame update

    void Awake()
    {
        if (foreGround != null) return;
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).name == "ForeGround") foreGround = transform.GetChild(i).transform;
        }
        sizeFullHealth = foreGround.localScale.x;
    }

    // Update is called once per frame
    public void ChangeHealth(float maxHealth,float curHealth)
    {
        if (foreGround == null) Awake();
        currentHeatlh = curHealth;
        sizeCurHealth= curHealth/maxHealth;
        foreGround.localScale = new Vector3(sizeFullHealth*sizeCurHealth,foreGround.localScale.y,foreGround.localScale.z);
    }
}
