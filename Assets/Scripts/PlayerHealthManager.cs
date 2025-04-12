using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealthManager : MonoBehaviour
{
    public int PlayerMaxHealth;
    public int PlayerCurrentHealth;
    public GameObject damageNumber;
    private bool FlashActivate;
    public float FlashLength;
    private float FlashCounter;
    private SpriteRenderer spriteRenderer;
    private SFXManager SFXMan;
    private GameManager GameMan;

    private PlayerContrloller thePC;


    public GameObject bloodSprayPrefab;

    //public float TimeToReload;

    // Start is called before the first frame update
    void Start()
    {
        SFXMan = FindObjectOfType<SFXManager>();
        GameMan = FindObjectOfType<GameManager>();
        PlayerCurrentHealth = PlayerMaxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
        thePC = FindObjectOfType<PlayerContrloller>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerCurrentHealth <= 0)
        {
            SFXMan.PlayerDead.Play();
            StartCoroutine(SprayBlood(3f, gameObject.transform.position, gameObject));
            GameMan.RestartGame(3f);
            gameObject.SetActive(false);
            /* TimeToReload -= Time.deltaTime;
            if (TimeToReload < 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                gameObject.SetActive(true);
            }
            */
        }

        if (FlashActivate)
        {
            if (FlashCounter > FlashLength * 0.66)
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0f);
            else if (FlashCounter > FlashLength * 0.33)
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1f);
            else if (FlashCounter > 0)
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0f);
            else
            {
                FlashActivate = false;
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1f);
            }
            FlashCounter -= Time.deltaTime;
        }
        
    }

    private IEnumerator SprayBlood(float delay, Vector2 position, GameObject player)
    {
        var bloodSpray = (GameObject)Instantiate(bloodSprayPrefab, position, Quaternion.identity);
        Destroy(bloodSpray, 3f);
        //Destroy(player);
        yield return new WaitForSeconds(delay);
    }


    public void HurtPlayer(int damageToGive,Vector2 directionPush, float pushForce)
    {
        SFXMan.PlayerHurt.Play();
        PlayerCurrentHealth -= damageToGive;
        if (PlayerCurrentHealth < 0) PlayerCurrentHealth = 0;
        var clone = Instantiate(damageNumber, transform.position, Quaternion.Euler(Vector2.zero));
        clone.GetComponent<FloatingNumbers>().damageNumber = damageToGive;
        clone.GetComponent<FloatingNumbers>().color = Color.red;
        FlashActivate = true;
        FlashCounter = FlashLength;

        thePC.directionPush = directionPush;
        thePC.pushTimeCounter = thePC.pushTime;
        thePC.pushForce = pushForce;
    }

    public void SetMaxHealth()
    {
        PlayerCurrentHealth = PlayerMaxHealth;
    }
}
