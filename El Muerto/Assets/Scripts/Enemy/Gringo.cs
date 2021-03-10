using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gringo : Enemy
{

    [Header("SHOOT")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject smoke;
    [SerializeField] private float fireDistance;



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
        GameObject b = Instantiate(bullet, firePoint.position, transform.rotation);
        b.GetComponent<bullet>().facingRight = this.facingRight;
        b.GetComponent<bullet>().suicideTime = fireDistance;

        Instantiate(smoke, firePoint.position, transform.rotation);
    } 


}
