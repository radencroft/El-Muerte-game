using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadyBlue : Enemy
{
    [Header("ATTACK")]
    [SerializeField] private Transform throwPoint;
    [SerializeField] private GameObject bottle;

    public Vector2 angle;

    public float streingth;
     
    private void Start()
    {
        health = HP;
        anim = GetComponent<Animator>();
        col = GetComponent<BoxCollider2D>();
    }



    private void Update()
    {
        FacingDirection();
        Die();
    }

    public void Throw()
    {
        GameObject b = Instantiate(bottle, throwPoint.position, transform.rotation);
        b.GetComponent<Rigidbody2D>().velocity = angle * streingth;
    }
}
