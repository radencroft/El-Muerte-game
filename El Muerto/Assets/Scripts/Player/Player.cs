using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [HideInInspector] public Animator anim;
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public BoxCollider2D col;
    [HideInInspector] public bool facingRight;

    [Header("HEALTH")]
    public int HP;
    [HideInInspector] public int health;
    public float timeToDie;
    

    public void Flip()
    {
        if (rb.velocity.x > 0 && !facingRight || (rb.velocity.x < 0 && facingRight))
        {
            facingRight = !facingRight;
            transform.Rotate(0f, 180f, 0f);
        } 
    }

    public void ReduceHealth(int damage)
    {
        health -= damage;
    }

    public void TakeDamage(int dmg)
    {
        anim.SetTrigger("damage");
        ReduceHealth(dmg);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "enemy bullet")
        {
            TakeDamage(1);
        }

    }

    public void OnCollisionEnter2D(Collision2D other)
    { 
        if (other.gameObject.tag == "quicksand")
        {
            ReduceHealth(5);
        } 
    }

    public void Die()
    {
        if (health <= 0)
        {
            SceneController sceneManager = FindObjectOfType<SceneController>();
            StartCoroutine(loadScene());

            health = 0;
            anim.SetTrigger("dead");
            GetComponent<Inventory>().DropWeapon("pistol A");
            rb.bodyType = RigidbodyType2D.Static;
            col.enabled = false;
        }
    }
    private IEnumerator loadScene()
    {
        yield return new WaitForSeconds(timeToDie);
        SceneController sceneManager = FindObjectOfType<SceneController>();
        sceneManager.ChangeScene();
    }
}
