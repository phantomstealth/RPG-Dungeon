
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(SpriteRenderer))]
public class Hazard : MonoBehaviour
{
    public Sprite hitSprite;
    private SpriteRenderer spriteRenderer;

    // Use this for initialization
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        //GameManager.instance.PlayShootingSfx();
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.transform.tag == "Player")
        {
            spriteRenderer.sprite = hitSprite;
            coll.gameObject.GetComponent<PlayerHealthManager>().HurtPlayer(100,Vector2.zero,0);
            //GameManager.instance.RestartGame(3f);
        }
        else
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject, 0.1f);
                Destroy(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}