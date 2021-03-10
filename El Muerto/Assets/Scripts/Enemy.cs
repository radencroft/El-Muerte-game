using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [HideInInspector] public GameObject player;
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public Animator anim;
    [HideInInspector] public BoxCollider2D col;


    [HideInInspector] public bool facingRight;

    public int HP;
    public int damage;
    public int health;


    public void FacingDirection()
    {
        if (transform.rotation.y <= 0)
        {
            facingRight = true;
        }
        else
        {
            facingRight = false;
        }
    }

    public void ReduceHealth()
    {
        health -= damage;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "bullet")
        {
            ReduceHealth();
        }
    }

    public void Die()
    {
        if (health <= 0)
        {
            health = 0;
            anim.SetTrigger("dead");
            col.enabled = false;
        }
    }
}
