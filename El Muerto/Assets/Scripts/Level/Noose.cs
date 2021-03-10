using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Noose : MonoBehaviour
{
    [SerializeField] private bool nooseOnly;
    [SerializeField] private GameObject rope;
    [SerializeField] private Rigidbody2D rbBox;
    private BoxCollider2D player; 

    private void Start()
    { 
        player = GameObject.FindGameObjectWithTag("player").GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        if (!nooseOnly)
        {
            BoxCollider2D colRope = rope.GetComponent<BoxCollider2D>();
            Physics2D.IgnoreCollision(colRope, player);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "bullet")
        {
            Rigidbody2D rbRope = rope.GetComponent<Rigidbody2D>();
            rbRope.bodyType = RigidbodyType2D.Dynamic;
            if (rbBox != null)
            {
                rbBox.bodyType = RigidbodyType2D.Dynamic;
            }
            Destroy(GetComponent<BoxCollider2D>());
        }
    }
}
