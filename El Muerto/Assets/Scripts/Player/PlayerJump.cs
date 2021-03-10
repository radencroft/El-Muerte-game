using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    private BoxCollider2D col;
    private Rigidbody2D rb;
    private Animator anim;
    private PlayerOne player;

    [Header("GROUND")]
    public bool grounded;
    [SerializeField] private Vector2 checkSize;
    [SerializeField] private LayerMask whatIsGround;
    private Vector2 size;

    [Header("JUMP")]
    [SerializeField] private float jumpForce;
    private bool triggerJump;
    private bool inDialogue;

    private void Awake()
    {
        col = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        player = GetComponent<PlayerOne>();
    }
      

    void Update()
    {
        GroundCheck();
        JumpRequest();
    }

    private void FixedUpdate()
    {
        Jump();
    }

    private void GroundCheck()
    {

        if (player.isCrouching)
        {
            size = new Vector2(checkSize.x, 0.5f);
        }
        else
        {
            size = checkSize;
        }

        Vector2 feet = new Vector2(transform.position.x, transform.position.y - col.bounds.extents.y);
        grounded = Physics2D.OverlapBox(feet, size, 0f, whatIsGround);
        anim.SetBool("grounded", grounded); 
    }

    private void JumpRequest()
    {
        inDialogue = GetComponent<PlayerOne>().inDialogue;
        if (Input.GetButtonDown("Jump") && grounded && !player.stopMovement && !inDialogue && !player.isCrouching && !player.isFalcon)
        {
            FindObjectOfType<AudioManager>().Play("jump");
            triggerJump = true;
        }
    }

    private void Jump()
    {  
        if (triggerJump )
        {
            triggerJump = false;
            rb.velocity = Vector2.up * jumpForce;
        }
    }


}
