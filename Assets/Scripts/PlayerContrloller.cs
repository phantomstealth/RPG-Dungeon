using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContrloller : MonoBehaviour
{
    public string CurrentState="Attack";

    public float moveSpeed;
    public float currentMoveSpeed;
    public float diagMoveSpeed;

    private Animator anim;
    private Animator animWeapon;

    private bool PlayerMoving;

    public Vector2 LastMove;
    private Vector2 moveInput;

    private Rigidbody2D myRigBody;
    private static bool PlayerExists;
    public bool attacking;
    public float attackTime;
    public string startPoint;
    private float attackTimeCounter;
    private SFXManager SFXMan;
    private JoyChangePicture jchangepic;

    public bool canMove;

    
    public float pushForce;
    public float pushTime = 0.2f;
    public float pushTimeCounter;
    public Vector2 directionPush;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent <Animator>();
        animWeapon = transform.GetChild(0).gameObject.GetComponent<Animator>();
        myRigBody = GetComponent<Rigidbody2D>();
        SFXMan = FindObjectOfType<SFXManager>();
        jchangepic = FindObjectOfType<JoyChangePicture>();
        canMove = true;
        if (!PlayerExists)
        {
            PlayerExists = true;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ChangePicturesOnButton(string currentState)
    {
        jchangepic.ChangePictureonButton(currentState);
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMoving = false;

        if (!canMove)
        {
            myRigBody.velocity = Vector2.zero;
            return;
        }

        float hMove = Input.GetAxisRaw("Horizontal");
        float vMove = Input.GetAxisRaw("Vertical");

        if (pushTimeCounter>0)
        {
            myRigBody.velocity = new Vector2(directionPush.x * pushForce, directionPush.y * pushForce);
            pushTimeCounter -= Time.deltaTime;
            return;
        }

        if (!attacking)
        {
            moveInput = new Vector2(hMove, vMove).normalized;
            if (moveInput != Vector2.zero)
            {
                myRigBody.velocity = new Vector2(moveInput.x * moveSpeed, moveInput.y * moveSpeed);
                PlayerMoving = true;
                LastMove = moveInput;
                if (!SFXMan.Walk.isPlaying) SFXMan.Walk.Play();
            }
            else
            {
                myRigBody.velocity = Vector2.zero;
            }

            if (Input.GetButtonDown("Fire1")&(CurrentState=="Attack"))
            {
                attacking = true;
                LastMove = transform.GetChild(0).gameObject.GetComponent<WeaponImpact>().directMain;
                SFXMan.PlayerAttack.Play();
                attackTimeCounter = attackTime;
                myRigBody.velocity = Vector2.zero;
                anim.SetBool("Attacking", attacking);
                animWeapon.SetBool("Attacking", attacking);
            }

        }

        if (attackTimeCounter > 0)
        {
            attackTimeCounter -= Time.deltaTime;
        }
        else if (attackTimeCounter <= 0)
        {
            attacking = false;
            anim.SetBool("Attacking", attacking);
            animWeapon.SetBool("Attacking", attacking);
        }

        anim.SetFloat("moveX",hMove);
        anim.SetFloat("moveY", vMove);

        anim.SetBool("PlayerMoving", PlayerMoving);
        anim.SetFloat("LastMoveX", LastMove.x);
        anim.SetFloat("LastMoveY", LastMove.y);
    }
}
