using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOne : Player
{ 
    private Inventory inventory;

    private float horizontal;
    private float vertical;

    [Header("MOVEMENT")]
    [SerializeField] private float ST;
    private float speed;

    public bool stopMovement, stopShooting, inDialogue, isCrouching;
    private bool grounded;

    [Header("SHOOTING")]
    [SerializeField] private Transform standFirePos;
    [SerializeField] private Transform crouchFirePos;
    private Transform firePos;

    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject smoke;

    [Header("FALCON")]
    [SerializeField] private GameObject falcon;
    public bool isFalcon;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        inventory = GetComponent<Inventory>();
    }

    void Start()
    {
        speed = ST;
        health = HP;
        firePos = standFirePos;
    }
     
    void Update()
    {
        InputCheck();
        Flip();
        Move(); 
        Die();
    }

    private void InputCheck()
    {
        if (!stopShooting)
        {
            if (Input.GetButtonDown("Fire") && !stopMovement && !inDialogue && grounded)
            {

                if (inventory.bullets > 0)
                {
                    anim.SetTrigger("shoot");
                    inventory.ReduceBullet(1);
                    FindObjectOfType<AudioManager>().Play("shoot");

                    //BULLET
                    GameObject b = Instantiate(bullet, firePos.position, transform.rotation);
                    b.GetComponent<bullet>().facingRight = this.facingRight;
                    b.GetComponent<bullet>().suicideTime = 2f;

                    //SMOKE
                    Instantiate(smoke, firePos.position, transform.rotation);

                }
                else
                {
                    FindObjectOfType<AudioManager>().Play("empty");
                } 
            } 
        }
        else
        {
            if (Input.GetButtonDown("Fire") && !isFalcon && !stopMovement && !inDialogue && grounded)
            {
                isFalcon = true;
                anim.SetTrigger("throw");
                FindObjectOfType<AudioManager>().Play("throw");

                GameObject birdy = Instantiate(falcon, firePos.position, transform.rotation); 
                birdy.GetComponent<Falcon>().facingRight = facingRight; 
            }
        }


        //CROUCH
        if (!inDialogue && grounded)
        {
            if (Input.GetButton("Down"))
            {
                anim.SetBool("crouch", true);
                isCrouching = true;
                firePos = crouchFirePos;
            }


            if (Input.GetButtonUp("Down"))
            {
                anim.SetBool("crouch", false);
                isCrouching = false;
                firePos = standFirePos;
            }
        }
    }

    private void Move()
    {
        grounded = GetComponent<PlayerJump>().grounded;

        if (!stopMovement && !inDialogue && !isFalcon && !isCrouching)
        {
            horizontal = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
            vertical = rb.velocity.y;

            rb.velocity = new Vector2(horizontal, vertical);
            anim.SetFloat("speed", Mathf.Abs(horizontal));
        }
        else
        {
            if (grounded)
            {
                rb.velocity = new Vector2(0f, rb.velocity.y);           //blockiram samo X-axis npr. samo da bi padao ako je u vazduhu
                anim.SetFloat("speed", 0f);
            }
        }
    }
     
}
