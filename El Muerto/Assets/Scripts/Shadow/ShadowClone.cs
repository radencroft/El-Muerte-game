using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowClone : MonoBehaviour
{ 
    private Animator anim;
    private Rigidbody2D rb;
    private ShadowInventory inventory;

    public float horizontal;
    private float vertical;

    [Header("MOVEMENT")]
    [SerializeField] private float ST;
    private float speed;

    [SerializeField] private bool facingRight;
    [HideInInspector] public bool stopMovement, stopShooting, inDialogue; 

    [Header("SHOOTING")]
    [SerializeField] private GameObject smoke;
    [SerializeField] private GameObject bullet;
    private Transform firePos;
     
    [Header("GROUND")]
    [SerializeField] private Vector2 checkSize;
    [SerializeField] private LayerMask whatIsGround;
    private bool grounded;

    private Transform groundCheck; 
    [SerializeField] private float groundDistance;

    [Header("FALCON")]
    [SerializeField] private GameObject falcon;
    public bool isFalcon;

    private void Awake()
    { 
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        inventory = GetComponent<ShadowInventory>();
    }

    void Start()
    {
        speed = ST;
        firePos = this.gameObject.transform.GetChild(0);
        groundCheck = this.gameObject.transform.GetChild(1);
    }

    void Update()
    {
        GroundCheck();
        Move();
        Flip();
        InputCheck(); 
    }

    private void InputCheck()
    {
        if (!stopShooting)
        {
            if (Input.GetButtonDown("Fire") && !stopMovement && !inDialogue && grounded && inventory.bullets > 0)
            {
                anim.SetTrigger("shoot"); 
                inventory.ReduceBullet(1);

                //BULLET
                GameObject b = Instantiate(bullet, firePos.position, transform.rotation);
                b.GetComponent<bullet>().facingRight = facingRight;

                //SMOKE
                Instantiate(smoke, firePos.position, transform.rotation);
            }
        }
        else
        {
            if (Input.GetButtonDown("Fire") && !isFalcon && !stopMovement && !inDialogue && grounded)
            {
                isFalcon = true;
                anim.SetTrigger("throw");

                GameObject birdy = Instantiate(falcon, firePos.position, transform.rotation);
                birdy.GetComponent<Falcon>().facingRight = facingRight;
            }
        }
    }

    private void Move()
    {

        if (!stopMovement && !inDialogue && !isFalcon)
        {
            horizontal =  Input.GetAxis("Horizontal") * speed * Time.deltaTime;
            vertical = rb.velocity.y;

            ShadowCheck();

            rb.velocity = new Vector2(horizontal, vertical);
            anim.SetFloat("speed", Mathf.Abs(horizontal));
        }
        else
        {
            rb.velocity = new Vector2(0f, rb.velocity.y);           //blockiram samo X-axis npr. samo da bi padao ako je u vazduhu
            anim.SetFloat("speed", 0f);
        } 
    }

    private void ShadowCheck()
    { 
        if (!AmInShadows())
        {
            if (facingRight && horizontal > 0)
            {
                horizontal = 0;
            }
            else if (!facingRight && horizontal < 0)
            {
                horizontal = 0;
            }
        }
    }

    private void GroundCheck()
    {
        anim.SetBool("grounded", grounded);
    }

    private void Flip()
    {
        if (rb.velocity.x > 0 && !facingRight || (rb.velocity.x < 0 && facingRight))
        {
            facingRight = !facingRight;
            transform.Rotate(0f, 180f, 0f);
        }
    }

    private bool AmInShadows()
    { 
        bool noGroundAhead = Physics2D.Raycast(groundCheck.position, Vector2.down, groundDistance); 
        return noGroundAhead;
    } 
     

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.layer == 10)
        {
            grounded = true;
        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == 10)
        {
            grounded = false;
            Destroy(this.gameObject);
        }
    }
}
