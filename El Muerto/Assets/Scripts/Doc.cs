using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doc : Enemy
{
    [Header("SHOOT")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject smoke;
    [SerializeField] private float fireDistance;

    [Header("BULLET SPREAD")]
    [SerializeField] private float startAngle;
    [SerializeField] private float angleBetween;
    [SerializeField] private int numBullets;
    private float angle;
     

    private void Awake()
    { 
        angle = startAngle; 
    }

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

    public void Fire()
    {
        angle = startAngle;  
        for (int i = 0; i < numBullets; i++)
        {
            GameObject b = Instantiate(bullet, firePoint.position, transform.rotation);
            b.GetComponent<bullet>().facingRight = this.facingRight;
            b.GetComponent<bullet>().offsetAngle = angle;
            b.GetComponent<bullet>().suicideTime = fireDistance;

            angle += angleBetween; 
        } 

        Instantiate(smoke, firePoint.position, transform.rotation);
    }

}
