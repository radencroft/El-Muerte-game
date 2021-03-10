using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 dir;

    [SerializeField] private float speed;
    [HideInInspector] public bool facingRight;  

    [HideInInspector] public float offsetAngle;
    private Vector2 angle;

    [Header("SELFDESTRUCT")]
    [HideInInspector] public float suicideTime = 1f;

    [Header("EXPLOTION")]
    [SerializeField] private GameObject explotion;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        StartCoroutine(selfDestruct());
    }

    private void FixedUpdate()
    {
        Fly();
        Physics2D.IgnoreLayerCollision(11, 11);
        angle = new Vector2(0, offsetAngle);
        dir = facingRight ? Vector2.right + angle : Vector2.left + angle;
    }

    private void Fly()
    {
        rb.velocity = dir * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 8 || other.gameObject.layer == 9 || other.gameObject.layer == 10 || other.gameObject.layer == 12)
        {
            Instantiate(explotion, transform.position, transform.rotation);
            Destroy(this.gameObject);
        } 
    }

    IEnumerator selfDestruct()
    {
        yield return new WaitForSeconds(suicideTime);
        Instantiate(explotion, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }


}
